using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCellMovement : MonoBehaviour
{
    private float speed;
    private float rotateSpeed;
    public bool singleCellEating = false; 
    private SingleCellEnergy scEnergy; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform cellCheck;
    [SerializeField] private LayerMask plantCellLayer;
    [SerializeField] private Collider2D myCollider; 
    public int viewDistance = 20;
    //public int numRaycasts = 5;
    //public float angleBetweenRaycasts = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //cellMovement(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        InvokeRepeating("CellEating", 0f, .5f);
        scEnergy = GetComponent<SingleCellEnergy>();
        myCollider = GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
        //int numRayCasts = 5;
        //int angleBetweenRaycasts = 30; 
        //CreateRaycasts(numRayCasts, angleBetweenRaycasts);
    }
    void CellEating()
    {
        singlecellCheck();
    }

    public void cellMovement(float FB, float LR, float Rot)
    {
        speed = 150f;
        rotateSpeed = 150f; 
        LR = Mathf.Clamp(LR, -1, 1);
        FB = Mathf.Clamp(FB, -1, 1);
        Rot = Mathf.Clamp(Rot, -1, 1);  
        Vector2 movement = new Vector2(FB, LR) * speed * Time.fixedDeltaTime;
        rb.velocity = movement;
        // float rotation = LR * rotateSpeed * Time.deltaTime;
        // rb.angularVelocity = rotation;  
        float rotation = Rot * rotateSpeed * Time.fixedDeltaTime;
        rb.angularVelocity = rotation; 
    }

    public bool singlecellCheck()
    {
        
        Collider2D cellCollider = Physics2D.OverlapCircle(cellCheck.position, .6f, plantCellLayer);
        if (cellCollider != null && cellCollider.gameObject != gameObject)
        {
            if (cellCollider.gameObject.tag == "Plant" && cellCollider.gameObject.tag != null && !singleCellEating)
            {
               singleCellEating = true;
               Debug.Log("Cell is eating");
               PlantCellEnergy collidedCell = cellCollider.gameObject.GetComponent<PlantCellEnergy>();
               float preyEnergy = collidedCell.energy;
                if (preyEnergy > 0)
                {
                    preyEnergy -= 1f;
                    Debug.Log(collidedCell.name + " energy is " + preyEnergy);
                    collidedCell.energy = preyEnergy;
                    scEnergy.cellEnergyLevel ++;
                    singleCellEating = false;
                    Debug.Log("Cell Finished Eating");
                }
                else 
                {
                    Destroy(cellCollider.gameObject);
                    singleCellEating = false; 
                }
            }
            return true;
            
        }
        return false;
    }
    
//    public float[] CreateRaycasts(int numRayCasts, float angleBetweenRaycasts) 
//    {
//        float [] distances = new float[numRayCasts];
//        int layerMask = ~(1 << gameObject.layer); 
//        float angleStep = angleBetweenRaycasts / (numRayCasts - 1);
//        for (int i = 0; i < numRayCasts; i++)
//        {
//            float angle = i * angleStep - angleBetweenRaycasts / 2; 
//            //float angle = ((2 * i + 1) / numRayCasts) * (angleBetweenRaycasts / 2);
//            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//            Vector2 rayDirection = rotation * transform.up;
//            Vector2 rayStart = (Vector2)transform.position + Vector2.up * 0f; 
//            RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection, viewDistance, layerMask);
//           
//            if (hit.collider != null)
//            {
//                Debug.DrawRay(rayStart, rayDirection * viewDistance, Color.red);
//                distances[i] = hit.distance;
//                Debug.Log("Raycast hit");
//
//            }
//            else
//            {
//                Debug.DrawRay(rayStart, rayDirection * viewDistance, Color.red);
//                distances[i] = viewDistance;
//                Debug.Log("Raycast has not found another Object");
//
//            }
//            
//        }
//        return distances;
//
//    }
        


}

