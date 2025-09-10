using System;
using System.Collections;
using UnityEngine;

public class EnemyPlaceholder : MonoBehaviour
{
    private int _hp;
    private Action _onDeath;

    public void Init(int hp, Action onDeath)
    {
        _hp = hp;
        _onDeath = onDeath;
        StartCoroutine(AutoDie());
    }

    private IEnumerator AutoDie()
    {
        yield return new WaitForSeconds(Mathf.Clamp(_hp / 10f, 1f, 8f));
        _onDeath?.Invoke();
        Destroy(gameObject);
    }
}
