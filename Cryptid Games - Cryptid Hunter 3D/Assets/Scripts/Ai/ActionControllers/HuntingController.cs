using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuntingController : MonoBehaviour, ActionInterface
{
    
    public Rigidbody aiRigidbody = null;
    public Animator animator;
    private StateMachine SM;
    private NavMeshAgent agent;
    public Vector3 target = new Vector3();
    private Vector2 Heading;
    void Start()
    {
        
    }

    public void Transition()
    {
        agent = GetComponent<NavMeshAgent> ();
        aiRigidbody = GetComponent<Rigidbody>();
        Vector3 center = aiRigidbody.position;

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("AIFood");

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

        target = new Vector3(closest.transform.position.x, aiRigidbody.position.y,closest.transform.position.z);

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
