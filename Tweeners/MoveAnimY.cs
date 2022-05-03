using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class MoveAnimY : Tweener
    {
        private Transform Trn { get; set; }
        private float YChange { get; set; }
        private bool LocalPos { get; set; }
        private float StartY { get; set; }
        private float MoveTo { get; }

        public MoveAnimY(Transform trn, float moveTo, float animTime, bool localPos) : base(null, animTime)
        {
            MoveTo = moveTo;
            LocalPos = localPos;
            Trn = trn;
            OnEnd = () => _update(1);
        }

        public override void Start()
        {
            StartY = LocalPos ? Trn.localPosition.y : Trn.position.y;
            YChange = LocalPos ? MoveTo - Trn.localPosition.y : MoveTo - Trn.position.y;

        }

        protected override void _update(float progressRatio)
        {
            Vector3 pos = new Vector3(Trn.position.x, StartY + YChange * progressRatio, Trn.position.z);

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
