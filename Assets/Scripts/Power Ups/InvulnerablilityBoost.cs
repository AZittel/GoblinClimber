using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulnerablilityBoost : Obstacle
{
    private PlayerMovement player;
    public float invulnerableTime;
    public float speedFactor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        invulnerableTime = 4.0f;
        speedFactor = 2.0f;
        body = this.transform;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player picked up invulnerability");
            player.invulnerabilityTimer = invulnerableTime;
            player.speedBonusTime = invulnerableTime;
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
        transform.position = new Vector3(radius, transform.position.y, 0);
    }
}
