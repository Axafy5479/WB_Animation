using System;
using UnityEngine;
using UnityEngine.UI;

namespace WB.Tweener
{
    public static class WBTween
    {
        public static ITweener Anim(Func<float> valueGetter, Action<float> valueSetter, float endValue, float duration)
        {
            var t = TweenerPool.I.floatTweenerPool.Rent();
            t.SetParams(() => true, valueGetter, valueSetter, endValue, duration);
            return t;
        }

        public static ITweener MoveAnim(this Transform trn, Vector3 endValue, float duration, bool localPosition)
        {
            var t = TweenerPool.I.vec3TweenerPool.Rent();
            if (localPosition)
                t.SetParams(() => trn != null, () => trn.localPosition, pos => trn.localPosition = pos, endValue,
                    duration);
            else t.SetParams(() => trn != null, () => trn.position, pos => trn.position = pos, endValue, duration);
            return t;
        }

        public static ITweener MoveAnimX(this Transform trn, float endValue, float duration, bool localPosition)
        {
            var t = TweenerPool.I.floatTweenerPool.Rent();
            if (localPosition)
                t.SetParams(() => trn != null, () => trn.localPosition.x,
                    pos => trn.localPosition = new Vector3(pos, trn.localPosition.y, trn.localPosition.z), endValue,
                    duration);
            else
                t.SetParams(() => trn != null, () => trn.position.x,
                    pos => trn.position = new Vector3(pos, trn.position.y, trn.position.z), endValue,
                    duration);
            return t;
        }

        public static ITweener MoveAnimY(this Transform trn, float endValue, float duration, bool localPosition)
        {
            var t = TweenerPool.I.floatTweenerPool.Rent();
            if (localPosition)
                t.SetParams(() => trn != null, () => trn.localPosition.y,
                    pos => trn.localPosition = new Vector3(trn.localPosition.x, pos, trn.localPosition.z), endValue,
                    duration);
            else
                t.SetParams(() => trn != null, () => trn.position.y,
                    pos => trn.position = new Vector3(trn.position.x, pos, trn.position.z), endValue,
                    duration);
            return t;
        }

        public static ITweener MoveAnimZ(this Transform trn, float endValue, float duration, bool localPosition)
        {
            var t = TweenerPool.I.floatTweenerPool.Rent();
            if (localPosition)
                t.SetParams(() => trn != null, () => trn.localPosition.z,
                    pos => trn.localPosition = new Vector3(trn.localPosition.x, trn.localPosition.y, pos), endValue,
                    duration);
            else
                t.SetParams(() => trn != null, () => trn.position.z,
                    pos => trn.position = new Vector3(trn.position.x, trn.position.y, pos), endValue,
                    duration);
            return t;
        }

        public static ITweener ColorAnim(this SpriteRenderer sr, Color endValue, float duration)
        {
            var t = TweenerPool.I.colorTweenerPool.Rent();
            t.SetParams(() => sr != null, () => sr.color, c => sr.color = c, endValue, duration);
            return t;
        }

        public static ITweener ColorAnim(this Image im, Color endValue, float duration)
        {
            var t = TweenerPool.I.colorTweenerPool.Rent();
            t.SetParams(() => im != null, () => im.color, c => im.color = c, endValue, duration);
            return t;
        }

        public static ITweener ColorAnim(this Text text, Color endValue, float duration)
        {
            var t = TweenerPool.I.colorTweenerPool.Rent();
            t.SetParams(() => text != null, () => text.color, c => text.color = c, endValue, duration);
            return t;
        }

        public static ITweener SoundFadeIn(this AudioSource source, float endValue, float duration)
        {
            var t = TweenerPool.I.floatTweenerPool.Rent();
            t.SetParams(() => source != null, () => 0, v => source.volume = v, endValue, duration);
            return t;
        }

        public static ITweener SoundFadeOut(this AudioSource source, float duration)
        {
            var t = TweenerPool.I.floatTweenerPool.Rent();
            t.SetParams(() => source != null, () => source.volume, v => source.volume = v, 0, duration);
            return t;
        }


        internal static ITweenerUpdater Delay(float t)
        {
            var tweener = TweenerPool.I.floatTweenerPool.Rent();
            tweener.SetParams(() => true, () => 0, _ => { }, t, t);
            return tweener;
        }
    }
}