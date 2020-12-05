using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCrawler : Obstacle


{

    public float maxSpeed = 0.4f;
    private float speed;
    public static List<HorizontalCrawler> usedObstacles;
    public static List<HorizontalCrawler> unUsedObstacles;



    // Start is called before the first frame update
    void Start()
    {
       
        speed = Random.Range(maxSpeed / 2, maxSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Rotate(Vector3.up,speed);
    }

    public override void SetRadius(float radius)
    {
        

        body.position = new Vector3(radius, transform.position.y, 0);
    }
}
