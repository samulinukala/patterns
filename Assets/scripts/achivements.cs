using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class achivements : MonoBehaviour
{
    [SerializeField]
    public achivement_To_Get[] achivements_To_Get;
    public bool[] gotten;
    public enum Achivement_ID
    {
        coin_collector,It_was_an_accident
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        coin.OnCoinGet += SetAchivementToTrueForCoin;
        enemy.OnEnemyDie += SetAchivementToTrueForEnemy;
    }
   
    public void SetAchivementToTrueForCoin(Pawn pawn)
    {
        Debug.Log("coin get");
        achivements_To_Get[0].achived = true;
     

    }
    public void SetAchivementToTrueForEnemy(Pawn pawn)
    {
        Debug.Log("enemy killed");
        achivements_To_Get[1].achived = true;


    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("achivements");
            Debug.Log("coin get = " + achivements_To_Get[0].achived);
            Debug.Log("enemy killed = " + achivements_To_Get[1].achived);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [System.Serializable]
    public struct achivement_To_Get
    {
        public Achivement_ID Achivement_ID;
        public bool achived;
        
            

    }
    
}
