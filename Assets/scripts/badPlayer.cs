using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badPlayer : MonoBehaviour
{
    public float moveammount=1;
    private Command button_W = new moveForward();
    //Command Class
    public abstract class Command
    {
        private float m_moveammount = 1.0f;
        public float moveammount{ get {return moveammount; } }
        public abstract void Execute(Transform tf);
    }
    public class moveForward : Command
    {
        public override void Execute(Transform tf)
        {

            tf.Translate(Vector3.forward * moveammount);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) transform.Translate(Vector3.back*moveammount);
        if (Input.GetKeyDown(KeyCode.D)) transform.Translate(Vector3.right * moveammount);
        if (Input.GetKeyDown(KeyCode.A)) transform.Translate(Vector3.left * moveammount);
        if (Input.GetKeyDown(KeyCode.W)) button_W.Execute(transform);


    }
}
