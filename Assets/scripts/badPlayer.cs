using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class badPlayer : MonoBehaviour
{
    public float moveammount = 1;
    private Command button_W = new moveForward();
    private Command button_S = new moveBackward();
    private Command button_A = new moveLeft();
    private Command button_D = new moveRight();
    [SerializeField]
    public List<Command> commands = new List<Command>();
   public int currentplaceIntheList=0;
    public bool hasBeenUndoed=false;
    //Command Class
    [Serializable]
    public abstract class Command
    {
        private float m_moveammount = 1.0f;
        public float moveammount { get { return m_moveammount; } }
        public abstract void Execute(Transform tf);
        public abstract void Undo(Transform tf);
        
    }
    [Serializable]
    public class moveForward : Command
    {
        public override void Execute(Transform tf)
        {

            tf.Translate(Vector3.forward * moveammount);
        }
        public override void Undo(Transform tf)
        {
            tf.Translate(-Vector3.forward * moveammount);
        }
    }
    [Serializable]
    public class moveBackward : Command
    {
        public override void Execute(Transform tf)
        {

            tf.Translate(Vector3.back * moveammount);
        }
        public override void Undo(Transform tf)
        {
            tf.Translate(-Vector3.back * moveammount);
        }
    }
    [Serializable]
    public class moveLeft : Command
    {
        public override void Execute(Transform tf)
        {

            tf.Translate(Vector3.left * moveammount);
        }
        public override void Undo(Transform tf)
        {
            tf.Translate(-Vector3.left * moveammount);
        }
    }
    [Serializable]
    public class moveRight : Command
    {
        public override void Execute(Transform tf)
        {

            tf.Translate(Vector3.right * moveammount);
        }
        public override void Undo(Transform tf)
        {
            tf.Translate(-Vector3.right * moveammount);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void clearList(int _WhatPointsToStartClearing)
    {
        commands.RemoveRange(_WhatPointsToStartClearing,commands.Count-1-currentplaceIntheList);
        currentplaceIntheList=commands.Count-1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (hasBeenUndoed == true) clearList(currentplaceIntheList);
            commands.Add(button_S); currentplaceIntheList ++;
            button_S.Execute(transform);
            hasBeenUndoed = false;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            if (hasBeenUndoed == true)  clearList(currentplaceIntheList);  
            button_D.Execute(transform); 
            commands.Add(button_D); 
            currentplaceIntheList ++; 
            hasBeenUndoed = false; } 

        if (Input.GetKeyDown(KeyCode.A)) {
            if (hasBeenUndoed == true) clearList(currentplaceIntheList); 
            button_A.Execute(transform); 
            commands.Add(button_A);
            currentplaceIntheList++; 
            hasBeenUndoed = false; }

        if (Input.GetKeyDown(KeyCode.W)) { 
            if (hasBeenUndoed == true) clearList(currentplaceIntheList); 
            button_W.Execute(transform); 
            commands.Add(button_W);currentplaceIntheList++; 
            hasBeenUndoed = false; }

        if (commands.Count>0 && Input.GetKeyDown(KeyCode.Z))
        {
            commands[currentplaceIntheList-1].Undo(transform);
            currentplaceIntheList -- ;
            hasBeenUndoed= true;
           
        }
        if(commands.Count-1>currentplaceIntheList&&Input.GetKeyDown(KeyCode.X))
        {
            commands[currentplaceIntheList].Execute(transform);
            currentplaceIntheList += 1;
        }


    }
}
