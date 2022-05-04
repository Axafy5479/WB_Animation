using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class MoveAnimX : MonoTweener<Transform>
    {
        private float XChange { get; set; }
        private bool LocalPos { get; }
        private float StartX { get; set; }
        private float MoveTo { get; }

        public MoveAnimX(Transform trn, float moveTo, float animTime, bool localPos) : base(trn,null, animTime)
        {
            MoveTo = moveTo;
            LocalPos = localPos;
        }

        protected override void __start(Transform trn)
        {
            StartX = LocalPos ? trn.localPosition.x : trn.position.x;
            XChange = LocalPos ? MoveTo - trn.localPosition.x : MoveTo - trn.position.x;

        }

        protected override void __update(Transform trn,float progressRatio)
        {
            if (LocalPos)
            {
                trn.localPosition = new Vector3(StartX + XChange * progressRatio, trn.localPosition.y, trn.localPosition.z);
            }
            else
            {
                trn.position = new Vector3(StartX + XChange * progressRatio, trn.position.y, trn.position.z);
            }
        }

    }
}
