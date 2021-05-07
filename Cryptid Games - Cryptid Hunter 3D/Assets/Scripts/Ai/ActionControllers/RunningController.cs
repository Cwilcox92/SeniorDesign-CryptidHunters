using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunningController : MonoBehaviour, ActionInterface
{
    // Start is called before the first frame update
    public Rigidbody aiRigidbody = null;

    public Animator animator;

    private NavMeshAgent agent;
    private int velocity;
    private StateMachine SM;
    private Vector2 Heading;
    public Vector3 target = new Vector3();
    void Start()
    {
        
    }

    public void Transition()
    {
        agent = GetComponent<NavMeshAgent> ();
        aiRigidbody = GetComponent<Rigidbody>();
        Vector3 center = aiRigidbody.position;
        float xCord;
        float zCord;

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");

        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - center;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }


        xCord = aiRigidbody.position.x + (aiRigidbody.position.x - closest.transform.position.x);
        zCord = aiRigidbody.position.z + (aiRigidbody.position.z - closest.transform.position.z);

        target = new Vector3(xCord, aiRigidbody.position.y,zCord);

        Debug.Log("AI Food at: "+closest.transform.position.x +","+closest.transform.position.z);

        agent.destination = target;
        agent.isStopped = false;
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

    }

    public void Report()
    {
        
    }
}
