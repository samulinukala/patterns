using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : Pawn
{
    public static event Action<Pawn> OnEnemyDie;

    
    private void OnDisable()
    {
        

        OnEnemyDie?.Invoke(this);
    }
}
