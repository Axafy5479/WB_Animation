using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class MoveAnim : Tweener
    {
        protected Transform Trn { get; set; }
        protected Vector3 PosChange { get; set; }
        protected bool LocalPos { get; set; }
        protected Vector3 StartPos { get; set; }
        private Vector3 MoveTo { get; }

        public MoveAnim(Transform trn, Vector3 moveTo, float animTime, bool localPos) : base(null, animTime)
        {
            MoveTo = moveTo;
            LocalPos = localPos;
            Trn = trn;
            OnEnd = () => _update(1);
        }

        public override void Start()
        {
            StartPos = LocalPos ? Trn.localPosition : Trn.position;
            PosChange = LocalPos ? MoveTo - Trn.localPosition : MoveTo - Trn.position;

        }

        protected override void _update(float progressRatio)
        {
            if (LocalPos)
            {
                Trn.localPosition = StartPos + PosChange * progressRatio;
            }
            else
            {
                Trn.position = StartPos + PosChange * progressRatio;
            }
        }

    }
}
