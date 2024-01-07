using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCellEnergy : MonoBehaviour
{
    public bool plantCellEnergyLevel = false;
    public float energy;
    

    void Start()
    {
        energy = 1;
        InvokeRepeating("energyCounter", 0f, 2f);
    }

    void energyCounter()
    {
        
        if (energy < 25)
        {
            energy += 1f;
            plantCellEnergyLevel = false;
            
        }
        else if (energy >= 25)
        {
            if (!plantCellEnergyLevel)
            {
                plantCellEnergyLevel=true;
                energy = 1; 

            }
        }
    }
}
