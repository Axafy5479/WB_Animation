using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class GeneralAnim : Tweener
    {
        public float StartVal { get; }
        public float EndVal { get; }
        public Action<float> UpdateMethod { get; }

        public GeneralAnim(float startVal, float endVal, Action<float> updateMethod, float animTime) : base(null,
            animTime)
        {
            StartVal = startVal;
            EndVal = endVal;
            UpdateMethod = updateMethod;
        }

        protected override void _update(float progressRatio)
        {
            UpdateMethod(StartVal + progressRatio * (EndVal - StartVal));
        }
    }
}
