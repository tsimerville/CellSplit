using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCellMovement : MonoBehaviour
{
    private float speed;
    private SingleCellEnergy scEnergy; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform cellCheck;
    [SerializeField] private LayerMask singleCellLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cellMovement(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        InvokeRepeating("CellEating", 0f, .5f);
        scEnergy = GetComponent<SingleCellEnergy>();
    }

    void CellEating()
    {
        singlecellCheck();
    }

    public void cellMovement(float FB, float LR)
    {
        speed = 5f; 
        LR = Mathf.Clamp(LR, -1, 1);
        FB = Mathf.Clamp(FB, -1, 1);
        Vector2 direction = new Vector2(LR,FB);
        rb.velocity = direction * speed * Time.fixedDeltaTime; 
    }

    private bool singlecellCheck()
    {
        
        Collider2D cellCollider = Physics2D.OverlapCircle(cellCheck.position, .6f, singleCellLayer);
        if (cellCollider != null && cellCollider.gameObject != gameObject)
        {
            if (cellCollider.gameObject.tag == "Plant" && cellCollider.gameObject.tag != null)
            {
               PlantCellEnergy collidedCell = cellCollider.gameObject.GetComponent<PlantCellEnergy>();
               float preyEnergy = collidedCell.energy;
                if (preyEnergy > 0)
                {
                    preyEnergy -= 1f;
                    Debug.Log(collidedCell.name + " energy is " + preyEnergy);
                    collidedCell.energy = preyEnergy;
                    scEnergy.cellEnergyLevel ++; 
                }
                else 
                {
                    Destroy(cellCollider.gameObject);
                }
            }
            return true;   
        }
        return false;
    }

    private void cellEating(GameObject collidedCell)
    {
        if (collidedCell != null)
        {
            Debug.Log("Eating " + collidedCell.name);
        }
    }
}

