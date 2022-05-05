using UnityEngine;
using WB.Pool;

namespace WB.Tweener
{
    internal class TweenerPool
    {
        internal readonly Pool<Tweener<Color>> colorTweenerPool;
        internal readonly Pool<Tweener<float>> floatTweenerPool;
        internal readonly Pool<Tweener<Vector2>> vec2TweenerPool;
        internal readonly Pool<Tweener<Vector3>> vec3TweenerPool;

        internal void Return<T>(Tweener<T> tweener)
        {
            
        }
        
        private TweenerPool()
        {
            floatTweenerPool = WBPool.MakePool(3, () => new Tweener<float>());
            vec2TweenerPool = WBPool.MakePool(3, () => new Tweener<Vector2>());
            vec3TweenerPool = WBPool.MakePool(3, () => new Tweener<Vector3>());
            colorTweenerPool = WBPool.MakePool(3, () => new Tweener<Color>());
        }

        #region Singleton

        private static TweenerPool instance;
        internal static TweenerPool I => instance ??= new TweenerPool();

        #endregion
    }
}