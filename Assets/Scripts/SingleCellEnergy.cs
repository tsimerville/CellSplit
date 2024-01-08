using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class SingleCellEnergy : MonoBehaviour
{
    public bool singleCellEnergyLevel = false;
    public float cellEnergyLevel = 25;
    private float timer = 0f;
    private float mitosisThreshold = 50f;
    public bool singleCellMitosisTrigger = false; 
     
    

    void Start()
    {
        InvokeRepeating("EnergyCountDown", 0f, 2f);
    }

    void Update()
    {
        if (cellEnergyLevel > 0)
        {
            timer += Time.deltaTime;
            if (timer > mitosisThreshold)
            {
                singleCellMitosisTrigger = true; 
                timer = 0f;
                //Debug.Log("Mitosis Ready"); 
            }
        }
        else
        {
            timer = 0f;
            singleCellMitosisTrigger = false;
        }
    }

    void EnergyCountDown()
    {
        if (cellEnergyLevel > 0)
        {
            cellEnergyLevel -= 1; 
            singleCellEnergyLevel = false;
            //Debug.Log("Singcell Energy is " + cellEnergyLevel);
        }
        if (cellEnergyLevel == 0) 
        {
            singleCellEnergyLevel = true;
            Destroy(gameObject);
        }
    }
}
