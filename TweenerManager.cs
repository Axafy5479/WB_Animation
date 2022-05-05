using System.Collections.Generic;

namespace WB.Tweener
{
    internal class TweenerManager
    {
        private readonly List<ITweenerUpdater> tweeners;

        internal List<ITweenerUpdater> Tweeners
        {
            get
            {
                while (CompletedTweeners.Count > 0) tweeners.Remove(CompletedTweeners.Dequeue());

                while (AddingTweeners.Count > 0) tweeners.Add(AddingTweeners.Dequeue());

                return tweeners;
            }
        }

        internal Queue<ITweenerUpdater> CompletedTweeners { get; }
        private Queue<ITweenerUpdater> AddingTweeners { get; }

        internal void AddTweener(ITweenerUpdater tweener)
        {
            AddingTweeners.Enqueue(tweener);
        }

        #region Singleton

        private static TweenerManager instance;
        internal static TweenerManager I => instance ??= new TweenerManager();

        private TweenerManager()
        {
            tweeners = new List<ITweenerUpdater>();
            CompletedTweeners = new Queue<ITweenerUpdater>();
            AddingTweeners = new Queue<ITweenerUpdater>();
        }

        #endregion
    }
}