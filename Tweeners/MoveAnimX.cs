using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class MoveAnimX : Tweener
    {
        private Transform Trn { get; set; }
        private float XChange { get; set; }
        private bool LocalPos { get; set; }
        private float StartX { get; set; }
        private float MoveTo { get; }

        public MoveAnimX(Transform trn, float moveTo, float animTime, bool localPos) : base(null, animTime)
        {
            MoveTo = moveTo;
            LocalPos = localPos;
            Trn = trn;
            OnEnd = () => _update(1);
        }

        public override void Start()
        {
            StartX = LocalPos ? Trn.localPosition.x : Trn.position.x;
            XChange = LocalPos ? MoveTo - Trn.localPosition.x : MoveTo - Trn.position.x;

        }

        protected override void _update(float progressRatio)
        {
            Vector3 pos = new Vector3(StartX + XChange * progressRatio, Trn.position.y, Trn.position.z);

            if (LocalPos)
            {
                Trn.localPosition = pos;
            }
            else
            {
                Trn.position = pos;
            }
        }

    }
}
