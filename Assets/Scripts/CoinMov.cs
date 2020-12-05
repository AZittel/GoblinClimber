using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMov : Obstacle
{
    public float speed;
   

    public override void SetRadius(float radius)
    {
        transform.position = new Vector3(radius, transform.position.y, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
       
        body = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 60f);

    }
}
