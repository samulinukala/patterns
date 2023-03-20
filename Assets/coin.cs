using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class coin : Pawn
{
  
    public static event Action<Pawn> OnCoinGet;

    public int enemyValue { get; private set; }
  //laitoin collisiossa ker‰‰m‰‰n esineit‰ sen sijaan ett‰ se olisi disablessa
  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<badPlayer>() != null) 
        { 
            gameObject.SetActive(false); 
            OnCoinGet?.Invoke(this); 
        }

    }
}
