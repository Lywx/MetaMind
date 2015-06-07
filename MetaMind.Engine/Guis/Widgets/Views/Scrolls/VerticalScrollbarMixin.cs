namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using System;
    using Logic;

    public static class VerticalScrollbarMixin
    {
        private static bool actionRecursiveLocked;

        private static readonly Action<Action> Act = (action) =>
        {
            if (!actionRecursiveLocked)
            {
                actionRecursiveLocked = true;
                action();
            }

            actionRecursiveLocked = false;
        };

        public static void ScrollUp(this IPointViewVerticalLogic viewLogic, ViewVerticalScrollbar viewScrollbar)
        {
            Act(() =>
                {
                    viewScrollbar.Trigger();
                    viewLogic    .ScrollUp();
                });
        }

        public static void ScrollDown(this IPointViewVerticalLogic viewLogic, ViewVerticalScrollbar viewScrollbar)
        {
            Act(() =>
                {
                    viewScrollbar.Trigger();
                    viewLogic    .ScrollDown();
                });
        }

        public static void MoveUp(this IPointViewVerticalLogic viewLogic, ViewVerticalScrollbar viewScrollbar)
        {
            Act(() =>
                {
                    viewScrollbar.Trigger();
                    viewLogic    .MoveUp();
                });
        }

        public static void MoveDown(this IPointViewVerticalLogic viewLogic, ViewVerticalScrollbar viewScrollbar)
        {
            Act(() =>
                {
                    viewScrollbar.Trigger();
                    viewLogic    .MoveDown();
                });
        }
    }
}