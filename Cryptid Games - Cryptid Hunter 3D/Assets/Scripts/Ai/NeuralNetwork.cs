using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    public const int inputNodeCount = 2; // mirrored in RegressionHandler.cs
    public const int hiddenNodeCount = 4; // mirrored in RegressionHandler.cs
    public const int outputNodeCount = 4; // mirrored in RegressionHandler.cs

    private float[][] weightsOne = new float[inputNodeCount][];
    private float[][] weightsTwo = new float[hiddenNodeCount][];

    private float[] biasesInput = new float[inputNodeCount];
    private float[] biasesHidden = new float[hiddenNodeCount];
    private float[] biasesOutput = new float[outputNodeCount];

    private float[] layerOne = new float[hiddenNodeCount];
    private float[] layerOutput = new float[outputNodeCount];

    /* 
     *  UNITY INTERNAL FUNCTIONS
     */
    void Start()
    {
        // bootstrapping of memory and lists
        for (int i = 0; i < inputNodeCount; i++) {
            weightsOne[i] = new float[hiddenNodeCount];
        }
        for (int i = 0; i < hiddenNodeCount; i++) {
            weightsTwo[i] = new float[outputNodeCount];
        }
    }

    /* 
     *  MAIN WORK FUNCTIONS
     */
    public int CalculateTransition(float[] exampleInputs)
    {
        // reset node layers
        for (int i = 0; i < hiddenNodeCount; i++) {
            layerOne[i] = 0.0F;
        }
        for (int i = 0; i < outputNodeCount; i++) {
            layerOutput[i] = 0.0F;
        }

        // add biases to inputs
        for (int i = 0; i < inputNodeCount; i++) {
            exampleInputs[i] *= biasesInput[i];
        }

        // go from input to hidden
        // weights
        for (int i = 0; i < inputNodeCount; i++) {
            for (int j = 0; j < hiddenNodeCount; j++) {
                layerOne[j] += exampleInputs[i] * weightsOne[i][j];
            }
        }
        // biases
        for (int i = 0; i < hiddenNodeCount; i++) {
            layerOne[i] *= biasesHidden[i];
        }

        // go from hidden to output
        // weights
        for (int i = 0; i < hiddenNodeCount; i++) {
            for (int j = 0; j < outputNodeCount; j++) {
                layerOutput[i] += layerOne[i] * weightsTwo[i][j];
            }
        }
        // biases
        for (int i = 0; i < outputNodeCount; i++) {
            layerOutput[i] *= biasesOutput[i];
        }

        // get max index (i.e. number of state we move into)
        int maxIdx = 0;
        float maxWeight = 0.0F;
        for (int i = 0; i < outputNodeCount; i++) {
            if (maxWeight < layerOutput[i]) {
                maxWeight = layerOutput[i];
                maxIdx = i;
            }
        }

        // used in the state machine to calculate which state to transition to
        return maxIdx;
    }

    /* 
     *  MEMORY MANAGEMENT HELPERS
     */
    void fillVector(float[] b, float[] v)
    {
        for (int i = 0; i < b.Length; i++)
        {
            v[i] = b[i];
        }
    }

    void fillMatrix(float[][] w, float[][] m)
    {
        for (int i = 0; i < w.Length; i++)
        {
            for (int j = 0; j < w[i].Length; j++)
            {
                m[i][j] = w[i][j];
            }
        }
    }

    /* 
     *  GETTERS AND SETTERS
     */
    public int getInputNodeCount() { return inputNodeCount; }
    public int getHiddenNodeCount() { return hiddenNodeCount; }
    public int getOutputNodeCount() { return outputNodeCount; }

    public float[] GetBiases(int i)
    {
        switch (i)
        {
            case 0:
                return biasesInput;
            case 1:
                return biasesHidden;
            case 2:
                return biasesOutput;
            default:
                return null;
        }
    }

    public void SetBiases(float[] b, int i)
    {
        switch (i)
        {
            case 0:
                fillVector(b, biasesInput);
                break;
            case 1:
                fillVector(b, biasesHidden);
                break;
            case 2:
                fillVector(b, biasesOutput);
                break;
            default:
                break;
        }
    }

    public float[][] GetWeights(int i )
    {
        switch (i)
        {
            case 0:
                return weightsOne;
            case 1:
                return weightsTwo;
            default:
                return null;
        }
    }

    public void SetWeights(float[][] w, int i)
    {
        switch (i)
        {
            case 0:
                fillMatrix(w, weightsOne);
                break;
            case 1:
                fillMatrix(w, weightsTwo);
                break;
            default:
                break;
        }
    }

    public float[] GetLayer(int i)
    {
        switch (i)
        {
            case 1:
                return layerOne;
            case 2:
                return layerOutput;
            default:
                return null;
        }
    }

    public void SetLayer(float[] l, int i)
    {
        switch (i)
        {
            case 1:
                fillVector(l, layerOne);
                break;
            case 2:
                fillVector(l, layerOutput);
                break;
            default:
                break;
        }
    }
}
