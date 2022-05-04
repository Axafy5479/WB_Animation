using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public abstract class Tweener
    {
        public Ease Ease { get; private set; } = Ease.Liner;
        private AnimationManager manager;
        
        public Tweener(Action onEnd, float animTime)
        {
            manager = AnimationManager.I;
            AnimTime = animTime;
            //UpdateMethod = updateMethod;
            JoinedTweeners = new List<Tweener>();
            OnEnd = () => _update(1);
            if(onEnd!=null)OnEnd += onEnd;
        }

        public void TimeInitialization(float currentTime)
        {
            StartTime = currentTime;
            AnimTime += currentTime;
        }

        public virtual void Start()
        {
        }

        public void Update(float currentTime)
        {
            CurrentTime = currentTime;
            float progressRatio = (currentTime - StartTime) / (AnimTime - StartTime);
            _update(EasingFunctions.EaseFuncMap[Ease](progressRatio));
        }

        protected abstract void _update(float progressRatio);

        public float CurrentTime { get; private set; }
        public float StartTime { get; private set; }

        public float AnimTime { get; private set; }

        //public Action<float> UpdateMethod { get; }
        public Action OnEnd { get; }
        internal Tweener NextTween { get; set; }
        public List<Tweener> JoinedTweeners { get; }

        public Tweener SetEase(Ease ease)
        {
            Ease = ease;
            return this;
        }

        public void Kill()
        {
            NextTween = null;
            if(manager!=null)manager.RemoveTweener(this);
        }
    }
}
