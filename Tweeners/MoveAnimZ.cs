using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class MoveAnimZ : Tweener
    {
        private Transform Trn { get; set; }
        private float ZChange { get; set; }
        private bool LocalPos { get; set; }
        private float StartZ { get; set; }
        private float MoveTo { get; }

        public MoveAnimZ(Transform trn, float moveTo, float animTime, bool localPos) : base(null, animTime)
        {
            MoveTo = moveTo;
            LocalPos = localPos;
            Trn = trn;
            OnEnd = () => _update(1);
        }

        public override void Start()
        {
            StartZ = LocalPos ? Trn.localPosition.z : Trn.position.z;
            ZChange = LocalPos ? MoveTo - Trn.localPosition.z : MoveTo - Trn.position.z;

        }

        protected override void _update(float progressRatio)
        {
            Vector3 pos = new Vector3(Trn.position.x,Trn.position.y, StartZ+ZChange * progressRatio);

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