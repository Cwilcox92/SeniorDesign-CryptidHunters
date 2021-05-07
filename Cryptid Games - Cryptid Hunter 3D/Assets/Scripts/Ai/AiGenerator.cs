using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiGenerator : MonoBehaviour
{
    public GameObject aiPrefab = null;

    private List<List<float>> weights = new List<List<float>>();
    private List<float> gameReadyWeights = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        var ai = aiPrefab;

        // perform all weight gathering here
        foreach (var playerObj in GameObject.FindGameObjectsWithTag("Player"))
        {
            var w = playerObj.GetComponent<PlayerController>().CmdGetMyWeights();
            weights.Add(w);
        }

        float[] sums = new float[weights[0].Count];

        // squash list of lists into a single list
        foreach (var weightList in weights)
        {
            for (int i = 0; i < weightList.Count; i++)
            {
                sums[i] += weightList[i];
            }
        }

        for (int i = 0; i < sums.Length; i++)
        {
            sums[i] /= weights.Count;
            gameReadyWeights.Add(sums[i]);
        }

        // DEBUGGING OUTPUT THAT CAN BE USED TO VIEW THE VALUES BEING PASSED TO THE AI STATE MACHINE BOOTSTRAPPER
        // foreach (var val in gameReadyWeights)
        // {
        //     Debug.Log(val);
        // }

        ai.GetComponent<StateMachine>().CmdLoadWeights(weights[0]);
    }
}
