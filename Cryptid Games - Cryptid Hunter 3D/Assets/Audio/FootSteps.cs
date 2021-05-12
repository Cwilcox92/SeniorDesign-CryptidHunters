using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;


public class FootSteps : MonoBehaviour
{
    PlayerController playerController;
    public AudioManager audioManager;
    public Sound[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        playerController= GetComponent<PlayerController>();
        audioManager= FindObjectOfType<AudioManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.velocity == 0)
        {
            Sound s = Array.Find(sounds, sounds => sounds.name == name);
                if(s == null)
                {
                    Debug.Log("Sound " + name + " not found!");
                    return;

                }
                s.source.Stop();
        }
        else
        {
           Sound s = Array.Find(sounds, sounds => sounds.name == name);
                    if(s == null)
                    {
                        Debug.Log("Sound " + name + " not found!");
                        return;

                    }
                    s.source.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
                    s.source.Play();
        }
        
    }
}
