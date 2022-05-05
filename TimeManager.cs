using UnityEngine;

namespace WB.Tweener
{
    internal class TimeManager : MonoBehaviour
    {
        private TweenerManager tweenerManager;
        // Update is called once per frame

        private void Start()
        {
            tweenerManager = TweenerManager.I;
        }

        private void Update()
        {
            foreach (var t in tweenerManager.Tweeners) t.Update(Time.deltaTime);
        }
    }
}