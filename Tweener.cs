using System;
using System.Collections.Generic;
using WB.Pool;

namespace WB.Tweener
{
    public class Tweener<T> : ITweenerUpdater, ITweenerForSequence, IPoolObject
    {
        public Tweener()
        {
            joinedTweeners = new List<ITweener>();
        }
        private int ID;
        private Action ReturnTweenerMethod { get; set; }

        public void SetReturnMethod(Action returnMethod) => ReturnTweenerMethod = returnMethod;
        
        
        private Action onCompleted;
        
        
        
        
        

        private Func<T> ValueGetter { get; set; }
        private Action<T> ValueSetter { get; set; }
        private T EndValue { get; set; }
        private float Duration { get; set; }
        private float CurrentTime { get; set; }
        private Ease Ease { get; set; }
        private T StartValue { get; set; }
        private T ChangeValue { get; set; }
        private Func<bool> CheckTargetExist { get; set; }

        public void SetId(int id)
        {
            ID = id;
        }

        public void Initialize()
        {
            ValueGetter = null;
            ValueSetter = null;
            EndValue = default;
            Duration = 0;
            CurrentTime = 0;

            IsRunning = false;
            Ease = Ease.Liner;
            StartValue = default;
            ChangeValue = default;
            CheckTargetExist = null;
            onCompleted = null;
            nextTweener = null;
            joinedTweeners.Clear();
        }



        public void AddOnCompleted(Action method)
        {
            if (onCompleted == null)
                onCompleted = method;
            else
                onCompleted += method;
        }

        public bool IsRunning { get; private set; }


        public void Play()
        {
            if (CheckTargetExist())
            {
                IsRunning = true;
                CurrentTime = 0;
                StartValue = ValueGetter();
                ChangeValue = ABSTweenPluginUtility.I.Resolve<T>().EvaluateChangeValue(StartValue, EndValue);
                TweenerManager.I.AddTweener(this);
            }
            else
            {
                Kill();
            }

            joinedTweeners.ForEach(t => t.Play());
        }

        public void Sleep()
        {
            IsRunning = false;
        }

        public ITweener SetEase(Ease ease)
        {
            Ease = ease;
            return this;
        }

        public virtual void Update(float deltaTime)
        {
            if (CheckTargetExist())
            {
                CurrentTime += deltaTime;
                var progressRatio = CurrentTime / Duration;
                if (progressRatio < 1)
                {
                    ABSTweenPluginUtility.I.Resolve<T>()
                        .EvaluateAndApply(ValueSetter, StartValue, ChangeValue, progressRatio, Ease);
                }
                else
                {
                    ABSTweenPluginUtility.I.Resolve<T>()
                        .EvaluateAndApply(ValueSetter, StartValue, ChangeValue, 1, Ease);
                    OnCompleted();
                }
            }
            else
            {
                Kill();
            }
        }

        internal void SetParams(Func<bool> checkTargetExist, Func<T> valueGetter, Action<T> valueSetter, T endValue,
            float duration)
        {
            ValueGetter = valueGetter;
            ValueSetter = valueSetter;
            EndValue = endValue;
            Duration = duration;
            CheckTargetExist = checkTargetExist;
        }

        private void OnCompleted()
        {
            onCompleted?.Invoke();
            nextTweener?.Play();
            Kill();
        }

        public void Kill()
        {
            TweenerManager.I.CompletedTweeners.Enqueue(this);
            ReturnTweenerMethod();
        }

        #region For Sequence

        internal ITweener nextTweener;
        internal readonly List<ITweener> joinedTweeners;

        public void SetNextNode(ITweener node)
        {
            nextTweener = node;
        }

        public void AddJoinNode(ITweener node)
        {
            joinedTweeners.Add(node);
        }

        #endregion
    }
}