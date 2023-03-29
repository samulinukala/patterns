using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class controlManager : MonoBehaviour
{
    
   public List<badPlayer> players=new List<badPlayer>();
    public CinemachineFreeLook cam;
    public int previous_player;
    public int currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<players.Count-1;i++) 
        {
           
            if (i == 0)
            {
                players[i].controlstate=badPlayer.controlState.controlled;
            }
            else
            {
                players[i].controlstate= badPlayer.controlState.inert;
            }

        }
    }
    void switchPlayer(int player_to_change_to)
    {
        
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("control switched from: "+previous_player+" to: "+currentPlayer)
            if(i== previous_player)
            {
                players[i].controlstate = badPlayer.controlState.inert;
            }
            if(i == player_to_change_to)
            {
                players[i].controlstate = badPlayer.controlState.controlled;
                currentPlayer= i;
                cam.LookAt = players[currentPlayer].gameObject.transform;
                cam.Follow= players[currentPlayer].gameObject.transform;
                players[currentPlayer].virtualCamera = cam;
            }

        }
    }
    void takeInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentPlayer < players.Count-1 )
            {
                Debug.Log(currentPlayer);
                previous_player= currentPlayer;
                currentPlayer++;
                switchPlayer(currentPlayer);
            }
            else
            {
                Debug.Log(currentPlayer);
                previous_player = currentPlayer;
                currentPlayer = 0;
                switchPlayer(currentPlayer);

            }
        }else
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            if (currentPlayer >0)
            {
                Debug.Log(currentPlayer);
                previous_player = currentPlayer;
                currentPlayer--;
                switchPlayer(currentPlayer);
            }
            else
            {
                Debug.Log(currentPlayer);
                previous_player = currentPlayer;
                currentPlayer = players.Count - 1;
                switchPlayer(currentPlayer);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        takeInput();
    }
}
