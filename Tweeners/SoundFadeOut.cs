using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    public class SoundFadeOut : MonoTweener<AudioSource>
    {
        private float InitialVolume { get; set; }

        public SoundFadeOut(AudioSource audioSource, Action onEnd, float animTime) : base(audioSource, onEnd, animTime)
        {
        }

        protected override void __start(AudioSource audioSource)
        {
            InitialVolume = audioSource.volume;
        }

        protected override void __update(AudioSource audioSource, float progressRatio)
        {
            audioSource.volume = InitialVolume * (1 - progressRatio);
        }
    }
}
