using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegressionHandler : MonoBehaviour
{
    private const int depth = 10;
    private const int inputNodeCount = 2; // mirrored in NerualNetwork.cs
    private const int hiddenNodeCount = 4; // mirrored in NerualNetwork.cs
    private const int outputNodeCount = 4; // mirrored in NerualNetwork.cs
    private int histIdx = -1;
    private int elements = 0;
    private NeuralNetwork[] networkHistory = new NeuralNetwork[depth];
    private float[][] inputHistory = new float[depth][];

    public float fudge = 1.4F;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < depth; i++) {
            inputHistory[i] = new float[inputNodeCount];
        }
    }

    public void Push(NeuralNetwork n, float[] i)
    {
        histIdx++;
        networkHistory[histIdx % depth] = n;
        for (int j = 0; j < inputNodeCount; j++)
        {
            inputHistory[histIdx % depth][j] = i[j];
        }
        if (elements < depth)
        {
            elements++;
        }

        while (histIdx >= depth)
        {
            histIdx -= depth;
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

    public NeuralNetwork RunRegression(float[] finalInputs)
    {
        histIdx += depth; // hack to avoid having to re-wrap when we traverse backwards through the ring buffer
        int finalState = networkHistory[histIdx % depth].CalculateTransition(finalInputs);

        float[][] testMatrix = new float[hiddenNodeCount][];
        for (int i = 0; i < hiddenNodeCount; i++) {
            testMatrix[i] = new float[outputNodeCount];
        }

        for (int i = 0; i < depth; i++)
        {
            NeuralNetwork currentNetwork = Instantiate(networkHistory[histIdx % depth]);
            int currentState = currentNetwork.CalculateTransition(inputHistory[histIdx % depth]);

            int yConcern = 0;

            switch(currentState)
            {
                case 0:
                    yConcern = 0;
                    break;
                case 1:
                    yConcern = 1;
                    break;
                case 2:
                    yConcern = 2;
                    break;
                case 3:
                    yConcern = 3;
                    break;
                default:
                    break;
            }

            do {
                fillMatrix(currentNetwork.GetWeights(1), testMatrix);
                for (int x = 0; x < hiddenNodeCount; x++) {
                    for (int y = 0; y < outputNodeCount; y++) {
                        if (y != yConcern) {
                            testMatrix[x][y] *= fudge;
                        }
                    }
                }
                currentNetwork.SetWeights(testMatrix, 1);
            } while (currentState != currentNetwork.CalculateTransition(inputHistory[histIdx % depth]));

            histIdx--;
        }
        return networkHistory[histIdx];
    }
}
