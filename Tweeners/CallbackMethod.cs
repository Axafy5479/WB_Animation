using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class CallbackMethod : Tweener
    {
        public CallbackMethod(Action onEnd) : base(onEnd, 0)
        {
        }

        protected override void _update(float progressRatio)
        {
        }
    }

}

