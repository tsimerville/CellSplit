using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public GameObject plantCellPrefab; 
    public List<GameObject> cellPrefabs = new List<GameObject>();
    private int plantCellNumber = 0;

    void Start()
    {
        InvokeRepeating("firstPlantCells", 0f, 1f);

    }

    void Update()
    {
        CleanupDestroyedObjects();
    }
    private void mistosisSpawn()
    {
        List<GameObject> cellsToDuplicate = new List<GameObject>(cellPrefabs);
        foreach (GameObject originalPlant in cellsToDuplicate)
        {
            PlantCellEnergy plantCellEnergy = originalPlant.GetComponent<PlantCellEnergy>();
            if (plantCellEnergy != null && plantCellEnergy.plantCellEnergyLevel)
            {
                Vector2 spawnlocation = originalPlant.transform.position;
                plantCellEnergy.plantCellEnergyLevel = false;
                GameObject newPlants = Instantiate(plantCellPrefab, new Vector2(spawnlocation.x + UnityEngine.Random.Range(-1f, 1f), spawnlocation.y + UnityEngine.Random.Range(-1f, 1f)), Quaternion.identity);
                newPlants.name = "Plant Cell " + plantCellNumber++;
                cellPrefabs.Add(newPlants);
                newPlants.transform.parent = originalPlant.transform;
                 
            }
        }
    }
    void firstPlantCells()
    {
        int cellPrefabsLength = cellPrefabs.Count;
        if (cellPrefabsLength <= 25 ) 
        {
            float randomXValue = UnityEngine.Random.Range(-75f, 75f);
            float randomYValue = UnityEngine.Random.Range(-75f, 75f);
            GameObject newPlants = Instantiate(plantCellPrefab, new Vector2(randomYValue, randomXValue), Quaternion.identity);
            newPlants.name = "Plant Cell " + plantCellNumber++; 
            cellPrefabs.Add(newPlants);
            newPlants.transform.parent = transform;  
        }
        if (cellPrefabsLength <= 125)
        {
            mistosisSpawn();
        }
    }
    void CleanupDestroyedObjects()
    {
        int cellPrefabsLength = cellPrefabs.Count; 
        for (int i = cellPrefabsLength - 1; i >= 0; i--)
        {
            if (cellPrefabs[i] == null)
            {
                cellPrefabs.RemoveAt(i);
            }

        }
    }
  
}
