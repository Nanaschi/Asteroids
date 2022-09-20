using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace _Scripts
{
    /// <summary>
    /// A simple base class to simplify object pooling in Unity 2021.
    /// Derive from this class, call InitPool and you can Get and Release to your hearts content.
    /// If you enjoyed the video or this script, make sure you give me a like on YT and let me know what you thought :)
    /// </summary>
    /// <typeparam name="TOne">A MonoBehaviour object you'd like to perform pooling on.</typeparam>
    public abstract class DoublePoolerBase<TOne, TTwo> : MonoBehaviour
        where TOne : MonoBehaviour where TTwo : MonoBehaviour
    {
        private TOne _prefab1;
        private TTwo _prefab2;
        private ObjectPool<TOne> _pool1;
        private ObjectPool<TTwo> _pool2;

        private ObjectPool<TOne> Pool1
        {
            get
            {
                if (_pool1 == null)
                    throw new InvalidOperationException("You need to call InitPool before using it.");
                return _pool1;
            }
            set => _pool1 = value;
        }

        private ObjectPool<TTwo> Pool2
        {
            get
            {
                if (_pool1 == null)
                    throw new InvalidOperationException("You need to call InitPool before using it.");
                return _pool2;
            }
            set => _pool2 = value;
        }

        protected void InitPool(TOne prefab1, TTwo prefab2, int initial = 10, int max = 20,
            bool collectionChecks = false)
        {
            _prefab1 = prefab1;
            _prefab2 = prefab2;
            Pool1 = new ObjectPool<TOne>(CreateSetup1, GetSetup1, ReleaseSetup1, DestroySetup1,
                collectionChecks, initial, max);

            Pool2 = new ObjectPool<TTwo>(CreateSetup2, GetSetup2, ReleaseSetup2, DestroySetup2,
                collectionChecks, initial, max);
        }

        private void DestroySetup2(TTwo obj)
        {
            Object.Destroy(obj);
        }

        private void ReleaseSetup2(TTwo obj)
        {
            obj.gameObject.SetActive(false);
        }

        public virtual void GetSetup2(TTwo obj)
        {
            obj.gameObject.SetActive(true);
        }

        public virtual TTwo CreateSetup2()
        {
            return Instantiate(_prefab2);
        }

        private void DestroySetup1(TOne obj)
        {
            Object.Destroy(obj);
        }

        private void ReleaseSetup1(TOne obj)
        {
            obj.gameObject.SetActive(false);
        }

        public virtual void GetSetup1(TOne obj)
        {
            obj.gameObject.SetActive(true);
        }

        public virtual TOne CreateSetup1()
        {
            return Instantiate(_prefab1);
        }


        #region Getters

        public TOne Get1() => Pool1.Get();
        public void Release1(TOne obj) => Pool1.Release(obj);

        public TTwo Get2() => Pool2.Get();
        public void Release2(TTwo obj) => Pool2.Release(obj);

        #endregion
    }
}