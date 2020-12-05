using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingObstacle : Obstacle
{

    public float speed = 1000;
    private bool direction;
    public float minSize = 0.5f;
    public float maxSize = 1.3f;
    public static List<TimingObstacle> usedObstacles;
    public static List<TimingObstacle> unUsedObstacles;

    public override void SetRadius(float radius)
    {

      
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
            transform.localScale *= (1 + speed * Time.deltaTime);
            if (transform.localScale.x >= maxSize)
            {
                direction = !direction;
            }
        }
        else
        {
            transform.localScale *= (1 - speed * Time.deltaTime);
            if (transform.localScale.x <=minSize)
            {
                direction = !direction;
            }
        }
    }
}
