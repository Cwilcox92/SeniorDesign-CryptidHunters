using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
            foreach(Sound s in sounds)
            {
                s.source= gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch= s.pitch;
                s.source.loop = s.loop;
                s.source.playOnAwake= s.playOnAwake;
            }
        
    }

   public void Play(string name)
   {
       Sound s = Array.Find(sounds, sounds => sounds.name == name);
       if(s == null)
       {
           Debug.Log("Sound " + name + " not found!");
           return;

       }
       s.source.Play();
   }

   public void Stop(string name)
   {
       Sound s = Array.Find(sounds, sounds => sounds.name == name);
       if(s == null)
       {
           Debug.Log("Sound " + name + " not found!");
           return;

       }
       s.source.Stop();
   }
}
