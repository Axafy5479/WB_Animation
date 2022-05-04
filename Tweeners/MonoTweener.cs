using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public abstract class MonoTweener<T> : Tweener where T : Component
    {
        private T animationTarget;

        private T AnimationTarget
        {
            get
            {
                if (animationTarget == null)
                {
                    Debug.LogWarning($"アニメーション({this.GetType()})の対象は存在していない、または削除されています");
                    Kill();
                }

                return animationTarget;
            }
        }

        protected MonoTweener(T animationTarget, Action onEnd, float animTime) : base(onEnd, animTime)
        {
            this.animationTarget = animationTarget;
        }

        public override void Start()
        {
            if (AnimationTarget == null) return;
            __start(AnimationTarget);
        }



        protected override void _update(float progressRatio)
        {
            if (AnimationTarget == null) return;
            __update(AnimationTarget, progressRatio);
        }

        protected virtual void __start(T animTarget)
        {
        }

        protected abstract void __update(T animTarget, float progressRatio);
    }

}
