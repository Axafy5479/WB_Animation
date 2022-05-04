using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class MoveAnimZ : MonoTweener<Transform>
    {
        private float ZChange { get; set; }
        private bool LocalPos { get; set; }
        private float StartZ { get; set; }
        private float MoveTo { get; }

        public MoveAnimZ(Transform trn, float moveTo, float animTime, bool localPos) : base(trn,null, animTime)
        {
            MoveTo = moveTo;
            LocalPos = localPos;
        }

        protected override void __start(Transform trn)
        {
            StartZ = LocalPos ? trn.localPosition.z : trn.position.z;
            ZChange = LocalPos ? MoveTo - trn.localPosition.z : MoveTo - trn.position.z;

        }

        protected override void __update(Transform trn,float progressRatio)
        {
            if (LocalPos)
            {
                trn.localPosition = new Vector3(trn.localPosition.x,trn.localPosition.y, StartZ+ZChange * progressRatio);
            }
            else
            {
                trn.position = new Vector3(trn.position.x,trn.position.y, StartZ+ZChange * progressRatio);
            }
        }

    }
}