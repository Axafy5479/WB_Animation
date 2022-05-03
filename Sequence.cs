using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class Sequence
    {
        public List<(Tweener tweener, bool isAppended)> Methods { get; }
            = new List<(Tweener tweener, bool isAppended)>();


        public Sequence Append(Tweener tweener)
        {
            if (Methods.Count > 0)
            {
                Methods[Methods.Count - 1].tweener.NextTween = tweener;
            }

            Methods.Add((tweener, true));
            return this;
        }

        public Sequence Join(Tweener tweener)
        {
            Tweener lastAppended = null;

            for (int i = Methods.Count - 1; i >= 0; i--)
            {
                if (Methods[i].isAppended)
                {
                    lastAppended = Methods[i].tweener;
                    break;
                }
            }

            if (lastAppended != null)
            {
                lastAppended.JoinedTweeners.Add(tweener);
                Methods.Add((tweener, false));
            }
            else
            {
                Append(tweener);
            }

            return this;
        }

        public void Play()
        {
            AnimationManager.I.AddSequence(this);
        }

        public void Kill()
        {
            foreach (var tweener in Methods)
            {
                tweener.tweener.Kill();
            }
        }
    }
}
