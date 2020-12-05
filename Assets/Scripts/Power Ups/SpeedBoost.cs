using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Obstacle
{

    private PlayerMovement player;
    public float speedBonusTimeValue;
    public float speedFactor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        speedBonusTimeValue = 3.0f;
        speedFactor = 2.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player picked up speed boost");
            player.speedBonusTime = speedBonusTimeValue;
            player.speedModifier = speedFactor;
            Destroy(gameObject);
  
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetRadius(float radius)
    {
        body.position = new Vector3(radius, transform.position.y, 0);
    }
}
