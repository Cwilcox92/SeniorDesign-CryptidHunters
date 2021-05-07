using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public enum State
    {
        Trapped = -3,
        Loading = -2,
        NoTransition = -1,
        Hunting = 1,
        Running,
        Sleeping,
        Wandering
    }

    public State state = State.Loading;

    ActionInterface activeController = null;

    private bool regressed = false;

    private List<float> transitions = new List<float>();

    private float[] inputs = new float[2];

    private NeuralNetwork activeNetwork;
    private RegressionHandler rhandler;

    // Start is called before the first frame update
    void Start() 
    {
        rhandler = gameObject.AddComponent<RegressionHandler>() as RegressionHandler;
        activeNetwork = gameObject.AddComponent<NeuralNetwork>() as NeuralNetwork;

        for (int i = 0; i < 2; i++) {
            inputs[i] = 0.0F;
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputs[0] = senseDistance("TrapBody");
        inputs[1] = senseDistance("Player");

        if (state != State.Trapped) {
            regressed = false;
            State newState = calculateTransition();
            if (newState >= 0 && newState != state) 
            {
                rhandler.Push(Instantiate(activeNetwork), inputs);
                Transition(newState);
            }

            // NOTE: ***ALL*** controllers used for action must implement the ActionInterface to make this work.
            if (activeController != null)
            {
                activeController.Act();            
            }
        } else if (!regressed) {
            rhandler.RunRegression(inputs);
            regressed = !regressed;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    // get the closest distance to an object tagged with tag t
    float senseDistance(string t)
    {
        Vector3 myPosition = transform.position;
        GameObject[] objs;
        float closest = 1000000000.0F;

        objs = GameObject.FindGameObjectsWithTag(t);

        foreach (GameObject obj in objs) {
            float dist = Vector3.Distance(obj.transform.position, myPosition);
            if (dist < closest)
            {
                closest = dist;
            }
        }

        return closest;
    }
    
    // return NoTransition if the state shouldn't change
    State calculateTransition()
    {
        int maxIdx = activeNetwork.CalculateTransition(inputs);

        // TODO: Remove this
        //maxIdx = 3;

        // convert output node to action state
        switch (maxIdx)
        {
            case 0:
                Debug.Log("Transition -> Hunting");
                return State.Hunting;
            case 1:
                Debug.Log("Transition -> Running");
                return State.Running;
            case 2:
                Debug.Log("Transition -> Sleeping");
                return State.Sleeping;
            case 3:
                Debug.Log("Transition -> Wandering");
                return State.Wandering;
            default:
                Debug.Log("Transition -> Default -> Wandering");
                return State.Wandering;
        }
    }

    // this function will perform whatever actions are necessary to begin acting in a new state
    // returns the new active controller
    void Transition(State s)
    {
        switch (s)
        {
            case State.Hunting:
                activeController = GetComponent<HuntingController>();
                break;
            case State.Running:
                activeController = GetComponent<RunningController>();
                break;
            case State.Sleeping:
                activeController = GetComponent<SleepingController>();
                break;
            case State.Wandering:
                activeController = GetComponent<WanderingController>();
                break;

            case State.Loading:
                Debug.Log("Waiting for weights to be filled in from CmdLoadWeights");
                break;
            // the following two states are included for the sake of
            // covering every possible state.
            // it should not be possible to be in either state within this function
            case State.NoTransition:
                Debug.Log("This should never happen: NoTransition");
                break;
            default:
                Debug.Log("We should never hit this default.");
                break;
        }

        // perform the transition and update the AI's state
        if (activeController != null) {
            activeController.Transition();
        }
        state = s;
    }

    // load in weights
    public void CmdLoadWeights(List<float> t)
    {
        // converts the linear list given to us into the multi dimensional structs we use
        int index = 0;
        int inputNodeCount = activeNetwork.getInputNodeCount();
        int hiddenNodeCount = activeNetwork.getHiddenNodeCount();
        int outputNodeCount = activeNetwork.getOutputNodeCount();

        //
        // WEIGHTS
        //
        // weightsOne
        float[][] tmpMatrix = new float[inputNodeCount][];
        for (int i = 0; i < inputNodeCount; i++) {
            tmpMatrix[i] = new float[hiddenNodeCount];
        }
        for (int i = 0; i < inputNodeCount; i++) {
            for (int j = 0; j < hiddenNodeCount; j++) {
                tmpMatrix[i][j] = t[index];
                index++;
            }
        }
        activeNetwork.SetWeights(tmpMatrix, 0);
        // weightsTwo
        tmpMatrix = new float[hiddenNodeCount][];
        for (int i = 0; i < hiddenNodeCount; i++) {
            tmpMatrix[i] = new float[outputNodeCount];
        }
        for (int i = 0; i < hiddenNodeCount; i++) {
            for (int j = 0; j < outputNodeCount; j++) {
                tmpMatrix[i][j] = t[index];
                index++;
            }
        }
        activeNetwork.SetWeights(tmpMatrix, 1);

        //
        // BIASES
        //
        float[] tmpVector = new float[inputNodeCount];
        for (int i = 0; i < inputNodeCount; i++) {
            tmpVector[i] = t[index];
            index++;
        }
        activeNetwork.SetBiases(tmpVector, 0);
        tmpVector = new float[hiddenNodeCount];
        for (int i = 0; i < hiddenNodeCount; i++) {
            tmpVector[i] = t[index];
            index++;
        }
        activeNetwork.SetBiases(tmpVector, 1);
        tmpVector = new float[outputNodeCount];
        for (int i = 0; i < outputNodeCount; i++) {
            tmpVector[i] = t[index];
            index++;
        }
        activeNetwork.SetBiases(tmpVector, 2);

        State initialState = calculateTransition();
        Transition(initialState);
    }

    public void hollywoodShutdown()
    {
        state = State.Trapped;
    }

    public void shutdown()
    {

    }
}

// this interface must be implemented by every behaviour script
public interface ActionInterface 
{
    void Act();
    void Transition();
    void Report();
}
