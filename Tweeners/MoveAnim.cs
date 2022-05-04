using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class MoveAnim :  MonoTweener<Transform>
    {
        private Vector3 PosChange { get; set; }
        private bool LocalPos { get; set; }
        private Vector3 StartPos { get; set; }
        private Vector3 MoveTo { get; }

        public MoveAnim(Transform trn, Vector3 moveTo, float animTime, bool localPos) : base(trn,null, animTime)
        {
            MoveTo = moveTo;
            LocalPos = localPos;
        }

        protected override void __start(Transform trn)
        {
            StartPos = LocalPos ? trn.localPosition : trn.position;
            PosChange = LocalPos ? MoveTo - trn.localPosition : MoveTo - trn.position;

        }

        protected override void __update(Transform trn,float progressRatio)
        {
            if (LocalPos)
            {
                trn.localPosition = StartPos + PosChange * progressRatio;
            }
            else
            {
                trn.position = StartPos + PosChange * progressRatio;
            }
        }

    }
}
