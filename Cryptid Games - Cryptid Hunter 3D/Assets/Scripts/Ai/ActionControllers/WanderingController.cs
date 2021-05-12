using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingController : MonoBehaviour, ActionInterface
{

    public Rigidbody aiRigidbody = null;
    public Animator animator;
    private StateMachine SM;
    private NavMeshAgent agent;
    public Vector3 target = new Vector3();
    private Vector2 Heading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Transition()
    {  
        agent = GetComponent<NavMeshAgent> ();
        aiRigidbody = GetComponent<Rigidbody>();
        Vector3 center = aiRigidbody.position;
        float xCord = Random.Range(-50,50);
        float zCord = Random.Range(-50,50);
        if(Mathf.Abs(center.x + xCord) > 100)
        {
            xCord = center.x - xCord;
        }
        else
        {
            xCord = center.x + xCord;
        }
        if(Mathf.Abs(center.z + zCord) > 100)
        {
            zCord = center.z - zCord;
        }
        else
        {
            zCord = center.z + zCord;
        }

        target = new Vector3(xCord, aiRigidbody.position.y,zCord);
        //target = new Vector3(-17, aiRigidbody.position.y,-83);

        SleepCoroutine(30);

        agent.destination =  target;
        agent.isStopped = false;
    }
    IEnumerable SleepCoroutine(int waitTime)
    {
        Debug.Log("Started sleep at timestamp : " + Time.time);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Finished sleep at timestamp : " + Time.time);
    }

    private void Update() {
        Heading.x= Vector3.Normalize(GetComponent<Rigidbody>().velocity).x;
        Heading.y= Vector3.Normalize(GetComponent<Rigidbody>().velocity).y;
        
        animator.SetFloat("Horizontal", Heading.x);
        animator.SetFloat("Vertical", Heading.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TrapBody")
        {
            aiRigidbody = GetComponent<Rigidbody>();
            agent.destination =  aiRigidbody.position;
            SM = GetComponent<StateMachine>();
            SM.hollywoodShutdown();
        }
    }

    public void Act()
    {
        //aiRigidbody.position = Vector3.MoveTowards(transform.position, target, 0.1f);
    }

    public void Report()
    {
        Debug.Log("Wandering Controller Target: " + target);
    }
}