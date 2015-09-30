// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Engine.Actions.Intervals
{
    using Action;

    public class MoveByAction : GameFiniteTimeAction
    {
        #region Constructors

        public MoveByAction(float duration, GamePoint position) : base(duration)
        {
            PositionDelta = position;
        }

        #endregion Constructors

        public GamePoint PositionDelta { get; private set; }

        protected internal override GameActionState StartAction(GameActor target)
        {
            return new MoveByActionState(this, target);
        }

        public override GameFiniteTimeAction Reverse()
        {
            return new MoveByAction(Duration, new GamePoint(-PositionDelta.X, -PositionDelta.Y));
        }
    }

    public class MoveByActionState : GameFiniteTimeActionState
    {
        protected GamePoint PositionDelta;
        protected GamePoint EndPosition;
        protected GamePoint StartPosition;
        protected GamePoint PreviousPosition;

        public MoveByActionState(MoveByAction action, GameActor target)
            : base(action, target)
        {
            PositionDelta = action.PositionDelta;
            PreviousPosition = StartPosition = target.Position;
        }

        public override void Update(float time)
        {
            if (Target == null)
                return;

            var currentPos = Target.Position;
            var diff = currentPos - PreviousPosition;
            StartPosition = StartPosition + diff;
            GamePoint newPos = StartPosition + PositionDelta * time;
            Target.Position = newPos;
            PreviousPosition = newPos;
        }
    }
}
