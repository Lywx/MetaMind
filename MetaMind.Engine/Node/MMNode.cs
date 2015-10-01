// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Actions;
    using Actions.Intervals;
    using Geometry;
    using Microsoft.Xna.Framework;
    using Screen;

    /** @brief MMNode is the main element. Anything thats gets drawn or contains things that get drawn is a MMNode.
        The most popular MMNodes are: MMScene, MMLayer, MMSprite, MMMenu.

        The main features of a MMNode are:
        - They can contain other MMNode nodes (addChild, getChildByTag, removeChild, etc)
        - They can schedule periodic callback (schedule, unschedule, etc)
        - They can execute actions (runAction, stopAction, etc)

        Some MMNode nodes provide extra functionality for them or their children.

        Subclassing a MMNode usually means (one/all) of:
        - overriding init to initialize resources and schedule callbacks
        - create callbacks to handle the advancement of time
        - overriding draw to render the node

        Features of MMNode:
        - position
        - scale (x, y)
        - rotation (in degrees, clockwise)
        - MMCamera (an interface to gluLookAt )
        - MMGridBase (to do mesh transformations)
        - anchor point
        - size
        - visible
        - z-order
        - openGL z position

        Default values:
        - rotation: 0
        - position: (x=0,y=0)
        - scale: (x=1,y=1)
        - contentSize: (x=0,y=0)
        - anchorPoint: (x=0,y=0)

        Limitations:
        - A MMNode is a "void" object. It doesn't have a texture

        Order in transformations with grid disabled
        -# The node will be translated (position)
        -# The node will be rotated (rotation)
        -# The node will be scaled (scale)
        -# The node will be moved according to the camera values (camera)

        Order in transformations with grid enabled
        -# The node will be translated (position)
        -# The node will be rotated (rotation)
        -# The node will be scaled (scale)
        -# The grid will capture the screen
        -# The node will be moved according to the camera values (camera)
        -# The grid will render the captured screen

        Camera:
        - Each node has a camera. By default it points to the center of the MMNode.
        */

    public class MMNode : ICCUpdatable, ICCFocusable, IComparer<MMNode>, IComparable<MMNode>
    {
        // Use this to determine if a tag has been set on the node.
        public const int TagInvalid = -1;

        private bool ignoreAnchorPointForPosition;

        private bool isCleaned;

        private bool transformIsDirty;

        private int tag;

        private uint arrivalIndex, currentChildArrivalIndex;

        private int zOrder;

        private float vertexZ;

        private float rotationX;

        private float rotationY;

        private float scaleX;

        private float scaleY;

        private float skewX;

        private float skewY;

        public MMNodeGraphics Graphics { get; protected set; }

        // opacity controls
        MMPoint anchorPoint;

        MMPoint anchorPointInPoints;

        MMPoint position;

        MMSize contentSize;

        MMPoint3 fauxLocalCameraCenter;

        MMPoint3 fauxLocalCameraTarget;

        MMPoint3 fauxLocalCameraUpDirection;

        Dictionary<int, List<MMNode>> childrenByTag;

        MMScene scene;

        MMLayer layer;

        MMNode parent;

        MMAffineTransform affineLocalTransform;

        MMAffineTransform additionalTransform;

        List<MMEventListener> toBeAddedListeners;                       // The listeners to be added lazily when an EventDispatcher is not yet available

        private struct LazyAction
        {
            public MMAction Action;

            public MMNode Target;

            public bool Paused;

            public LazyAction(MMAction action, MMNode target, bool paused = false)
            {
                this.Action = action;
                this.Target = target;
                this.Paused = paused;
            }
        }

        List<LazyAction> toBeAddedActions;                       // The Actions to be added lazily when an ActionManager is not yet available

        private struct LazySchedule
        {
            public Action<float> Selector;

            public ICCUpdatable Target;

            public float Interval;

            public uint Repeat;

            public float Delay;

            public bool Paused;

            public int Priority;

            public bool IsPriority;

            public LazySchedule(Action<float> selector, ICCUpdatable target, float interval, uint repeat, float delay, bool paused)
            {
                this.Selector   = selector;
                this.Target     = target;
                this.Interval   = interval;
                this.Repeat     = repeat;
                this.Delay      = delay;
                this.Paused     = paused;
                this.Priority   = 0;
                this.IsPriority = false;
            }

            public LazySchedule(ICCUpdatable target, int priority, bool paused)
            {
                this.Selector   = null;
                this.Target     = target;
                this.Interval   = 0;
                this.Repeat     = 0;
                this.Delay      = 0;
                this.Paused     = paused;
                this.Priority   = priority;
                this.IsPriority = true;
            }
        }

        /// <remarks>
        ///     The Schedules to be added lazily when an ScheduleManager is not yet available 
        /// </remarks>
        private List<LazySchedule> toBeAddedSchedules;

        #region Properties

        // Auto-implemented properties

        public bool IsRunning { get; protected set; }

        public virtual bool HasFocus { get; set; }

        public virtual bool Visible { get; set; }

        /// <remarks>
        ///     If this is true, the screen will be recorded into the director's state
        /// </remarks>
        public virtual bool IsSerializable { get; protected set; }      

        public object UserData { get; set; }

        public object UserObject { get; set; }

        public string Name { get; set; }

        public MMRawList<MMNode> Children { get; protected set; }

        protected bool IsReorderChildDirty { get; set; }

        // Manually implemented properties

        public virtual bool CanReceiveFocus => this.Visible;

        public virtual bool IgnoreAnchorPointForPosition
        {
            get { return this.ignoreAnchorPointForPosition; }
            set
            {
                if (value != this.ignoreAnchorPointForPosition)
                {
                    this.ignoreAnchorPointForPosition = value;
                    this.transformIsDirty = true;
                }
            }
        }

        public int Tag
        {
            get { return this.tag; }
            set
            {
                if (this.tag != value)
                {
                    this.Parent?.ChangedChildTag(this, this.tag, value);

                    this.tag = value;
                }
            }
        }

        public int ChildrenCount
        {
            get { return this.Children == null ? 0 : this.Children.Count; }
        }

        public int ZOrder
        {
            get { return this.zOrder; }
            set
            {
                if (this.zOrder != value)
                {
                    if (this.Parent != null)
                        this.Parent.ReorderChild(this, value);

                    this.zOrder = value;

                    if (this.EventDispatcher != null)
                        this.EventDispatcher.MarkDirty = this;
                }
            }
        }

        public int NumberOfRunningActions
        {
            get { return this.ActionManager != null ? this.ActionManager.NumberOfRunningActionsInTarget(this) : 0; }
        }

        public virtual float VertexZ
        {
            get { return this.vertexZ; }
            set
            {
                if (this.vertexZ != value)
                {
                    this.vertexZ = value;
                    this.transformIsDirty = true;
                }
            }
        }

        public virtual float SkewX
        {
            get { return this.skewX; }
            set
            {
                this.skewX = value;
                this.transformIsDirty = true;
            }
        }

        public virtual float SkewY
        {
            get { return this.skewY; }
            set
            {
                this.skewY = value;
                this.transformIsDirty = true;

#if USE_PHYSICS
				if (_physicsBody != null)
				{
					MMLog.Log("Node WARNING: PhysicsBody doesn't support setSkewY");
				}
#endif

            }
        }

        // 2D rotation of the node relative to the 0,1 vector in a clock-wise orientation.
        public virtual float Rotation
        {
            set
            {
                this.rotationX = this.rotationY = value;
                this.transformIsDirty = true;

#if USE_PHYSICS
				if (_physicsBody == null || !_physicsBody._rotationResetTag)
				{
					UpdatePhysicsBodyRotation(Scene);
				}
#endif

            }
        }

        public virtual float RotationX
        {
            get { return this.rotationX; }
            set
            {
                this.rotationX = value;
                this.transformIsDirty = true;
            }
        }

        public virtual float RotationY
        {
            get { return this.rotationY; }
            set
            {
                this.rotationY = value;
                this.transformIsDirty = true;
            }
        }

        // The general scale that applies to both X and Y directions.
        public virtual float Scale
        {
            set
            {
                this.scaleX = this.scaleY = value;
                this.transformIsDirty = true;

#if USE_PHYSICS
				UpdatePhysicsBodyTransform(Scene);
#endif

            }
        }

        public virtual float ScaleX
        {
            get { return this.scaleX; }
            set
            {
                this.scaleX = value;
                this.transformIsDirty = true;

#if USE_PHYSICS
				UpdatePhysicsBodyTransform(Scene);
#endif

            }
        }

        public virtual float ScaleY
        {
            get { return this.scaleY; }
            set
            {
                this.scaleY = value;
                this.transformIsDirty = true;

#if USE_PHYSICS
				UpdatePhysicsBodyTransform(Scene);
#endif

            }
        }

        public virtual float PositionX
        {
            get { return this.position.X; }
            set { this.Position = new MMPoint(value, this.position.Y); }
        }

        public virtual float PositionY
        {
            get { return this.position.Y; }
            set { this.Position = new MMPoint(this.position.X, value); }
        }

        public virtual MMPoint Position
        {
            get { return this.position; }
            set
            {
                if (this.position != value || value == MMPoint.Zero)
                {
                    this.position = value;
                    this.transformIsDirty = true;

#if USE_PHYSICS
					if (_physicsBody == null || !_physicsBody._positionResetTag)
					{
						UpdatePhysicsBodyPosition(Scene);
					}
#endif

                }
            }
        }

        public virtual MMPoint PositionWorldspace
        {
            get
            {
                MMAffineTransform parentWorldTransform
                = this.Parent != null ? this.Parent.AffineWorldTransform : MMAffineTransform.Identity;

                return parentWorldTransform.Transform(this.Position);
            }
        }

        // Returns the anchor point in pixels, AnchorPoint * ContentSize. This does not use
        // the scale factor of the node.
        public virtual MMPoint AnchorPointInPoints
        {
            get { return this.anchorPointInPoints; }
            internal set
            {
                this.anchorPointInPoints = value;
                this.transformIsDirty = true;
            }
        }

        // Returns the Anchor Point of the node as a value [0,1], where 1 is 100% of the dimension and 0 is 0%.
        public virtual MMPoint AnchorPoint
        {
            get { return this.anchorPoint; }
            set
            {

#if USE_PHYSICS
				if (_physicsBody != null)
				{
                    if (!value.Equals(MMPoint.AnchorMiddle))
					    MMLog.Log("Node warning: This node has a physics body, the anchor must be in the middle, you cann't change this to other value.");
                    else
					    UpdatePhysicsBodyPosition(Scene);
				}
#endif

                if (!value.Equals(this.anchorPoint))
                {
                    this.anchorPoint = value;
                    this.anchorPointInPoints = new MMPoint(this.contentSize.Width * this.anchorPoint.X, this.contentSize.Height * this.anchorPoint.Y);
                    this.transformIsDirty = true;
                }
            }
        }

        public virtual MMSize ScaledContentSize
        {
            get
            {
                var sizeToScale = this.ContentSize;
                return new MMSize(sizeToScale.Width * this.ScaleX, sizeToScale.Height * this.ScaleY);
            }
        }

        public virtual MMSize ContentSize
        {
            get { return this.contentSize; }
            set
            {
                if (!MMSize.Equal(ref value, ref this.contentSize))
                {
                    this.contentSize = value;
                    this.anchorPointInPoints = new MMPoint(this.contentSize.Width * this.anchorPoint.X, this.contentSize.Height * this.anchorPoint.Y);

                    this.transformIsDirty = true;
                }
            }
        }

        // Returns the bounding box of this node in parent space
        public MMRect BoundingBox
        {
            get
            {
                MMPoint boundingBoxOrigin = this.Position;

                if (!this.IgnoreAnchorPointForPosition)
                {
                    boundingBoxOrigin -= this.AnchorPointInPoints;
                }

                return new MMRect(boundingBoxOrigin.X, boundingBoxOrigin.Y, this.ContentSize.Width, this.ContentSize.Height);
            }
        }

        // Bounding box after scale/rotation/skew in parent space
        public MMRect BoundingBoxTransformedToParent
        {
            get
            {
                MMAffineTransform localTransform = this.AffineLocalTransform;
                MMRect transformedBounds = localTransform.Transform(new MMRect(0.0f, 0.0f, this.ContentSize.Width, this.ContentSize.Height));
                return transformedBounds;
            }
        }

        // Bounding box after scale/rotation/skew in world space
        public MMRect BoundingBoxTransformedToWorld
        {
            get
            {
                MMAffineTransform localTransform = this.AffineWorldTransform;
                MMRect worldtransformedBounds = localTransform.Transform(new MMRect(0.0f, 0.0f, this.ContentSize.Width, this.ContentSize.Height));
                return worldtransformedBounds;
            }
        }

        public virtual MMAffineTransform AffineLocalTransform
        {
            get { if (this.transformIsDirty) this.UpdateTransform(); return this.affineLocalTransform; }
        }

        public MMAffineTransform AffineWorldTransform
        {
            get
            {
                MMAffineTransform worldTransform = this.AffineLocalTransform;
                MMNode parent = this.Parent;
                if (parent != null)
                {
                    var parentTransform = parent.AffineWorldTransform;
                    MMAffineTransform.Concat(ref worldTransform, ref parentTransform, out worldTransform);
                }

                return worldTransform;
            }
        }

        public MMAffineTransform AdditionalTransform
        {
            get { return this.additionalTransform; }
            set
            {
                if (value != this.additionalTransform)
                {
                    this.additionalTransform = value;
                    this.transformIsDirty = true;
                }
            }
        }

        public MMNode this[int tag]
        {
            get { return this.GetChildByTag(tag); }
        }

        public virtual MMScene Scene
        {
            get { return this.scene; }
            internal set
            {
                if (this.scene != value)
                {
                    if (this.scene != null)
                    {
                        this.scene.SceneViewportChanged -=
                            new CocosSharp.MMScene.SceneViewportChangedEventHandler(OnSceneViewportChanged);
                    }

                    this.scene = value;

                    // All the children should belong to same scene
                    if (this.Children != null)
                    {
                        foreach (MMNode child in this.Children)
                        {
                            child.Scene = this.scene;
                        }
                    }

                    if (this.scene != null)
                    {
                        this.scene.SceneViewportChanged +=
                            new CocosSharp.MMScene.SceneViewportChangedEventHandler(OnSceneViewportChanged);

                        this.OnSceneViewportChanged(this, null);

                        this.AddedToScene();

                        this.AttachActions();
                        this.AttachSchedules();
                    }

                    this.AttachEvents();
                }
            }
        }

        public virtual MMLayer Layer
        {
            get { return this.layer; }
            internal set
            {
                if (this.layer != value)
                {
                    if (this.layer != null)
                    {
                        this.layer.LayerVisibleBoundsChanged -=
                            new CocosSharp.MMLayer.LayerVisibleBoundsChangedEventHandler(OnLayerVisibleBoundsChanged);
                    }

                    this.layer = value;

                    // All the children should belong to same layer
                    if (this.Children != null)
                    {
                        foreach (MMNode child in this.Children)
                        {
                            child.Layer = this.layer;
                        }
                    }

                    if (this.layer != null)
                    {
                        this.layer.LayerVisibleBoundsChanged +=
                                        new CocosSharp.MMLayer.LayerVisibleBoundsChangedEventHandler(OnLayerVisibleBoundsChanged);

                        this.OnLayerVisibleBoundsChanged(this, null);
                    }

                    if (this.layer != null && this.layer.Scene != null)
                        this.Scene = this.layer.Scene;
                }
            }
        }

        public MMNode Parent
        {
            get { return this.parent; }
            internal set
            {
                if (this.parent != value)
                {
                    this.parent = value;

                    this.ParentUpdatedTransform();
                }
            }
        }

        public virtual MMApplication Application
        {
            get { return this.Window != null ? this.Window.Application : null; }
        }

        public virtual MMDirector Director
        {
            get { return this.Scene.Director; }
            set { this.Scene.Director = value; }
        }

        public virtual MMCamera Camera
        {
            get { return (this.Layer == null) ? null : this.Layer.Camera; }
            set
            {
                if (this.Layer != null)
                    this.Layer.Camera = value;
            }
        }

        public virtual MMWindow Window
        {
            get { return this.Scene != null ? this.Scene.Window : null; }
            set { this.Scene.Window = value; }
        }

        public virtual MMViewport Viewport
        {
            get { return this.Scene != null ? this.Scene.Viewport : null; }
            set { this.Scene.Viewport = value; }
        }

        internal virtual MMEventDispatcher EventDispatcher
        {
            get { return this.Scene != null ? this.Scene.EventDispatcher : null; }
        }

        internal bool EventDispatcherIsEnabled
        {
            get { return this.EventDispatcher != null ? this.EventDispatcher.IsEnabled : false; }
            set
            {
                if (this.EventDispatcher != null)
                    this.EventDispatcher.IsEnabled = value;
            }
        }

        internal MMPoint3 FauxLocalCameraCenter
        {
            get { return this.fauxLocalCameraCenter; }
            set
            {
                if (this.fauxLocalCameraCenter != value)
                {
                    this.fauxLocalCameraCenter = value;
                    this.transformIsDirty = true;
                }
            }
        }

        internal MMPoint3 FauxLocalCameraTarget
        {
            get { return this.fauxLocalCameraTarget; }
            set
            {
                if (this.fauxLocalCameraTarget != value)
                {
                    this.fauxLocalCameraTarget = value;
                    this.transformIsDirty = true;
                }
            }
        }

        internal MMPoint3 FauxLocalCameraUpDirection
        {
            get { return this.fauxLocalCameraUpDirection; }
            set
            {
                if (this.fauxLocalCameraUpDirection != value)
                {
                    this.fauxLocalCameraUpDirection = value;
                    this.transformIsDirty = true;
                }
            }
        }

        MMScheduler Scheduler
        {
            get { return this.Application != null ? this.Application.Scheduler : null; }
        }

        MMActionManager ActionManager
        {
            get { return this.Application != null ? this.Application.ActionManager : null; }
        }

        #endregion Properties

        #region Constructors

        public MMNode(MMSize contentSize) : this()
        {
            this.ContentSize = contentSize;
        }

        public MMNode()
        {
#if USE_PHYSICS
			_physicsBody = null;
			_physicsScaleStartX = 1.0f;
			_physicsScaleStartY = 1.0f;
#endif

            this.additionalTransform = MMAffineTransform.Identity;
            this.scaleX = 1.0f;
            this.scaleY = 1.0f;
            this.Visible = true;
            this.tag = TagInvalid;

            this.HasFocus = false;
            this.IsSerializable = true;

            this.FauxLocalCameraUpDirection = new MMPoint3(0.0f, 1.0f, 0.0f);
        }

        #endregion Constructors


        #region Physics

#if USE_PHYSICS

		void UpdatePhysicsBodyTransform(MMScene scene)
		{
			UpdatePhysicsBodyScale(scene);
			UpdatePhysicsBodyPosition(scene);
			UpdatePhysicsBodyRotation(scene);
		}

		void UpdatePhysicsBodyPosition(MMScene scene)
		{
			if (_physicsBody != null)
			{
				//_physicsBody.Position = new cpVect(PositionX, PositionY);

				if (scene != null && scene.PhysicsWorld != null)
				{
					var pos = Parent == scene ? Position : scene.WorldToParentspace(Position);
                    _physicsBody.Position = Position;//new cpVect(PositionX, PositionY);
				}
				else
				{
					_physicsBody.Position = Position;
				}
			}

			if (Children != null)
			{
				foreach (var child in Children)
				{
					if (child != null)
						child.UpdatePhysicsBodyPosition(scene);
				}
			}
		}

		void UpdatePhysicsBodyRotation(MMScene scene)
		{
			//if (_physicsBody != null)
			//{
			//	if (scene != null && scene.GetPhysicsWorld() != null)
			//	{
			//		float rotation = _rotationZ_X;
			//		for (MMNode parent = Parent; parent != scene; parent = parent.Parent)
			//		{
			//			rotation += parent.Rotation;
			//		}
			//		_physicsBody.SetRotation(rotation);
			//	}
			//	else
			//	{
			//		_physicsBody.SetRotation(_rotationZ_X);
			//	}
			//}

			//foreach (var child in Children)
			//{
			//	child.UpdatePhysicsBodyRotation(scene);
			//	child.UpdatePhysicsBodyPosition(scene);
			//}
		}

		void UpdatePhysicsBodyScale(MMScene scene)
		{

			if (_physicsBody != null)
			{

				if (scene != null && scene.PhysicsWorld != null)
				{
					float scaleX = this.scaleX / _physicsScaleStartX;
					float scaleY = this.scaleY / _physicsScaleStartY;
					for (MMNode parent = Parent; parent != scene; parent = parent.Parent)
					{
						scaleX *= parent.ScaleX;
						scaleY *= parent.ScaleY;
					}
					_physicsBody.SetScale(scaleX, scaleY);
				}
				else
				{
					_physicsBody.SetScale(scaleX / _physicsScaleStartX, scaleY / _physicsScaleStartY);
				}
			}

			if (Children != null)
			{
				foreach (var child in Children)
				{
					child.UpdatePhysicsBodyRotation(scene);
					child.UpdatePhysicsBodyPosition(scene);
				}

			}


		}

		/** *   set the PhysicsBody that let the sprite effect with physics * @note This method will set anchor point to Vec2::ANCHOR_MIDDLE if body not null, and you cann't change anchor point if node has a physics body. */

		public MMPhysicsBody PhysicsBody
		{
			get { return _physicsBody; }
			set
			{

				var body = value;
				if (_physicsBody == body)
				{
					return;
				}

				if (body != null)
				{
					if (body.Node != null)
					{
						body.Node.PhysicsBody = null;
					}

					body._node = this;
					//body->retain();

					// physics rotation based on body position, but node rotation based on node anthor point
					// it cann't support both of them, so I clear the anthor point to default.
					if (AnchorPoint != MMPoint.AnchorMiddle)
					{
						MMLog.Log("Node warning: setPhysicsBody sets anchor point to MMPoint.AnchorMiddle.");
						AnchorPoint = MMPoint.AnchorMiddle;
					}
				}

				if (_physicsBody != null)
				{
					var world = _physicsBody.GetWorld();
					_physicsBody.RemoveFromWorld();
					_physicsBody._node = null;
					//_physicsBody->release();

					if (world != null && body != null)
					{
						world.AddBody(body);
					}
				}

				_physicsBody = body;
				_physicsScaleStartX = scaleX;
				_physicsScaleStartY = scaleY;

				if (body != null)
				{
					MMNode node;
					MMScene scene = null;
					for (node = this.Parent; node != null; node = node.Parent)
					{
						MMScene tmpScene = node as MMScene;
						if (tmpScene != null && tmpScene.PhysicsWorld != null)
						{
							scene = tmpScene;
							break;
						}
					}

					if (scene != null)
					{
                        UpdatePhysicsBodyTransform(scene);
						scene.PhysicsWorld.AddBody(body);
					}

					UpdatePhysicsBodyTransform(scene);
				}


			}
		}




#endif

        #endregion


        #region Event dispatcher handling

        internal void AttachEvents()
        {
            if (this.EventDispatcher == null)
                return;

            if (this.toBeAddedListeners != null && this.toBeAddedListeners.Count > 0)
            {
                var eventDispatcher = this.EventDispatcher;
                foreach (var listener in this.toBeAddedListeners)
                {
                    if (listener.SceneGraphPriority != null)
                        eventDispatcher.AddEventListener(listener, listener.SceneGraphPriority);
                    else
                        eventDispatcher.AddEventListener(listener, listener.FixedPriority, this);
                }

                this.toBeAddedListeners.Clear();
                this.toBeAddedListeners = null;
            }
        }

        #endregion Event dispatcher handling


        #region Scene callbacks

        // Hidden event handlers

        void OnSceneViewportChanged(object sender, EventArgs e)
        {
            if (this.Scene != null && this.Window != null && this.Viewport != null && this.Camera != null)
            {
                this.ViewportChanged();
                this.VisibleBoundsChanged();
            }
        }

        void OnLayerVisibleBoundsChanged(object sender, EventArgs e)
        {
            if (this.Scene != null && this.Viewport != null && this.Camera != null)
                this.VisibleBoundsChanged();
        }

        // Users override methods below to listen to changes to scene

        protected virtual void VisibleBoundsChanged()
        {
        }

        protected virtual void ViewportChanged()
        {
        }

        protected virtual void AddedToScene()
        {
        }

        #endregion Scene callbacks


        #region Cleaning up

        ~MMNode()
        {

#if USE_PHYSICS
			this._physicsBody = null;
#endif
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of managed resources
            }

            // Want to stop all actions and timers regardless of whether or not this object was explicitly disposed
            this.Cleanup();

            if (this.EventDispatcher != null)
                this.EventDispatcher.RemoveEventListeners(this);

            // Clean up the UserData and UserObject as these may hold references to other MMNodes.
            this.UserData = null;
            this.UserObject = null;

            if (this.Children != null && this.Children.Count > 0)
            {
                MMNode[] elements = this.Children.Elements;
                foreach (MMNode child in this.Children.Elements)
                {
                    if (child != null)
                    {
                        if (!child.isCleaned)
                        {
                            child.OnExit();
                        }
                        child.Parent = null;
                    }
                }
            }

        }

        protected virtual void ResetCleanState()
        {
            this.isCleaned = false;
            if (this.Children != null && this.Children.Count > 0)
            {
                MMNode[] elements = this.Children.Elements;
                for (int i = 0, count = this.Children.Count; i < count; i++)
                {
                    elements[i].ResetCleanState();
                }
            }
        }

        public virtual void Cleanup()
        {
            if (this.isCleaned == true)
            {
                return;
            }

            // actions
            this.StopAllActions();

            // timers
            this.UnscheduleAll();

            if (this.Children != null && this.Children.Count > 0)
            {
                MMNode[] elements = this.Children.Elements;
                for (int i = 0, count = this.Children.Count; i < count; i++)
                {
                    elements[i].Cleanup();
                }
            }

            this.Scene = null;
            this.Layer = null;
            this.Camera = null;
            this.isCleaned = true;
        }

        #endregion Cleaning up


        #region Unit conversion

        public MMPoint ConvertToWorldspace(MMPoint point)
        {
            var transformedPoint = this.AffineWorldTransform.Transform(point);
            return transformedPoint;

        }

        public MMRect ConvertToWorldspace(MMRect rect)
        {
            var transformedRect = this.AffineWorldTransform.Transform(rect);
            return transformedRect;

        }

        public MMPoint WorldToParentspace(MMPoint point)
        {
            MMPoint transformedPoint = this.AffineWorldTransform.Inverse.Transform(point);
            transformedPoint += this.BoundingBox.Origin;

            return transformedPoint;
        }

        public MMPoint ScreenToWorldspace(MMPoint point)
        {
            return this.Layer.ScreenToWorldspace(point);
        }

        public MMRect VisibleBoundsWorldspace
        {
            get
            {
                return this.Layer.VisibleBoundsWorldspace;
            }
        }

        #endregion Unit conversion


        #region Serialization

        public virtual void Serialize(Stream stream)
        {
            StreamWriter sw = new StreamWriter(stream);
            MMSerialization.SerializeData(this.Visible, sw);
            MMSerialization.SerializeData(this.rotationX, sw);
            MMSerialization.SerializeData(this.rotationY, sw);
            MMSerialization.SerializeData(this.scaleX, sw);
            MMSerialization.SerializeData(this.scaleY, sw);
            MMSerialization.SerializeData(this.skewX, sw);
            MMSerialization.SerializeData(this.skewY, sw);
            MMSerialization.SerializeData(this.VertexZ, sw);
            MMSerialization.SerializeData(this.ignoreAnchorPointForPosition, sw);
            MMSerialization.SerializeData(this.IsRunning, sw);
            MMSerialization.SerializeData(this.IsReorderChildDirty, sw);
            MMSerialization.SerializeData(this.tag, sw);
            MMSerialization.SerializeData(this.zOrder, sw);
            MMSerialization.SerializeData(this.anchorPoint, sw);
            MMSerialization.SerializeData(this.contentSize, sw);
            MMSerialization.SerializeData(this.Position, sw);
            if (this.Children != null)
            {
                MMSerialization.SerializeData(this.Children.Count, sw);
                foreach (MMNode child in this.Children)
                {
                    sw.WriteLine(child.GetType().AssemblyQualifiedName);
                }
                foreach (MMNode child in this.Children)
                {
                    child.Serialize(stream);
                }
            }
            else
            {
                MMSerialization.SerializeData(0, sw); // No children
            }
        }

        public virtual void Deserialize(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            this.Visible = MMSerialization.DeSerializeBool(sr);
            this.rotationX = MMSerialization.DeSerializeFloat(sr);
            this.rotationY = MMSerialization.DeSerializeFloat(sr);
            this.scaleX = MMSerialization.DeSerializeFloat(sr);
            this.scaleY = MMSerialization.DeSerializeFloat(sr);
            this.skewX = MMSerialization.DeSerializeFloat(sr);
            this.skewY = MMSerialization.DeSerializeFloat(sr);
            this.VertexZ = MMSerialization.DeSerializeFloat(sr);
            this.ignoreAnchorPointForPosition = MMSerialization.DeSerializeBool(sr);
            this.IsRunning = MMSerialization.DeSerializeBool(sr);
            this.IsReorderChildDirty = MMSerialization.DeSerializeBool(sr);
            this.tag = MMSerialization.DeSerializeInt(sr);
            this.zOrder = MMSerialization.DeSerializeInt(sr);
            this.AnchorPoint = MMSerialization.DeSerializePoint(sr);
            this.ContentSize = MMSerialization.DeSerializeSize(sr);
            this.Position = MMSerialization.DeSerializePoint(sr);
            // m_UserData is handled by the specialized class.
            // TODO: Serializze the action manager
            // TODO :Serialize the grid
            // TODO: Serialize the camera
            string s;
            int count = MMSerialization.DeSerializeInt(sr);
            for (int i = 0; i < count; i++)
            {
                s = sr.ReadLine();
                Type screenType = Type.GetType(s);
                MMNode scene = Activator.CreateInstance(screenType) as MMNode;
                this.AddChild(scene);
                scene.Deserialize(stream);
            }
        }

        #endregion Serialization

        public MMNode GetChildByTag(int tag)
        {
            Debug.Assert(tag != (int)MMNodeTag.Invalid, "Invalid tag");

            if (this.childrenByTag != null && this.childrenByTag.Count > 0)
            {
                Debug.Assert(this.Children != null && this.Children.Count > 0);

                List<MMNode> list;
                if (this.childrenByTag.TryGetValue(tag, out list))
                {
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                }
            }
            return null;
        }

        #region AddChild

        public void AddChild(MMNode child, int zOrder = 0)
        {
            Debug.Assert(child != null, "Argument must be no-null");
            this.AddChild(child, zOrder, child.Tag);
        }

        public virtual void AddChild(MMNode child, int zOrder, int tag)
        {
            Debug.Assert(child != null, "Argument must be non-null");
            Debug.Assert(child.Parent == null, "child already added. It can't be added again");
            Debug.Assert(child != this, "Can not add myself to myself.");

            if (this.Children == null)
            {
                this.Children = new MMRawList<MMNode>();
            }

            child.arrivalIndex = ++this.currentChildArrivalIndex;

            this.InsertChild(child, zOrder, tag);

            child.Parent = this;
            child.tag = tag;
            if (child.isCleaned)
            {
                child.ResetCleanState();
            }

            // We want all our children to have the same layer as us
            // Set this before we call child.OnEnter
            child.Layer = this.Layer;
            child.Scene = this.Scene;

#if USE_PHYSICS
			// Recursive add children with which have physics body.
			if (Scene != null && Scene.PhysicsWorld != null)
			{
				child.UpdatePhysicsBodyTransform(Scene);
				Scene.AddChildToPhysicsWorld(child);
			}
#endif

            if (this.IsRunning)
            {
                child.OnEnter();
                child.OnEnterTransitionDidFinish();
            }
        }

        void InsertChild(MMNode child, int z, int tag)
        {
            this.IsReorderChildDirty = true;
            this.Children.Add(child);

            this.ChangedChildTag(child, TagInvalid, tag);

            child.zOrder = z;
        }

        #endregion AddChild


        #region RemoveChild

        public void RemoveFromParent(bool cleanup = true)
        {
            if (this.Parent != null)
            {
                this.Parent.RemoveChild(this, cleanup);
            }
        }

        public virtual void RemoveChild(MMNode child, bool cleanup = true)
        {
            // explicit nil handling
            if (this.Children == null || child == null)
            {
                return;
            }

            this.ChangedChildTag(child, child.Tag, TagInvalid);

            if (this.Children.Contains(child))
            {
                this.DetachChild(child, cleanup);
            }
        }

        public void RemoveChildByTag(int tag, bool cleanup = true)
        {
            Debug.Assert(tag != (int)MMNodeTag.Invalid, "Invalid tag");

            MMNode child = this[tag];

            if (child == null)
            {
                MMLog.Log("CocosSharp: removeChildByTag: child not found!");
            }
            else
            {
                this.RemoveChild(child, cleanup);
            }
        }

        public virtual void RemoveAllChildrenByTag(int tag, bool cleanup = true)
        {
            Debug.Assert(tag != (int)MMNodeTag.Invalid, "Invalid tag");
            while (true)
            {
                MMNode child = this[tag];
                if (child == null)
                {
                    break;
                }
                this.RemoveChild(child, cleanup);
            }
        }

        public virtual void RemoveAllChildren(bool cleanup = true)
        {
            // not using detachChild improves speed here
            if (this.Children != null && this.Children.Count > 0)
            {
                if (this.childrenByTag != null)
                {
                    this.childrenByTag.Clear();
                }

                MMNode[] elements = this.Children.Elements;
                for (int i = 0, count = this.Children.Count; i < count; i++)
                {
                    MMNode node = elements[i];

                    // IMPORTANT:
                    //  -1st do onExit
                    //  -2nd cleanup
                    if (this.IsRunning)
                    {
                        node.OnExitTransitionDidStart();
                        node.OnExit();
                    }

#if USE_PHYSICS
					if (node._physicsBody != null)
					{
						node._physicsBody.RemoveFromWorld();
					}
#endif

                    if (cleanup)
                    {
                        node.Cleanup();
                    }

                    // set parent nil at the end
                    node.Parent = null;
                }

                this.Children.Clear();
            }
        }

        void DetachChild(MMNode child, bool doCleanup)
        {
            // IMPORTANT:
            //  -1st do onExit
            //  -2nd cleanup
            if (this.IsRunning)
            {
                child.OnExitTransitionDidStart();
                child.OnExit();
            }

#if USE_PHYSICS

			if (child._physicsBody != null)
			{
				child._physicsBody.RemoveFromWorld();
			}

#endif

            // If you don't do cleanup, the child's actions will not get removed and the
            // its scheduledSelectors_ dict will not get released!
            if (doCleanup)
            {
                child.Cleanup();
            }

            // set parent nil at the end
            child.Parent = null;

            this.Children.Remove(child);
        }

        #endregion RemoveChild


        #region Child Sorting

        int IComparer<MMNode>.Compare(MMNode n1, MMNode n2)
        {
            return n1.CompareTo(n2);
        }

        public int CompareTo(MMNode that)
        {
            int compare = this.ZOrder.CompareTo(that.ZOrder);

            // In the case where zOrders are equivalent, resort to ordering
            // based on when children were added to parent
            if (compare == 0)
                compare = this.arrivalIndex.CompareTo(that.arrivalIndex);

            return compare;
        }

        public void SortAllChildren()
        {
            if (this.IsReorderChildDirty)
            {
                Array.Sort(this.Children.Elements, 0, this.Children.Count, this);
                this.IsReorderChildDirty = false;
            }
        }

        void ChangedChildTag(MMNode child, int oldTag, int newTag)
        {
            List<MMNode> list;

            if (this.childrenByTag != null && oldTag != TagInvalid)
            {
                if (this.childrenByTag.TryGetValue(oldTag, out list))
                {
                    list.Remove(child);
                }
            }

            if (newTag != TagInvalid)
            {
                if (this.childrenByTag == null)
                {
                    this.childrenByTag = new Dictionary<int, List<MMNode>>();
                }

                if (!this.childrenByTag.TryGetValue(newTag, out list))
                {
                    list = new List<MMNode>();
                    this.childrenByTag.Add(newTag, list);
                }

                list.Add(child);
            }
        }

        public virtual void ReorderChild(MMNode child, int zOrder)
        {
            Debug.Assert(child != null, "Child must be non-null");

            // lets not do anything here if the z-order is not to be changed
            if (child.zOrder == zOrder)
                return;

            this.IsReorderChildDirty = true;
            child.zOrder = zOrder;
        }

        #endregion Child Sorting


        #region Events and Listeners

        /// <summary>
        /// Adds a event listener for a specified event with the priority of scene graph.
        /// The priority of scene graph will be fixed value 0. So the order of listener item
        /// in the vector will be ' <0, scene graph (0 priority), >0'.
        /// </summary>
        /// <param name="listener">The listener of a specified event.</param>
        /// <param name="node">The priority of the listener is based on the draw order of this node.</param>
        public void AddEventListener(MMEventListener listener, MMNode node = null)
        {

            if (node == null)
                node = this;

            if (this.EventDispatcherIsEnabled)
            {
                this.EventDispatcher.AddEventListener(listener, node);
            }
            else
            {
                if (this.toBeAddedListeners == null)
                    this.toBeAddedListeners = new List<MMEventListener>();

                listener.SceneGraphPriority = node;
                this.toBeAddedListeners.Add(listener);
            }
        }

        /// <summary>
        /// Adds a event listener for a specified event with the fixed priority.
        /// A lower priority will be called before the ones that have a higher value.
        /// 0 priority is not allowed for fixed priority since it's used for scene graph based priority.
        /// </summary>
        /// <param name="listener">The listener of a specified event.</param>
        /// <param name="fixedPriority">The fixed priority of the listener.</param>
        public void AddEventListener(MMEventListener listener, int fixedPriority)
        {
            if (this.EventDispatcherIsEnabled)
            {
                this.EventDispatcher.AddEventListener(listener, fixedPriority, this);
            }
            else
            {
                if (this.toBeAddedListeners == null)
                    this.toBeAddedListeners = new List<MMEventListener>();

                listener.FixedPriority = fixedPriority;
                this.toBeAddedListeners.Add(listener);
            }
        }

        /// <summary>
        /// Adds a Custom event listener.
        /// It will use a fixed priority of 1.
        /// </summary>
        /// <returns>The generated event. Needed in order to remove the event from the dispather.</returns>
        /// <param name="eventName">Event name.</param>
        /// <param name="callback">Callback.</param>
        public MMEventListenerCustom AddCustomEventListener(string eventName, Action<MMEventCustom> callback)
        {
            var listener = new MMEventListenerCustom(eventName, callback);
            this.AddEventListener(listener, 1);
            return listener;
        }

        /// <summary>
        /// Remove a listener
        /// </summary>
        /// <param name="listener">The specified event listener which needs to be removed.</param>
        public void RemoveEventListener(MMEventListener listener)
        {
            if (this.EventDispatcherIsEnabled)
                this.EventDispatcher.RemoveEventListener(listener);

            if (this.toBeAddedListeners != null && this.toBeAddedListeners.Contains(listener))
                this.toBeAddedListeners.Remove(listener);
        }

        /// <summary>
        /// Removes all listeners with the same event listener type
        /// </summary>
        /// <param name="listenerType"></param>
        public void RemoveEventListeners(MMEventListenerType listenerType)
        {
            if (this.EventDispatcher != null)
                this.EventDispatcher.RemoveEventListeners(listenerType);

            if (this.toBeAddedListeners != null)
            {

                var listenerID = string.Empty;
                switch (listenerType)
                {
                    case MMEventListenerType.TOUCH_ONE_BY_ONE:
                        listenerID = MMEventListenerTouchOneByOne.LISTENER_ID;
                        break;
                    case MMEventListenerType.TOUCH_ALL_AT_ONCE:
                        listenerID = MMEventListenerTouchAllAtOnce.LISTENER_ID;
                        break;
                    case MMEventListenerType.MOUSE:
                        listenerID = MMEventListenerMouse.LISTENER_ID;
                        break;
                    case MMEventListenerType.AMMELEROMETER:
                        listenerID = MMEventListenerAccelerometer.LISTENER_ID;
                        break;
                    case MMEventListenerType.KEYBOARD:
                        listenerID = MMEventListenerKeyboard.LISTENER_ID;
                        break;
                    case MMEventListenerType.GAMEPAD:
                        listenerID = MMEventListenerGamePad.LISTENER_ID;
                        break;

                    default:
                        Debug.Assert(false, "Invalid listener type!");
                        break;
                }

                for (int i = 0; i < this.toBeAddedListeners.Count; i++)
                {
                    if (this.toBeAddedListeners[i].ListenerID == listenerID)
                    {
                        this.toBeAddedListeners.RemoveAt(i);
                    }
                }

                if (this.toBeAddedListeners.Count == 0)
                    this.toBeAddedListeners = null;

            }
        }

        /// <summary>
        /// Removes all listeners which are associated with the specified target.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="recursive"></param>
        public void RemoveEventListeners(MMNode target, bool recursive = false)
        {
            if (this.EventDispatcher != null)
                this.EventDispatcher.RemoveEventListeners(target, recursive);

            if (this.toBeAddedListeners != null)
            {
                for (int i = 0; i < this.toBeAddedListeners.Count; i++)
                {
                    if (this.toBeAddedListeners[i].SceneGraphPriority == target)
                    {
                        this.toBeAddedListeners.RemoveAt(i);
                    }
                }

                if (this.toBeAddedListeners.Count == 0)
                    this.toBeAddedListeners = null;

            }
        }

        /// <summary>
        /// Removes all listeners which are associated with this node.
        /// </summary>
        /// <param name="recursive"></param>
        public void RemoveEventListeners(bool recursive = false)
        {
            this.RemoveEventListeners(this, recursive);
        }

        /// <summary>
        /// Removes all listeners
        /// </summary>
        public void RemoveAllListeners()
        {
            if (this.EventDispatcher != null)
                this.EventDispatcher.RemoveAll();

            if (this.toBeAddedListeners != null)
            {
                this.toBeAddedListeners.Clear();
                this.toBeAddedListeners = null;
            }
        }

        /// <summary>
        /// Pauses all listeners which are associated the specified target.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="recursive"></param>
        public void PauseListeners(MMNode target, bool recursive = false)
        {
            if (this.EventDispatcher != null)
                this.EventDispatcher.Pause(target, recursive);
        }

        /// <summary>
        /// Pauses all listeners which are associated the specified this node.
        /// </summary>
        /// <param name="recursive"></param>
        public void PauseListeners(bool recursive = false)
        {
            this.PauseListeners(this, recursive);
        }

        /// <summary>
        /// Resumes all listeners which are associated the specified target.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="recursive"></param>
        public void ResumeListeners(MMNode target, bool recursive = false)
        {
            if (this.EventDispatcher != null)
                this.EventDispatcher.Resume(target, recursive);

        }

        /// <summary>
        /// Resumes all listeners which are associated the this node.
        /// </summary>
        /// <param name="recursive"></param>
        public void ResumeListeners(bool recursive = false)
        {
            this.ResumeListeners(this, recursive);
        }

        /// <summary>
        /// Sets listener's priority with fixed value.
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="fixedPriority"></param>
        public void SetListenerPriority(MMEventListener listener, int fixedPriority)
        {
            if (this.EventDispatcherIsEnabled)
                this.EventDispatcher.SetPriority(listener, fixedPriority);

            if (this.toBeAddedListeners != null && this.toBeAddedListeners.Contains(listener))
            {
                var found = this.toBeAddedListeners.IndexOf(listener);
                this.toBeAddedListeners[found].FixedPriority = fixedPriority;
            }
        }

        /// <summary>
        /// Dispatchs a custom event.
        /// </summary>
        /// <param name="customEvent">Custom event.</param>
        /// <param name="userData">User data.</param>
        public void DispatchEvent(string customEvent, object userData = null)
        {
            if (this.EventDispatcherIsEnabled)
                this.EventDispatcher.DispatchEvent(customEvent, userData);

        }

        /// <summary>
        /// Dispatches the event
        /// Also removes all EventListeners marked for deletion from the event dispatcher list.
        /// </summary>
        /// <param name="eventToDispatch"></param>
        public void DispatchEvent(MMEvent eventToDispatch)
        {
            if (this.EventDispatcherIsEnabled)
                this.EventDispatcher.DispatchEvent(eventToDispatch);
        }


        #endregion Events and Listeners

        public virtual void Update(float dt)
        {
        }

        public void Visit()
        {
            var identity = MMAffineTransform.Identity;
            Visit(ref identity);
        }

        public virtual void Visit(ref MMAffineTransform parentWorldTransform)
        {
            if (!this.Visible)
                return;

            if (this.transformIsDirty)
                this.UpdateTransform();


            var worldTransform = MMAffineTransform.Identity;
            MMAffineTransform.Concat(ref this.affineLocalTransform, ref parentWorldTransform, out worldTransform);

            this.SortAllChildren();

            VisitRenderer(ref worldTransform);

            if (this.Children != null)
            {
                var elements = this.Children.Elements;
                for (int i = 0, N = this.Children.Count; i < N; ++i)
                {
                    var child = elements[i];
                    if (child.Visible)
                        child.Visit(ref worldTransform);
                }
            }
        }

        protected virtual void VisitRenderer(ref MMAffineTransform worldTransform)
        {
        }
        #region Entering and exiting

        public virtual void OnEnter()
        {

            if (this.Children != null && this.Children.Count > 0)
            {
                MMNode[] elements = this.Children.Elements;
                for (int i = 0, count = this.Children.Count; i < count; i++)
                {
                    elements[i].OnEnter();
                }
            }

            this.Resume();

            this.IsRunning = true;

        }

        public virtual void OnEnterTransitionDidFinish()
        {
            if (this.Children != null && this.Children.Count > 0)
            {
                MMNode[] elements = this.Children.Elements;
                for (int i = 0, count = this.Children.Count; i < count; i++)
                {
                    elements[i].OnEnterTransitionDidFinish();
                }
            }
        }

        public virtual void OnExitTransitionDidStart()
        {
            if (this.Children != null && this.Children.Count > 0)
            {
                MMNode[] elements = this.Children.Elements;
                for (int i = 0, count = this.Children.Count; i < count; i++)
                {
                    elements[i].OnExitTransitionDidStart();
                }
            }
        }

        public virtual void OnExit()
        {
            this.Pause();

            this.IsRunning = false;

            if (this.Children != null && this.Children.Count > 0)
            {
                MMNode[] elements = this.Children.Elements;
                for (int i = 0, count = this.Children.Count; i < count; i++)
                {
                    elements[i].OnExit();
                }
            }
        }

        #endregion Entering and exiting


        #region Actions

        internal void AttachActions()
        {
            if (this.toBeAddedActions != null && this.toBeAddedActions.Count > 0)
            {
                var actionManger = this.ActionManager;
                foreach (var action in this.toBeAddedActions)
                {
                    this.ActionManager.AddAction(action.Action, action.Target, action.Paused);
                }

                this.toBeAddedActions.Clear();
                this.toBeAddedActions = null;
            }
        }

        MMActionState AddLazyAction(MMAction action, MMNode target, bool paused = false)
        {
            if (this.toBeAddedActions == null)
                this.toBeAddedActions = new List<LazyAction>();

            this.toBeAddedActions.Add(new LazyAction(action, target, paused));
            return null;
        }

        public void AddAction(MMAction action, bool paused = false)
        {
            if (this.ActionManager != null)
                this.ActionManager.AddAction(action, this, paused);
            else
                this.AddLazyAction(action, this, paused);
        }

        public void AddActions(bool paused, params MMFiniteTimeAction[] actions)
        {
            if (this.ActionManager != null)
                this.ActionManager.AddAction(new MMSequence(actions), this, paused);
            else
                this.AddLazyAction(new MMSequence(actions), this, paused);
        }

        public MMActionState Repeat(uint times, params MMFiniteTimeAction[] actions)
        {
            return RunAction(new MMRepeat(new MMSequence(actions), times));
        }

        public MMActionState Repeat(uint times, MMFiniteTimeAction action)
        {
            return RunAction(new MMRepeat(action, times));
        }

        public MMActionState RepeatForever(params MMFiniteTimeAction[] actions)
        {
            return RunAction(new MMRepeatForever(actions));
        }

        public MMActionState RepeatForever(MMFiniteTimeAction action)
        {
            return RunAction(new MMRepeatForever(action) { Tag = action.Tag });
        }

        public MMActionState RunAction(MMAction action)
        {
            Debug.Assert(action != null, "Argument must be non-nil");

            return this.ActionManager != null ? this.ActionManager.AddAction(action, this, !this.IsRunning) : this.AddLazyAction(action, this, !this.IsRunning);
        }

        /// <summary>
        /// Runs an Action so that it can be awaited.
        /// </summary>
        /// <param name="action">An instance of a MMFiniteTimeAction object.</param>
        public Task<MMActionState> RunActionAsync(MMFiniteTimeAction action)
        {

            Debug.Assert(action != null, "Argument must be non-nil");

            var tcs = new TaskCompletionSource<MMActionState>();

            MMActionState state = null;
            var asyncAction = new MMSequence(action, new MMCallFunc(() => tcs.TrySetResult(state)));

            state = this.ActionManager != null ? this.ActionManager.AddAction(asyncAction, this, !this.IsRunning) : this.AddLazyAction(asyncAction, this, !this.IsRunning);

            return tcs.Task;
        }

        public MMActionState RunActions(params MMFiniteTimeAction[] actions)
        {
            Debug.Assert(actions != null, "Argument must be non-nil");
            Debug.Assert(actions.Length > 0, "Paremeter: actions has length of zero. At least one action must be set to run.");
            var action = actions.Length > 1 ? new MMSequence(actions) : actions[0];

            return this.ActionManager != null ? this.ActionManager.AddAction(action, this, !this.IsRunning) : this.AddLazyAction(action, this, !this.IsRunning);
        }

        /// <summary>
        /// Runs a sequence of Actions so that it can be awaited.
        /// </summary>
        /// <param name="actions">An array of MMFiniteTimeAction objects.</param>
        public Task<MMActionState> RunActionsAsync(params MMFiniteTimeAction[] actions)
        {
            Debug.Assert(actions != null, "Argument must be non-nil");
            Debug.Assert(actions.Length > 0, "Paremeter: actions has length of zero. At least one action must be set to run.");

            var tcs = new TaskCompletionSource<MMActionState>();

            var numActions = actions.Length;
            var asyncActions = new MMFiniteTimeAction[actions.Length + 1];
            Array.Copy(actions, asyncActions, numActions);

            MMActionState state = null;
            asyncActions[numActions] = new MMCallFunc(() => tcs.TrySetResult(state));

            var asyncAction = asyncActions.Length > 1 ? new MMSequence(asyncActions) : asyncActions[0];

            state = this.ActionManager != null ? this.ActionManager.AddAction(asyncAction, this, !this.IsRunning) : this.AddLazyAction(asyncAction, this, !this.IsRunning);

            return tcs.Task;
        }

        public void StopAllActions()
        {
            if (this.ActionManager != null)
                this.ActionManager.RemoveAllActionsFromTarget(this);
        }

        public void StopAction(MMActionState actionState)
        {
            if (this.ActionManager != null)
                this.ActionManager.RemoveAction(actionState);
        }

        public void StopAction(int tag)
        {
            Debug.Assert(tag != (int)MMNodeTag.Invalid, "Invalid tag");
            this.ActionManager.RemoveAction(tag, this);
        }

        public MMAction GetAction(int tag)
        {
            Debug.Assert(tag != (int)MMNodeTag.Invalid, "Invalid tag");
            return this.ActionManager.GetAction(tag, this);
        }

        public MMActionState GetActionState(int tag)
        {
            Debug.Assert(tag != (int)MMNodeTag.Invalid, "Invalid tag");
            return this.ActionManager.GetActionState(tag, this);
        }

        #endregion Actions


        #region Scheduling

        internal void AttachSchedules()
        {
            if (this.toBeAddedSchedules != null && this.toBeAddedSchedules.Count > 0)
            {
                var scheduler = this.Scheduler;
                foreach (var schedule in this.toBeAddedSchedules)
                {
                    if (schedule.IsPriority)
                        scheduler.Schedule(schedule.Target, schedule.Priority, schedule.Paused);
                    else
                        scheduler.Schedule(schedule.Selector, schedule.Target, schedule.Interval, schedule.Repeat, schedule.Delay, schedule.Paused);
                }

                this.toBeAddedSchedules.Clear();
                this.toBeAddedSchedules = null;
            }
        }

        void AddLazySchedule(Action<float> selector, ICCUpdatable target, float interval, uint repeat, float delay, bool paused)
        {
            if (this.toBeAddedSchedules == null)
                this.toBeAddedSchedules = new List<LazySchedule>();

            this.toBeAddedSchedules.Add(new LazySchedule(selector, target, interval, repeat, delay, paused));
        }

        void AddLazySchedule(ICCUpdatable target, int priority, bool paused)
        {
            if (this.toBeAddedSchedules == null)
                this.toBeAddedSchedules = new List<LazySchedule>();

            this.toBeAddedSchedules.Add(new LazySchedule(target, priority, paused));
        }

        public void Schedule()
        {
            this.Schedule(0);
        }

        public void Schedule(int priority)
        {
            if (this.Scheduler != null)
                this.Scheduler.Schedule(this, priority, !this.IsRunning);
            else
                this.AddLazySchedule(this, priority, !this.IsRunning);
        }

        public void Unschedule()
        {
            this.Scheduler.Unschedule(this);
        }

        public void Schedule(Action<float> selector)
        {
            this.Schedule(selector, 0.0f, MMSchedulePriority.RepeatForever, 0.0f);
        }

        public void Schedule(Action<float> selector, float interval)
        {
            this.Schedule(selector, interval, MMSchedulePriority.RepeatForever, 0.0f);
        }

        public void Schedule(Action<float> selector, float interval, uint repeat, float delay)
        {
            Debug.Assert(selector != null, "Argument must be non-nil");
            Debug.Assert(interval >= 0, "Argument must be positive");

            if (this.Scheduler != null)
                this.Scheduler.Schedule(selector, this, interval, repeat, delay, !this.IsRunning);
            else
                this.AddLazySchedule(selector, this, interval, repeat, delay, !this.IsRunning);
        }

        public void ScheduleOnce(Action<float> selector, float delay)
        {
            this.Schedule(selector, 0.0f, 0, delay);
        }

        public void Unschedule(Action<float> selector)
        {
            // explicit nil handling
            if (selector == null)
                return;

            if (this.Scheduler == null)
            {
                if (this.toBeAddedSchedules != null && this.toBeAddedSchedules.Count > 0)
                {
                    var safeDelete = new List<LazySchedule>();
                    foreach (var schedule in this.toBeAddedSchedules)
                    {
                        if (schedule.Selector == selector && schedule.Target == this)
                            safeDelete.Add(schedule);
                    }
                    foreach (var safeSchedule in safeDelete)
                    {
                        this.toBeAddedSchedules.Remove(safeSchedule);
                    }
                }
            }
            else
                this.Scheduler.Unschedule(selector, this);
        }

        public void UnscheduleAll()
        {
            if (this.Scheduler != null)
                this.Scheduler.UnscheduleAll(this);
        }

        public void Resume()
        {
            if (this.Scheduler != null)
                this.Scheduler.Resume(this);
            if (this.ActionManager != null)
                this.ActionManager.ResumeTarget(this);
            if (this.EventDispatcher != null)
                this.EventDispatcher.Resume(this);
        }

        public void Pause()
        {
            if (this.Scheduler != null)
                this.Scheduler.PauseTarget(this);
            if (this.ActionManager != null)
                this.ActionManager.PauseTarget(this);
            if (this.EventDispatcher != null)
                this.EventDispatcher.Pause(this);
        }

        #endregion Scheduling


        #region Transformations

        protected virtual void ParentUpdatedTransform()
        {
            if (this.Children != null)
            {
                foreach (MMNode child in this.Children)
                {
                    child.ParentUpdatedTransform();
                }
            }
        }

        protected virtual void UpdateTransform()
        {
            // Translate values
            float x = this.position.X;
            float y = this.position.Y;

            this.affineLocalTransform = MMAffineTransform.Identity;

            if (this.ignoreAnchorPointForPosition)
            {
                x += this.anchorPointInPoints.X;
                y += this.anchorPointInPoints.Y;
            }

            // Rotation values
            // Change rotation code to handle X and Y
            // If we skew with the exact same value for both x and y then we're simply just rotating
            float cx = 1, sx = 0, cy = 1, sy = 0;
            if (this.rotationX != 0 || this.rotationY != 0)
            {
                float radiansX = -MMMacros.MMDegreesToRadians(this.rotationX);
                float radiansY = -MMMacros.MMDegreesToRadians(this.rotationY);
                cx = (float)Math.Cos(radiansX);
                sx = (float)Math.Sin(radiansX);
                cy = (float)Math.Cos(radiansY);
                sy = (float)Math.Sin(radiansY);
            }

            bool needsSkewMatrix = (this.skewX != 0f || this.skewY != 0f);

            // optimization:
            // inline anchor point calculation if skew is not needed
            if (!needsSkewMatrix && !this.anchorPointInPoints.Equals(MMPoint.Zero))
            {
                x += cy * -this.anchorPointInPoints.X * this.scaleX + -sx * -this.anchorPointInPoints.Y * this.scaleY;
                y += sy * -this.anchorPointInPoints.X * this.scaleX + cx * -this.anchorPointInPoints.Y * this.scaleY;
            }

            // Build Transform Matrix
            // Adjusted transform calculation for rotational skew
            this.affineLocalTransform.A = cy * this.scaleX;
            this.affineLocalTransform.B = sy * this.scaleX;
            this.affineLocalTransform.C = -sx * this.scaleY;
            this.affineLocalTransform.D = cx * this.scaleY;
            this.affineLocalTransform.Tx = x;
            this.affineLocalTransform.Ty = y;
            this.affineLocalTransform.Tz = this.VertexZ;

            // If skew is needed, apply skew and then anchor point
            if (needsSkewMatrix)
            {
                var skewMatrix = new MMAffineTransform(
                    1.0f, (float)Math.Tan(MMMacros.MMDegreesToRadians(this.skewY)),
                    (float)Math.Tan(MMMacros.MMDegreesToRadians(this.skewX)), 1.0f,
                    0.0f, 0.0f);

                MMAffineTransform.Concat(ref skewMatrix, ref this.affineLocalTransform, out this.affineLocalTransform);

                // adjust anchor point
                if (!this.anchorPointInPoints.Equals(MMPoint.Zero))
                {
                    this.affineLocalTransform = MMAffineTransform.Translate(this.affineLocalTransform,
                        -this.anchorPointInPoints.X,
                        -this.anchorPointInPoints.Y);
                }
            }

            MMAffineTransform.Concat(ref this.additionalTransform, ref this.affineLocalTransform, out this.affineLocalTransform);

            Matrix fauxLocalCameraTransform = Matrix.Identity;

            if (this.FauxLocalCameraCenter != this.FauxLocalCameraTarget)
            {
                fauxLocalCameraTransform = Matrix.CreateLookAt(
                    new Vector3(this.FauxLocalCameraCenter.X, this.FauxLocalCameraCenter.Y, this.FauxLocalCameraCenter.Z),
                    new Vector3(this.FauxLocalCameraTarget.X, this.FauxLocalCameraTarget.Y, this.FauxLocalCameraTarget.Z),
                    new Vector3(this.FauxLocalCameraUpDirection.X, this.FauxLocalCameraUpDirection.Y, this.FauxLocalCameraUpDirection.Z));
                fauxLocalCameraTransform.M41 += this.AnchorPointInPoints.X;
                fauxLocalCameraTransform.M42 += this.AnchorPointInPoints.Y;
            }

            var affineCameraTrans = new MMAffineTransform(fauxLocalCameraTransform);
            MMAffineTransform.Concat(ref affineCameraTrans, ref this.affineLocalTransform, out this.affineLocalTransform);

            if (this.Children != null)
            {
                foreach (MMNode child in this.Children)
                {
                    child.ParentUpdatedTransform();
                }
            }

            this.transformIsDirty = false;
        }

        #endregion Transformations


        public virtual void KeyBackClicked()
        {
        }

        public virtual void KeyMenuClicked()
        {
        }
    }
}