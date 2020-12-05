using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{

    private PlayerMovement player;
    public float slowDownTimeValue;
    public float speedModifierValue;
    public float invulnabilityTime;

    // Start is called before the first frame update
    void Start()
    {
        slowDownTimeValue = 2.0f;
        speedModifierValue = 0.3f;
        invulnabilityTime = 1.5f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {  
            CoinMov coin = other.gameObject.GetComponent<CoinMov>();
            Destroy(other.gameObject);
        }
        if(other.tag == "obstacleBody" && player.invulnerabilityTimer > 0.0f)
        {
            Destroy(other.gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
