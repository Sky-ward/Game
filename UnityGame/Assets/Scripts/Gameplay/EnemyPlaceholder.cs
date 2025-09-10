using System;
using System.Collections;
using UnityEngine;

public class EnemyPlaceholder : MonoBehaviour, IPoolable
{
    private int _hp;
    private Action _onDeath;
    private Coroutine _dieRoutine;
    private string _poolId;

    public void Init(string poolId, int hp, Action onDeath)
    {
        _poolId = poolId;
        _hp = hp;
        _onDeath = onDeath;
        if (_dieRoutine != null)
            StopCoroutine(_dieRoutine);
        _dieRoutine = StartCoroutine(AutoDie());
    }

    private IEnumerator AutoDie()
    {
        yield return new WaitForSeconds(Mathf.Clamp(_hp / 10f, 1f, 8f));
        _onDeath?.Invoke();
        PoolManager.Instance.Return(_poolId, gameObject);
    }

    public void OnReturnedToPool()
    {
        _onDeath = null;
        if (_dieRoutine != null)
        {
            StopCoroutine(_dieRoutine);
            _dieRoutine = null;
        }
    }
}
