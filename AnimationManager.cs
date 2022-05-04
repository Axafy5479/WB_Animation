using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Animation
{
    internal class AnimationManager : MonoBehaviour
    {
        #region Singleton

        private static AnimationManager instance;
        public static AnimationManager I
        {
            get
            {
                AnimationManager[] instances = null;
                if (instance == null)
                {
                    instances = FindObjectsOfType<AnimationManager>();
                    if (instances.Length == 0)
                    {
                        GameObject go = new GameObject("WB_AnimationManager");
                        DontDestroyOnLoad(go);
                        Debug.Log("生成!");
                        return go.AddComponent<AnimationManager>();
                    }
                    else if (instances.Length > 1)
                    {
                        Debug.LogError("UpdateManagerのインスタンスが複数存在します");
                    }
                    else
                    {
                        instance = instances[0];
                    }
                }

                return instance;
            }
        }

        #endregion


        #region Internal Methods

        internal void AddSequence(Sequence sequence)
        {
            AddTween(sequence.Methods[0].tweener);
        }

        internal bool RemoveTweener(Tweener tweener) => Tweeners.Remove(tweener);

        #endregion


        #region Private Methods

        private float CurrentTime { get; set; } = 0;
        private List<Tweener> Tweeners { get; } = new List<Tweener>();

        private void AddNextTweens(Tweener prevTween)
        {
            if (prevTween.NextTween != null)
            {
                AddTween(prevTween.NextTween);
            }
        }

        private void AddTween(Tweener tweener)
        {
            if (tweener is CallbackMethod callback)
            {
                callback.OnEnd();
                AddNextTweens(callback);
            }
            else
            {
                tweener.TimeInitialization(CurrentTime);
                Tweeners.Add(tweener);
                tweener.Start();
            }

            tweener.JoinedTweeners.ForEach(AddTween);
        }

        private void Update()
        {
            CurrentTime += Time.deltaTime;
            ExecuteUpdatingMethods();
        }

        private void ExecuteUpdatingMethods()
        {
            int index = 0;
            while (index < Tweeners.Count)
            {
                var currentTweener = Tweeners[index];
                if (currentTweener.AnimTime <= CurrentTime)
                {
                    currentTweener.OnEnd?.Invoke();
                    AddNextTweens(currentTweener);
                    Tweeners.Remove(currentTweener);
                }
                else
                {
                    currentTweener.Update(CurrentTime);
                    index++;
                }
            }
        }
        
        private void OnDestroy()
        {
            new List<Tweener>(Tweeners).ForEach(t=>t.Kill());
        }

        #endregion

    }
}
