using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class badPlayer : MonoBehaviour
{
    public float moveammount = 1;
    private Command button_W = new moveForward();
    private Command button_S = new moveBackward();
    private Command button_A = new moveLeft();
    private Command button_D = new moveRight();
    private float timerReplay = 0;
    public float timerReplayTarget=0.3f;
   
    public int replayActionCount=0;
    [SerializeField]
    public List<Command> commands = new List<Command>();
   public int currentplaceIntheList=0;
    public bool hasBeenUndoed=false;
    private Vector3 startPos;
    public Stack<Command> undoStack = new Stack<Command>();
    public Stack<Command> redoStack = new Stack<Command>();
    private bool setSpawn = false;
    public enum controlState
    {
        AI,
        inert,
        controlled,

    }
    public controlState controlstate;
    //score & enemies
    [SerializeField]
    private int Score = 0;
    [SerializeField]
    private int enemiesKilled = 0;

 
    public CinemachineFreeLook virtualCamera;

 
  
    //Command Class
    [Serializable]
    public abstract class Command
    {
        private float m_moveammount = 50;
        public float moveammount { get { return m_moveammount; } }
        public abstract void Execute(Transform tf);
        public abstract void Undo(Transform tf);
        
        
    }
    public enum gamestate
    {
        playing,
        paused,
        replay,

    }
    public gamestate m_gamestate = gamestate.playing;
    [Serializable]
    public class moveForward : Command
    {
        public override void Execute(Transform tf)
        {
            Rigidbody rb = tf.gameObject.GetComponent<Rigidbody>();
            Vector3 oppositeDirection = -tf.forward;
            rb.AddForce(oppositeDirection * moveammount*Time.deltaTime, ForceMode.Impulse);
          //  tf.transform.Translate(Vector3.back * moveammount);
            //tf.gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.back * moveammount,ForceMode.Impulse);
        }
        public override void Undo(Transform tf)
        {
            Rigidbody rb = tf.gameObject.GetComponent<Rigidbody>();
            Vector3 oppositeDirection = tf.forward;
            rb.AddForce(oppositeDirection * moveammount * Time.deltaTime, ForceMode.Impulse);
           // tf.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * moveammount, ForceMode.Impulse);
        }
    }
    [Serializable]
    public class moveBackward : Command
    {
        public override void Execute(Transform tf)
        {
             Rigidbody rb = tf.gameObject.GetComponent<Rigidbody>();
            Vector3 oppositeDirection = tf.forward;
            rb.AddForce(oppositeDirection * moveammount * Time.deltaTime, ForceMode.Impulse);
           // tf.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * moveammount * Time.deltaTime, ForceMode.Impulse);
        }
        public override void Undo(Transform tf)
        {
            Rigidbody rb = tf.gameObject.GetComponent<Rigidbody>();
            Vector3 oppositeDirection = -tf.forward;
            rb.AddForce(oppositeDirection * moveammount * Time.deltaTime, ForceMode.Impulse);
           // tf.gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.back * moveammount * Time.deltaTime, ForceMode.Impulse);
        }
    }
    [Serializable]
    public class moveLeft : Command
    {
        public override void Execute(Transform tf)
        {

            tf.gameObject.GetComponent<Rigidbody>().AddForce(tf.right * moveammount * Time.deltaTime, ForceMode.Impulse);
        }
        public override void Undo(Transform tf)
        {
            tf.gameObject.GetComponent<Rigidbody>().AddForce(tf.right * moveammount * Time.deltaTime, ForceMode.Impulse);
        }
    }
    [Serializable]
    public class moveRight : Command
    {
        public override void Execute(Transform tf)
        {

            tf.gameObject.GetComponent<Rigidbody>().AddForce(-tf.right * moveammount * Time.deltaTime, ForceMode.Impulse);
        }
        public override void Undo(Transform tf)
        {
            tf.gameObject.GetComponent<Rigidbody>().AddForce(-tf.right * moveammount * Time.deltaTime, ForceMode.Impulse);
           
        
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;   
        Cursor.lockState = CursorLockMode.Locked;
    }
    void addToScore(Pawn _enemy)
    {
        this.Score +=  _enemy.value;
        this.enemiesKilled++;
        Debug.Log("score added");
    }
    private void Awake()
    {
        enemy.OnEnemyDie += addToScore;
        
    }
    void clearList(int _WhatPointsToStartClearing)
    {
        commands.RemoveRange(_WhatPointsToStartClearing,commands.Count-1-currentplaceIntheList);
        currentplaceIntheList=commands.Count-1;
    }
    private void FixedUpdate()
    {
       
    }

    void takeInput()
    {
        
        if(Input.GetKey(KeyCode.Space))
        {
            if (setSpawn != false)
            {
                setSpawn = false;
            }
            else if (setSpawn == false)
            {
                setSpawn = true; ;
            }
          
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            if (hasBeenUndoed == true) clearList(currentplaceIntheList);
            commands.Add(button_S); currentplaceIntheList++;
            button_S.Execute(transform);
            undoStack.Push(button_S);
            hasBeenUndoed = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (hasBeenUndoed == true) clearList(currentplaceIntheList);
            button_D.Execute(transform);
            commands.Add(button_D);
            undoStack.Push(button_D);
            currentplaceIntheList++;
            hasBeenUndoed = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (hasBeenUndoed == true) clearList(currentplaceIntheList);
            button_A.Execute(transform);
            commands.Add(button_A);
            undoStack.Push(button_A);
            currentplaceIntheList++;
            hasBeenUndoed = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (hasBeenUndoed == true) clearList(currentplaceIntheList);
            button_W.Execute(transform);
            undoStack.Push(button_W);
            commands.Add(button_W); currentplaceIntheList++;
            hasBeenUndoed = false;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_gamestate = gamestate.paused;
            Time.timeScale = 0.0f;
            Debug.Log("paused state");
        }

        if (commands.Count > 0 && Input.GetKey(KeyCode.Z))
        {
            commands[currentplaceIntheList - 1].Undo(transform);
            currentplaceIntheList--;

            hasBeenUndoed = true;

        }
        if (commands.Count - 1 > currentplaceIntheList && Input.GetKeyDown(KeyCode.X))
        {
            
            commands[currentplaceIntheList].Execute(transform);
            currentplaceIntheList += 1;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPos;
            m_gamestate=gamestate.replay;
            Debug.Log("switch to replay mode");
        }
        // tämä vaihtaa ai tilaan
        if(Input.GetKeyDown(KeyCode.T)) 
        {
            controlstate = controlState.AI;
            Debug.Log("now in ai mode");
        }

    }
    void handlerotatioin()
    {
        Vector3 cameraPosition = virtualCamera.transform.position;
        cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);
    }


    // Update is called once per frame
    void Update()
    {
        if (controlstate == controlState.controlled)
        {
            handlerotatioin();
            FindObjectOfType<spawnner>().spawnBool = setSpawn;
            if (m_gamestate == gamestate.playing)
            {
                takeInput();
            }
            else if (timerReplayTarget < timerReplay && m_gamestate == gamestate.replay)
            {
                Replay();
                timerReplay = 0;
            }
            else if (m_gamestate == gamestate.playing)
            {
                timerReplay += Time.deltaTime;
            }
            else
            if (m_gamestate == gamestate.paused)
            {

                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
                {
                    m_gamestate = gamestate.playing;
                    Time.timeScale = 1.0f;
                    Debug.Log("resume");
                }
            }
        }
        else if(controlstate==controlState.AI) 
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                controlstate = controlState.controlled;
            }
            // tässä pitäisi olla metodit ai liikkumiseen ja muuhun sellaiseen
            Debug.Log("now in player mode");
        }
    }

    private void Replay()
    {
        if (replayActionCount < currentplaceIntheList)
        {
            m_gamestate = gamestate.replay;
            commands[replayActionCount].Execute(transform);
            replayActionCount++;
        }
        else
        {
            m_gamestate=gamestate.playing;
            Debug.Log("replay over switching to player mode");
        }
    }
}
