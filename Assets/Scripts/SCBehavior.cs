using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SCBehavior : MonoBehaviour
{
    public bool mutateMutations = true;
    public float mutationAmount = 0.8f;
    public float mutationChance = 0.2f;
    private bool isMutated = false;
    public float[] distances = new float[6];
    public float viewDistance = 20f;
    public float LR = 0f;
    public float FB = 0f;
    public float Rot = 0f;  
    private bool colDetection; 

    public NeuralNetwork nn;
    public SingleCellMovement scm;
    public SingleCellEnergy sce;




    public void Awake()
    {
        nn = gameObject.GetComponent<NeuralNetwork>();
        scm = gameObject.GetComponent<SingleCellMovement>();
        sce = gameObject.GetComponent<SingleCellEnergy>();
        distances = new float[6];
        

    }

    public void FixedUpdate()
    {
        if (!isMutated)
        {
            MutateCell();
            isMutated = true;
        }
        int numRayCasts = 5;
        float angleBetweenRaycasts = 30f;

        //float[] distances = new float[numRayCasts];

        for (int i = 0; i < numRayCasts; i++)
        {
            float angleStep = angleBetweenRaycasts / (numRayCasts - 1);
            int layerMask = ~(1 << gameObject.layer);
            float angle = i * angleStep - angleBetweenRaycasts / 2;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 rayDirection = rotation * transform.up;
            Vector2 rayStart = (Vector2)transform.position + Vector2.up * 0f;
            RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection, viewDistance, layerMask);

            if (hit.collider != null)
            {
                Debug.DrawRay(rayStart, rayDirection * viewDistance, Color.red);
                distances[i] = hit.distance / viewDistance;
                Debug.Log("Raycast hit");

            }
            else
            {
                Debug.DrawRay(rayStart, rayDirection * viewDistance, Color.red);
                distances[i] = 1;
                //Debug.Log("Raycast has not found another Object");

            }


        }
        float energy = sce.cellEnergyLevel;
        colDetection = scm.singleCellEating; 
        Debug.Log(colDetection.ToString()); //this always returns false, need to fix at some point. 
        float[] inputsToNN = distances;
        float[] inputsToNetwork = inputsToNN
            .Concat(new float[] { colDetection ? 1 : 0 })
            .Concat(new float[] { energy })
            .ToArray();
        //Debug.Log(inputsToNNAdded.Length);
        float[] outputsFromNN = nn.Brain(inputsToNetwork);
        //Debug.Log(outputsFromNN.Length);
        FB = outputsFromNN[0];
        LR = outputsFromNN[1];
        Rot = outputsFromNN[2];
        
        scm.cellMovement(FB, LR, Rot);



    }
    public void MutateCell()
    {
        if (mutateMutations)
        {
            mutationAmount += Random.Range(-1.0f, 1.0f) / 100;
            mutationChance += Random.Range(-1.0f, 1.0f) / 100;
        }
        mutationAmount = Mathf.Max(mutationAmount, 0);
        mutationChance = Mathf.Max(mutationChance, 0);

        nn.MutateNetwork(mutationAmount, mutationChance);
    }
}
