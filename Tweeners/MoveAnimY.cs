using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class MoveAnimY : MonoTweener<Transform>
    {
        private float YChange { get; set; }
        private bool LocalPos { get; set; }
        private float StartY { get; set; }
        private float MoveTo { get; }

        public MoveAnimY(Transform trn, float moveTo, float animTime, bool localPos)  : base(trn,null, animTime)
        {
            MoveTo = moveTo;
            LocalPos = localPos;
        }

        protected override void __start(Transform trn)
        {
            if (trn == null) return;
            StartY = LocalPos ? trn.localPosition.y : trn.position.y;
            YChange = LocalPos ? MoveTo - trn.localPosition.y : MoveTo - trn.position.y;

        }

        protected override void __update(Transform trn,float progressRatio)
        {
            if (trn == null) return;
            
            if (LocalPos)
            {
                trn.localPosition = new Vector3(trn.localPosition.x, StartY + YChange * progressRatio, trn.localPosition.z);
            }
            else
            {
                trn.position = new Vector3(trn.position.x, StartY + YChange * progressRatio, trn.position.z);
            }
        }

    }
}
