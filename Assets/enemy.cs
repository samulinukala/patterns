using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public static event Action<enemy> OnEnemyDie;

    public int enemyValue { get; private set; }
    private void OnDisable()
    {
        enemyValue = UnityEngine.Random.Range(1,5);

        OnEnemyDie?.Invoke(this);
    }
}
