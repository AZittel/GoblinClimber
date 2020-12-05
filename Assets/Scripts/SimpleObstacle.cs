using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObstacle : Obstacle
{


    public static List<SimpleObstacle> usedObstacles;
    public static List<SimpleObstacle> unUsedObstacles;


    public override void SetRadius(float radius)
    {
        

        body.position = new Vector3(radius, transform.position.y, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
