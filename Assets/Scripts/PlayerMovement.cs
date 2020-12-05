using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public GameObject towerCenter;
    public float speed = 2.5f;
    public new GameObject camera;
    CameraFollow cfollow;
    private float turbo = 1.0f;
    public float speedModifier = 1.0f;
    public float invulnerabilityTimer = 0.0f;
    public float slowDownTime = 0.0f;
    public float speedBonusTime = 0.0f;
    public static bool closeToLava;

    public bool cinematicMode = false;

    private bool disableW = false;
    private bool disableS = false;
    private bool disableA = false;
    private bool disableD = false;

    // Start is called before the first frame update
    void Start()
    {
        cfollow = camera.GetComponent<CameraFollow>();
        closeToLava = false;
    }



    // super disgusting spaghetti code, clean up after Submission
    void Update()
    {
        //cinematicMode just pretends the player presses W (used in main menu)
        if (cinematicMode)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 3.0f * turbo * speedModifier);
            //Slowly adjust camera to look from below
            BGController.playerVertical = 1.0f;
            if (closeToLava == false)
            {
                cfollow.yOffset = Mathf.Lerp(cfollow.yOffset, 3, .9f * Time.deltaTime);
            }
            if (closeToLava)
            {
                cfollow.yOffset = Mathf.Lerp(cfollow.yOffset, -4f, .4f * Time.deltaTime);
                closeToLava = false;
            }
            BGController.playerVertical = Mathf.Lerp(0.1f, BGController.playerVertical, .95f);
            return;
        }   
        //invulnabilityTimer
        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }
        else
        {
            invulnerabilityTimer = 0.0f;
        }
        if(slowDownTime > 0.0f)
        {
            slowDownTime -= Time.deltaTime;
        }
        else if(slowDownTime <= 0.0f)
        {
            slowDownTime = 0.0f;
        }
        if(speedBonusTime > 0.0f)
        {
            speedBonusTime -= Time.deltaTime;
        }
        if(speedBonusTime <= 0.0f && slowDownTime <= 0.0f)
        {
            speedBonusTime = 0.0f;
            speedModifier = 1.0f;
        }
        /*
        if (Input.GetKey(KeyCode.LeftShift))
        {
            turbo = 10.0f;
        }
        */

        animator.speed = speedModifier;

        RayCastCollision();

        if (Input.GetKey("w") && !disableW)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed * turbo * speedModifier);
            //Slowly adjust camera to look from below
            BGController.playerVertical = 1.0f;
            if (closeToLava == false) 
                { 
                cfollow.yOffset = Mathf.Lerp(cfollow.yOffset, 3, .9f * Time.deltaTime); 
                }
            }
        if (Input.GetKey("s") && !disableS)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed * turbo * speedModifier);
            //Slowly adjust camera to look from above
            BGController.playerVertical = -1.0f;
            if (closeToLava == false)
            {
                cfollow.yOffset = Mathf.Lerp(cfollow.yOffset, -3, .9f * Time.deltaTime);
            }
            //animator.speed = animator.speed * speedModifier * -1.0f;
        }
        if (Input.GetKey("a") && !disableA)
        {
            transform.RotateAround(towerCenter.transform.position, new Vector3(0,1,0), 35 * Time.deltaTime * speed * Mathf.Clamp(speedModifier,1.0f,1.2f));
            BGController.playerHorizontal = 1.0f;
        }
        if (Input.GetKey("d") && !disableD)
        {
            transform.RotateAround(towerCenter.transform.position, new Vector3(0,-1,0), 35 * Time.deltaTime * speed * Mathf.Clamp(speedModifier, 1.0f, 1.2f));
            BGController.playerHorizontal = -1.0f;
        }
        if (!Input.anyKey)
        {
            animator.speed = 0.0f;
        }

        //something is causing camera jagginess, not sure yet what
        if (closeToLava)
        {
            cfollow.yOffset = Mathf.Lerp(cfollow.yOffset, -4f, .4f * Time.deltaTime);
            closeToLava = false;
        }

        BGController.playerHorizontal = Mathf.Lerp(0.1f, BGController.playerHorizontal, .95f);
        BGController.playerVertical = Mathf.Lerp(0.1f, BGController.playerVertical, .95f);
        //turbo = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("colliding with " + other.ToString()+" at "+Time.frameCount);
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit collision with " + other.ToString() + " at " + Time.frameCount);
    }

    //raycast in the 4 movement directions
    //if a collision happens that movement direction is disabled
    private void RayCastCollision()
    {
        LayerMask layerMask =~ LayerMask.GetMask("PowerUp");
        Vector3 playerCenter = transform.position + transform.up * .35f + transform.right * .35f;
        disableW = Physics.Raycast(playerCenter, transform.up, .5f, layerMask);
        //Debug.DrawRay(playerCenter, transform.up * .5f, Color.red, 100f, false);
        disableS = Physics.Raycast(playerCenter, transform.up*-1.0f, .5f, layerMask);
        //Debug.DrawRay(playerCenter, transform.up * -.5f, Color.red, 100f, false);
        disableA = Physics.Raycast(playerCenter, transform.right*-1.0f+transform.forward*-.1f, .5f, layerMask);
        //Debug.DrawRay(playerCenter, transform.right * -.5f, Color.red, 100f, false);
        disableD = Physics.Raycast(playerCenter, transform.right + transform.forward * -.1f, .5f, layerMask);
        //Debug.DrawRay(playerCenter, transform.right * .5f, Color.red, 100f, false);
        //Debug.Log("Up:"+disableW+". Down:"+disableS + ". Left:" + disableA + ". Right:" + disableD);
    }
}
