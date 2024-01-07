using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantCellMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform plantCheck;
    [SerializeField] private LayerMask plantLayer;
    private bool isMovingTowardsPlant;
    private List<GameObject> plantsToMoveTowards; 

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        CellController cellController = GetComponentInParent<CellController>();
        if (cellController != null )
        {
            plantsToMoveTowards = cellController.cellPrefabs; 
        }
        else
        {
            Debug.Log("cellController is Null");
        }
    }

    void FixedUpdate()
    {
        speed = 2f; 
        Vector2 sumOfPostions = Vector2.zero;
        int nearbyCellsCount = 0;

        foreach (GameObject plantCell in plantsToMoveTowards)
        {
            if ( plantCell != null )
            {
                Vector2 plantLocation = plantCell.transform.position;
                Vector2 thisPlantCellLocation = transform.position;
                float cellDistance = Vector2.Distance(thisPlantCellLocation, plantLocation);
                if (cellDistance < 20f && cellDistance > 1f)
                {
                    sumOfPostions += plantLocation;
                    nearbyCellsCount++;
                }
            }
            
        }
        if (nearbyCellsCount > 0)
        {
            Vector2 averagePositions = sumOfPostions / nearbyCellsCount;
            Vector2 direction = averagePositions - (Vector2)transform.position;

            rb.velocity = direction * speed * Time.fixedDeltaTime;
        //    if (plantsNear())
        //    {
        //        Debug.Log("I'm Near Plants");
        //        rb.velocity = Vector2.zero;
        //        isMovingTowardsPlant = false;
        //    }
        //    else
        //    {
        //        isMovingTowardsPlant = true;
        //    }
        }
        
    }
    //private void OnDrawGizmos()
   //{
        //Gizmos.DrawWireSphere(plantCheck.position, 0.51f);
   // }
 //   private bool plantsNear()
 //   {
 //       Collider2D plantCollider = Physics2D.OverlapCircle(plantCheck.position, .51f, plantLayer);
 //       //OnDrawGizmos();
 //       if (plantCollider != null && plantCollider.gameObject != gameObject)
 //       {
 //           Debug.Log("plants touching");
 //           //Gizmos.color = Color.red;
 //           //Gizmos.DrawLine(transform.position, plantCollider.transform.position);
 //           return true;
 //       }
 //       return false;
 //   }
    


}

    