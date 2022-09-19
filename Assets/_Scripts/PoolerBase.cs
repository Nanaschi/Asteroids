using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

/// <summary>
/// A simple base class to simplify object pooling in Unity 2021.
/// Derive from this class, call InitPool and you can Get and Release to your hearts content.
/// If you enjoyed the video or this script, make sure you give me a like on YT and let me know what you thought :)
/// </summary>
/// <typeparam name="TOne">A MonoBehaviour object you'd like to perform pooling on.</typeparam>
public abstract class PoolerBase<TOne>: MonoBehaviour
    where TOne : MonoBehaviour 
{
    private TOne _prefab;
    private ObjectPool<TOne> _pool;

    private ObjectPool<TOne> Pool {
        get {
            if (_pool == null) throw new InvalidOperationException("You need to call InitPool before using it.");
            return _pool;
        }
        set => _pool = value;
    }

    protected void InitPool(TOne prefab, int initial = 10, int max = 20, bool collectionChecks = false) {
        _prefab = prefab;
        Pool = new ObjectPool<TOne>(
            CreateSetup,
            GetSetup,
            ReleaseSetup,
            DestroySetup,
            collectionChecks,
            initial,
            max);
    }

    #region Overrides
    protected virtual TOne CreateSetup() => Object.Instantiate(_prefab);
    protected virtual void GetSetup(TOne obj) => obj.gameObject.SetActive(true);
    protected virtual void ReleaseSetup(TOne obj) => obj.gameObject.SetActive(false);
    protected virtual void DestroySetup(TOne obj) => Object.Destroy(obj);
    #endregion

    #region Getters
    public TOne Get() => Pool.Get();
    public void Release(TOne obj) => Pool.Release(obj);
    #endregion
}
