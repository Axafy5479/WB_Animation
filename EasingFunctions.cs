using System;
using System.Collections.Generic;

/*
 * (参考)
 * easings.net
 */

namespace WB.Tweener
{
    public enum Ease
    {
        Liner,
        InOutSine,
        InOutQuad,
        OutSine,
        OutQuad,
        InSine,
        InQuad,
        OutBack,
        InBack
    }

    public static class EasingFunctions
    {
        internal static Dictionary<Ease, Func<float, float>> EaseFuncMap = new Dictionary<Ease, Func<float, float>>
        {
            {Ease.Liner, Liner},
            {Ease.InOutSine, InOutSin},
            {Ease.InOutQuad, InOutQuad},
            {Ease.InSine, InSine},
            {Ease.InQuad, InQuad},
            {Ease.OutSine, OutSine},
            {Ease.OutQuad, OutQuad},
            {Ease.OutBack, OutBack},
            {Ease.InBack, InBack}
        };

        private static float Liner(float progressRatio)
        {
            return progressRatio;
        }

        private static float InOutSin(float progressRatio)
        {
            return (1f - (float) Math.Cos(Math.PI * progressRatio)) / 2f;
        }

        private static float InOutQuad(float progressRatio)
        {
            return progressRatio < 0.5f
                ? 2 * progressRatio * progressRatio
                : -2 * (progressRatio - 1) * (progressRatio - 1) + 1;
        }

        private static float OutSine(float progressRatio)
        {
            return (float) Math.Sin(progressRatio * Math.PI / 2);
        }

        private static float OutQuad(float progressRatio)
        {
            return 1 - (progressRatio - 1) * (progressRatio - 1);
        }

        private static float InSine(float progressRatio)
        {
            return (float) (1 - Math.Cos(progressRatio * Math.PI / 2));
        }

        private static float InQuad(float progressRatio)
        {
            return progressRatio * progressRatio;
        }

        private static float OutBack(float progressRatio)
        {
            var c1 = 1.70158f;
            var c3 = c1 + 1;
            return (float) (1 + c3 * Math.Pow(progressRatio - 1, 3) + c1 * Math.Pow(progressRatio - 1, 2));
        }

        private static float InBack(float progressRatio)
        {
            var c1 = 1.70158f;
            var c3 = c1 + 1;
            return c3 * progressRatio * progressRatio * progressRatio - c1 * progressRatio * progressRatio;
        }
    }
}