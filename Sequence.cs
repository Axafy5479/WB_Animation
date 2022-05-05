using System;

namespace WB.Tweener
{
    public class Sequence
    {
        private ITweenerForSequence startNode, lastAppend, lastNode;

        public float CurrentTime { get; set; }
        public bool IsRunning { get; set; }

        private Sequence SetFirstTweener(ITweenerForSequence tweener)
        {
            var tweenerForSeq = tweener;
            startNode = tweenerForSeq;
            lastNode = tweenerForSeq;
            lastAppend = tweenerForSeq;
            return this;
        }

        public Sequence Append(ITweener tweener)
        {
            var tweenerForSeq = (ITweenerForSequence) tweener;

            if (startNode == null) return SetFirstTweener(tweenerForSeq);

            lastNode.SetNextNode(tweenerForSeq);
            lastNode = tweenerForSeq;
            lastAppend = tweenerForSeq;
            return this;
        }

        public Sequence Join(ITweener tweener)
        {
            var tweenerForSeq = (ITweenerForSequence) tweener;

            if (startNode == null) return SetFirstTweener(tweenerForSeq);

            lastNode = tweenerForSeq;
            lastAppend.AddJoinNode(tweenerForSeq);
            return this;
        }

        public Sequence Delay(float time)
        {
            Append(WBTween.Delay(time));
            return this;
        }

        public void Play()
        {
            IsRunning = true;
            startNode.Play();
        }


        public Sequence AppendCallback(Action method)
        {
            lastNode.AddOnCompleted(method);
            return this;
        }

        // private class Node
        // {
        //     public Node nextTweener;
        //     public List<Node> joinedTweeners;
        //
        //     public ITweenerUpdater Tweener { get; }
        //     public Node(ITweenerUpdater tweener)
        //     {
        //         Tweener = tweener;
        //         joinedTweeners = new List<Node>();
        //     }
        //
        //     public virtual void Play()
        //     {
        //         joinedTweeners.ForEach(t=>t.Play());
        //         Tweener.AddOnCompleted(OnEnd);
        //         Tweener.Play();
        //     }
        //
        //     public void OnEnd()
        //     {
        //         nextTweener?.Play();
        //     }
        //
        //     public void SetNextNode(Node node)
        //     {
        //         nextTweener=node;
        //     }
        //
        //     public void AddJoinNode(Node node)
        //     {
        //         joinedTweeners.Add(node);
        //     }
        //     
        // }
        //
        // private class StartNode : Node
        // {
        //     public StartNode() : base(null)
        //     {
        //     }
        //
        //     public override void Play()
        //     {
        //         OnEnd();
        //     }
        // }
        //
        //
    }
}