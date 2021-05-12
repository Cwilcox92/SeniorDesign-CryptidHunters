using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingController : MonoBehaviour, ActionInterface
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Transition()
    {

    }

    public void Act()
    {
        int waitTime = Random.Range(5, 30);
        SleepCoroutine(waitTime);
    }

    IEnumerable SleepCoroutine(int waitTime)
    {
        Debug.Log("Started sleep at timestamp : " + Time.time);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Finished sleep at timestamp : " + Time.time);
    }

    public void Report()
    {
        
    }
}