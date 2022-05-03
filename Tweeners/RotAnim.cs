using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class RotAnim : Tweener
    {
        protected Transform Trn { get; set; }
        protected Vector3 AngleChangeDeg { get; set; }
        protected bool LocalPos { get; set; }
        private float prevProgress = 0;

        public RotAnim(Transform trn, Vector3 angleChangeDeg, float animTime, bool localPos) : base(null, animTime)
        {
            LocalPos = localPos;
            Trn = trn;
            AngleChangeDeg = angleChangeDeg;
            OnEnd = () => _update(1);
        }

        protected override void _update(float progressRatio)
        {
            Trn.Rotate(AngleChangeDeg * (progressRatio - prevProgress));


            prevProgress = progressRatio;
        }

    }
}
