using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCellSpawner : MonoBehaviour
{
    public GameObject singleCellPrefab;
    private GameObject[] singleCellPrefabs;
    [SerializeField] private List<GameObject> scellPrefabs = new List<GameObject>();
    private int singleCellNumber = 0; 

    private void FixedUpdate()
    {
        singleCellPrefabs = GameObject.FindGameObjectsWithTag("SingleCell");

        if(singleCellPrefabs.Length < 1)
        {
            SpawnSingleCell();
        }
        SingleCellMitosis();

    }
    private void Update()
    {
          CleanupDestroyedObjects();
    }

    void SpawnSingleCell()
    {
        float randomXValue = UnityEngine.Random.Range(-75f, 75f);
        float randomYValue = UnityEngine.Random.Range(-75f, 75f);
        GameObject newSingleCell = Instantiate(singleCellPrefab, new Vector2(randomYValue, randomXValue), Quaternion.identity);
        newSingleCell.name = "Single Cell " + singleCellNumber++; 
        scellPrefabs.Add(newSingleCell);
        newSingleCell.transform.parent = transform;
    }

    void SingleCellMitosis()
    {
        List<int> mitosisReadyIndices = new List<int>();
        for (int i = 0; i < scellPrefabs.Count; i++)
        {
            SingleCellEnergy mitosisReady = scellPrefabs[i].GetComponent<SingleCellEnergy>();
            bool mitosisTriggerReady = mitosisReady.singleCellMitosisTrigger;

            if (mitosisTriggerReady)
            {
                mitosisReadyIndices.Add(i); 
            }
        }
        foreach (int index in mitosisReadyIndices)
        {
            GameObject singleCell = scellPrefabs[index];
            SingleCellEnergy mitosisReady = singleCell.GetComponent<SingleCellEnergy>();

            Vector2 spawnlocation = singleCell.transform.position;
            GameObject newSingleCell = Instantiate(singleCellPrefab, new Vector2(spawnlocation.x + UnityEngine.Random.Range(-1f, 1f), spawnlocation.y + UnityEngine.Random.Range(-1f, 1f)), Quaternion.identity);
            newSingleCell.name = "Single Cell " + singleCellNumber++;
            scellPrefabs.Add(newSingleCell);
            newSingleCell.transform.parent = singleCell.transform;
            mitosisReady.singleCellMitosisTrigger = false; 



        }
    }

    void CleanupDestroyedObjects()
    {
        int cellPrefabsLength = scellPrefabs.Count;
        for (int i = cellPrefabsLength - 1; i >= 0; i--)
        {
            if (scellPrefabs[i] == null)
            {
                scellPrefabs.RemoveAt(i);
            }

        }
    }
}
