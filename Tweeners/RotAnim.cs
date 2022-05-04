using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class RotAnim : Tweener
    {
        private Transform trn;

        private Transform Trn
        {
            get
            {
                if (trn == null)
                {
                    Debug.LogWarning("移動対象は存在していない、または削除されています");
                    Kill();
                }

                return trn;
            }
        }
        protected Vector3 AngleChangeDeg { get; set; }
        protected bool LocalPos { get; set; }
        private float prevProgress = 0;

        public RotAnim(Transform trn, Vector3 angleChangeDeg, float animTime, bool localPos) : base(null, animTime)
        {
            LocalPos = localPos;
            this.trn = trn;
            AngleChangeDeg = angleChangeDeg;
        }

        protected override void _update(float progressRatio)
        {
            if (Trn == null) return;
            Trn.Rotate(AngleChangeDeg * (progressRatio - prevProgress));


            prevProgress = progressRatio;
        }

    }
}
