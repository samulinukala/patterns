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
    public bool isReplaying = false;
    public int replayActionCount=0;
    [SerializeField]
    public List<Command> commands = new List<Command>();
   public int currentplaceIntheList=0;
    public bool hasBeenUndoed=false;
    private Vector3 startPos;
    public Stack<Command> undoStack = new Stack<Command>();
    public Stack<Command> redoStack = new Stack<Command>();
    public enemy enemyPrefab;
    //score & enemies
    [SerializeField]
    private int Score = 0;
    [SerializeField]
    private int enemiesKilled = 0;
    public int ammounttoSpawn=20;
    public float radius1 = 20;
    public float radius2 = 1;
    public float lifeTime = 5;
    public int ammounttoSpawn2;
    private bool setSpawn=false;
    public CinemachineFreeLook virtualCamera;
    public Vector3 minPosition;
    public Vector3 maxPosition;

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
            Rigidbody rb = tf.gameObject.GetComponent<Rigidbody>();
            Vector3 oppositeDirection = -tf.forward;
            rb.AddForce(oppositeDirection * moveammount, ForceMode.Impulse);
          //  tf.transform.Translate(Vector3.back * moveammount);
            //tf.gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.back * moveammount,ForceMode.Impulse);
        }
        public override void Undo(Transform tf)
        {
            Rigidbody rb = tf.gameObject.GetComponent<Rigidbody>();
            Vector3 oppositeDirection = tf.forward;
            rb.AddForce(oppositeDirection * moveammount, ForceMode.Impulse);
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
            rb.AddForce(oppositeDirection * moveammount, ForceMode.Impulse);
            tf.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * moveammount, ForceMode.Impulse);
        }
        public override void Undo(Transform tf)
        {
            Rigidbody rb = tf.gameObject.GetComponent<Rigidbody>();
            Vector3 oppositeDirection = -tf.forward;
            rb.AddForce(oppositeDirection * moveammount, ForceMode.Impulse);
            tf.gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.back * moveammount, ForceMode.Impulse);
        }
    }
    [Serializable]
    public class moveLeft : Command
    {
        public override void Execute(Transform tf)
        {

            tf.gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.left * moveammount, ForceMode.Impulse);
        }
        public override void Undo(Transform tf)
        {
            tf.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * moveammount, ForceMode.Impulse);
        }
    }
    [Serializable]
    public class moveRight : Command
    {
        public override void Execute(Transform tf)
        {

            tf.gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.right * moveammount, ForceMode.Impulse);
        }
        public override void Undo(Transform tf)
        {
            tf.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * moveammount, ForceMode.Impulse);
           
        
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

        if (commands.Count > 0 && Input.GetKeyDown(KeyCode.Z))
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
            isReplaying = true;
        }

    }
    void spawn()
    {
        if (setSpawn == true)
        {

            for (int i = 0; i < ammounttoSpawn; i++)
            {

                int rand = UnityEngine.Random.Range(0, 4);
                GameObject go = Instantiate(enemyPrefab,transform.position+UnityEngine.Random.insideUnitSphere * radius1, UnityEngine.Random.rotation).gameObject;

                Vector3 randomPosition = new Vector3(
                   UnityEngine.Random.Range(minPosition.x, maxPosition.x),
                   UnityEngine.Random.Range(minPosition.y, maxPosition.y),
                    UnityEngine.Random.Range(minPosition.z, maxPosition.z));

                        go.GetComponent<Rigidbody>().AddForce(randomPosition, ForceMode.Impulse) ;
                go.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
                Destroy(go, lifeTime);

            }
            for (int i = 0; i < ammounttoSpawn2; i++)
            {
               
                GameObject go = Instantiate(enemyPrefab, transform.position + UnityEngine.Random.insideUnitSphere * radius2, UnityEngine.Random.rotation).gameObject;
                go.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
                Vector3 randomPosition = new Vector3(
                    UnityEngine.Random.Range(minPosition.x, maxPosition.x),
                    UnityEngine.Random.Range(minPosition.y, maxPosition.y),
                     UnityEngine.Random.Range(minPosition.z, maxPosition.z));

                go.GetComponent<Rigidbody>().AddForce(randomPosition, ForceMode.Impulse);
                Destroy(go, lifeTime);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = virtualCamera.transform.position;
        cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);

        if (isReplaying == false)
        {
            takeInput();
        }
        else if(timerReplayTarget<timerReplay)
        {
            Replay();
            timerReplay = 0;
        }
        else
        {
            timerReplay += Time.deltaTime;
        }
        spawn();
    }

    private void Replay()
    {
        if (replayActionCount < currentplaceIntheList)
        {
            commands[replayActionCount].Execute(transform);
            replayActionCount++;
        }
        else
        {
            isReplaying= false;
        }
    }
}
