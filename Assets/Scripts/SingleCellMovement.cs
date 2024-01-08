using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCellMovement : MonoBehaviour
{
    private float speed;
    private float rotateSpeed; 
    private SingleCellEnergy scEnergy; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform cellCheck;
    [SerializeField] private LayerMask singleCellLayer;
    [SerializeField] private Collider2D myCollider; 
    public int viewDistance = 20;
    //public int numRaycasts = 5;
    //public float angleBetweenRaycasts = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cellMovement(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        InvokeRepeating("CellEating", 0f, .5f);
        scEnergy = GetComponent<SingleCellEnergy>();
        myCollider = GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
        CreateRaycasts(5, 30f);
    }
    void CellEating()
    {
        singlecellCheck();
    }

    public void cellMovement(float FB, float LR)
    {
        speed = 50f;
        rotateSpeed = 50f; 
        LR = Mathf.Clamp(LR, -1, 1);
        FB = Mathf.Clamp(FB, -1, 1);
        Vector2 movement = new Vector2(FB, LR) * speed * Time.deltaTime;
        rb.velocity = movement;
        float rotation = LR * rotateSpeed * Time.deltaTime;
        rb.angularVelocity = rotation;  
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
    
    public float[] CreateRaycasts(int numRayCasts, float angleBetweenRaycasts) 
    {
        float [] distances = new float[numRayCasts];
        int layerMask = ~(1 << gameObject.layer); 
        float angleStep = angleBetweenRaycasts / (numRayCasts - 1);
        for (int i = 0; i < numRayCasts; i++)
        {
            float angle = i * angleStep; 
            //float angle = ((2 * i + 1) / numRayCasts) * (angleBetweenRaycasts / 2);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 rayDirection = rotation * transform.up;
            Vector2 rayStart = (Vector2)transform.position + Vector2.up * 0.1f; 
            RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection, viewDistance, layerMask);
            if (hit.collider != null)
            {
                Debug.DrawRay(rayStart, rayDirection * viewDistance, Color.red);
                distances[i] = hit.distance;
                Debug.Log("Raycast has hit another Object");

            }
            else
            {
                Debug.DrawRay(rayStart, rayDirection * viewDistance, Color.red);
                distances[i] = viewDistance;
                Debug.Log("Raycast has not found another Object");
                
            }
        }
        return (distances); 

    }
}

