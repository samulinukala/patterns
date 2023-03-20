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
        startPos = transform.position;   
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
    void takeInput()
    {
        if(Input.GetKey(KeyCode.Space)) 
        {
            GameObject go = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go, 5f);
            GameObject go2 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go2, 5f); GameObject go3 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go3, 5f); GameObject go4 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go4, 5f); GameObject go5 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go5, 5f); GameObject go6 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go6, 5f); GameObject go7 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go7, 5f); GameObject go8 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go8, 5f); GameObject go9 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go9, 5f); GameObject go11 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go11, 5f); GameObject go12 = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitSphere, enemyPrefab.transform.rotation).gameObject;
            Destroy(go12, 5f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (hasBeenUndoed == true) clearList(currentplaceIntheList);
            commands.Add(button_S); currentplaceIntheList++;
            button_S.Execute(transform);
            undoStack.Push(button_S);
            hasBeenUndoed = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (hasBeenUndoed == true) clearList(currentplaceIntheList);
            button_D.Execute(transform);
            commands.Add(button_D);
            undoStack.Push(button_D);
            currentplaceIntheList++;
            hasBeenUndoed = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (hasBeenUndoed == true) clearList(currentplaceIntheList);
            button_A.Execute(transform);
            commands.Add(button_A);
            undoStack.Push(button_A);
            currentplaceIntheList++;
            hasBeenUndoed = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
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

    // Update is called once per frame
    void Update()
    {
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
