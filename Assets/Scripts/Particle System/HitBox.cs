using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
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
        if (other.tag == "PlayerHitBox" && player.invulnerabilityTimer <= 0.0f)
        {
                gameObject.SetActive(false);
                player.slowDownTime = slowDownTimeValue;
                player.speedModifier = speedModifierValue;
                player.invulnerabilityTimer = invulnabilityTime;
                Debug.Log("Player Hit by LavaBurst!");
                
        }
           
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
