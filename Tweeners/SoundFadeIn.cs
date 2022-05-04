using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class SoundFadeIn : MonoTweener<AudioSource>
    {
        private float VolumeTo { get; set; }

        public SoundFadeIn(AudioSource audioSource, float volumeTo, float animTime, Action onEnd = null) : base(
            audioSource, onEnd, animTime)
        {
            VolumeTo = volumeTo;
        }

        protected override void __update(AudioSource audioSource, float progressRatio)
        {
            audioSource.volume = VolumeTo * progressRatio;
        }
    }
}
