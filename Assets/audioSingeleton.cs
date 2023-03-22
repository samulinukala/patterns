using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioSingeleton : MonoBehaviour
{//make a singleton
    public static audioSingeleton instance { get; private set; }
    public AudioClip ds;
    public AudioClip s;
    public AudioSource AudioSource;
    // Start is called before the first frame update
     void Awake()
    {
        //set instance to me
       
        //check if there are others
        if (instance != null&&instance!=this)
        {
            Destroy(this);
            Debug.Log("this was wrong one");
        }else if (instance != null && instance == this)
        {
            Debug.Log("is already here");
        }
        else if(instance == null)
        {
            Debug.Log("instance set to this");
            instance = this;
        }
    }
    public void playSouund()
    {
        instance.AudioSource.clip = ds;
        instance.AudioSource.Play();
    }
    public void playSouund2()
    {
        instance.AudioSource.clip = s;
        instance.AudioSource.Play();
    }
    public void PlaySound()
    {
        Debug.Log("sounds");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
