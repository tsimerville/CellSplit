using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    int[] networkShape = {7, 32, 3};
    public Layer[] layers;
    public void Awake()
    {
        layers = new Layer[networkShape.Length - 1];
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = new Layer(networkShape[i], networkShape[i + 1]);
        }
        Random.InitState((int)System.DateTime.Now.Ticks);
    }
    public float[] Brain(float[] inputs)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (i == 0)
            {
                layers[i].Forward(inputs);
                layers[i].Activation();
            }
            else if (i == layers.Length - 1)
            {
                layers[i].Forward(layers[i - 1].nodeArray);
            }
            else
            {
                layers[i].Forward(layers[i - 1].nodeArray);
                layers[i].Activation();
            }
        }

        //Debug.Log($"Output layer size: {layers[layers.Length - 1].nodeArray.Length}");
        //Debug.Log($"Output layer values: {string.Join(", ", layers[layers.Length - 1].nodeArray)}");

        return (layers[layers.Length - 1].nodeArray);
    }

    public Layer[] CopyLayers()
    {
        Layer[] tmpLayers = new Layer[networkShape.Length - 1];
        for (int i = 0; i < layers.Length; i++)
        {
            tmpLayers[i] = new Layer(networkShape[i], networkShape[i + 1]);
            System.Array.Copy(layers[i].weightsArray, tmpLayers[i].weightsArray, layers[i].weightsArray.GetLength(0) * layers[i].weightsArray.GetLength(1));
            System.Array.Copy(layers[i].biasesArray, tmpLayers[i].biasesArray, layers[i].biasesArray.GetLength(0));
        }
        return (tmpLayers);
    }
    public class Layer
    {
        public float[,] weightsArray;
        public float[] biasesArray;
        public float[] nodeArray;

        private int n_inputs;
        private int n_neurons;

        public Layer(int n_inputs, int n_neurons)
        {
            this.n_neurons = n_neurons;
            this.n_inputs = n_inputs;

            weightsArray = new float[n_neurons, n_inputs];
            biasesArray = new float[n_neurons];
            

        }
        public void Forward(float[] inputsArray)
        {
            nodeArray = new float[n_neurons];

            for(int i = 0; i < n_neurons; i++)
            {
                for(int j = 0; j < n_inputs; j++)
                {
                    nodeArray[i] += weightsArray[i, j] * inputsArray[j];
                }

                nodeArray[i] += biasesArray[i];
            }
        }

        public void Activation()
        {
            for (int i = 0; i < nodeArray.Length; i++)
            {
                if (nodeArray[i] < 0)
                {
                    nodeArray[i] = 0;
                }
            }
        }


        public void MutateLayer(float mutationChance, float mutationAmount)
        {
            for (int i = 0; i < n_neurons; i++)
            {
                for (int j = 0; j < n_inputs; j++)
                {
                    if(Random.value < mutationChance)
                    {
                        weightsArray[i, j] += Random.Range(-1.0f, 1.0f) * mutationAmount; 

                    }
                }
                if(Random.value > mutationChance)
                {
                    biasesArray[i] += Random.Range(-1.0f, 1.0f) * mutationAmount; 
                }
            }
        }
        

    }
   

   
   
    public void MutateNetwork(float mutationChance, float mutationAmount)
    {
        for(int i = 0; i < layers.Length; i++)
        {
            layers[i].MutateLayer(mutationChance, mutationAmount);
        }
    }
}
