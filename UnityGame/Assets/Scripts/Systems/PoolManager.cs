using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    void OnReturnedToPool();
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private Dictionary<string, Stack<GameObject>> _pools = new Dictionary<string, Stack<GameObject>>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public GameObject Get(string id, Func<GameObject> createFunc)
    {
        if (_pools.TryGetValue(id, out var stack) && stack.Count > 0)
        {
            var go = stack.Pop();
            go.SetActive(true);
            return go;
        }
        return createFunc();
    }

    public void Return(string id, GameObject obj)
    {
        if (obj.TryGetComponent<IPoolable>(out var poolable))
            poolable.OnReturnedToPool();

        obj.SetActive(false);
        if (!_pools.TryGetValue(id, out var stack))
        {
            stack = new Stack<GameObject>();
            _pools[id] = stack;
        }
        stack.Push(obj);
    }
}
