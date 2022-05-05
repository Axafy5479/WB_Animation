using System;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Tweener
{
    internal class ABSTweenPluginUtility
    {
        private ABSTweenPluginUtility()
        {
            ABSMap = new Dictionary<Type, object>();
            ABSMap[typeof(float)] = new TweenFloat();
            ABSMap[typeof(Vector2)] = new TweenVector2();
            ABSMap[typeof(Vector3)] = new TweenVector3();
            ABSMap[typeof(Color)] = new TweenColor();
        }


        private Dictionary<Type, object> ABSMap { get; }

        internal ABSTweenPlugin<T> Resolve<T>()
        {
            return (ABSTweenPlugin<T>) ABSMap[typeof(T)];
        }

        private class TweenFloat : ABSTweenPlugin<float>
        {
            public override void EvaluateAndApply(Action<float> setter, float startValue, float changeValue,
                float progressRatio, Ease ease)
            {
                setter(startValue + changeValue * EasingFunctions.EaseFuncMap[ease](progressRatio));
            }

            public override float EvaluateChangeValue(float startValue, float endValue)
            {
                return endValue - startValue;
            }
        }

        private class TweenVector3 : ABSTweenPlugin<Vector3>
        {
            public override void EvaluateAndApply(Action<Vector3> setter, Vector3 startValue, Vector3 changeValue,
                float progressRatio, Ease ease)
            {
                setter(startValue + changeValue * EasingFunctions.EaseFuncMap[ease](progressRatio));
            }

            public override Vector3 EvaluateChangeValue(Vector3 startValue, Vector3 endValue)
            {
                return endValue - startValue;
            }
        }

        private class TweenVector2 : ABSTweenPlugin<Vector2>
        {
            public override void EvaluateAndApply(Action<Vector2> setter, Vector2 startValue, Vector2 changeValue,
                float progressRatio, Ease ease)
            {
                setter(startValue + changeValue * EasingFunctions.EaseFuncMap[ease](progressRatio));
            }

            public override Vector2 EvaluateChangeValue(Vector2 startValue, Vector2 endValue)
            {
                return endValue - startValue;
            }
        }

        private class TweenColor : ABSTweenPlugin<Color>
        {
            public override void EvaluateAndApply(Action<Color> setter, Color startValue, Color changeValue,
                float progressRatio, Ease ease)
            {
                setter(startValue + changeValue * EasingFunctions.EaseFuncMap[ease](progressRatio));
            }

            public override Color EvaluateChangeValue(Color startValue, Color endValue)
            {
                return endValue - startValue;
            }
        }

        #region Singleton

        private static ABSTweenPluginUtility instance;
        internal static ABSTweenPluginUtility I => instance ??= new ABSTweenPluginUtility();

        #endregion
    }

    internal abstract class ABSTweenPlugin<T>
    {
        public abstract void EvaluateAndApply
        (
            Action<T> setter,
            T startValue,
            T changeValue,
            float progressRatio,
            Ease ease
        );

        public abstract T EvaluateChangeValue
        (
            T startValue,
            T endValue
        );
    }
}