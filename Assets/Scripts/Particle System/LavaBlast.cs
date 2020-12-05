using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlast : MonoBehaviour
{
    public static float defaultWarningTimer = 2.0f;
    public ParticleSystem lavaBurst;
    public GameObject player;
    public GameObject lavaFloor;
    public int heightModifier = -2;
    public float minIntervall = 9.0f;
    public float maxIntervall = 14.0f;
    public float speed = 80;
    public AudioSource audio;

    private bool enable = false;
    private float intervall = 0.0f;
    private float warningTimer = defaultWarningTimer;

    private ParticleSystem.MainModule pMain;
    private ParticleSystem.EmissionModule emissionModule;
   

    public GameObject hitBox;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        player = (GameObject)GameObject.FindGameObjectWithTag("Player");
        lavaBurst = GameObject.FindGameObjectWithTag("LavaBurstParticles").GetComponent<ParticleSystem>();
        lavaFloor = GameObject.FindGameObjectWithTag("Floor");
        

        lavaBurst.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        pMain = lavaBurst.main;
        emissionModule = lavaBurst.emission;
        hitBox.SetActive(false);
        intervall = Random.Range(minIntervall, maxIntervall);
       
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            Debug.Log("Collision!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }
        // Update is called once per frame
        void Update()
    {
       
        warningTimer -= Time.deltaTime;
        intervall -= Time.deltaTime;
        if (intervall <= 0.0f)
        {
            enable = true;
            intervall = Random.Range(minIntervall, maxIntervall);
            var Transform = transform.rotation;
            Transform.y = player.transform.rotation.y - 90;
            hitBox.transform.rotation = player.transform.rotation;
            hitBox.transform.position = player.transform.position;
        }
        if (warningTimer > 0.0f)
        {
            audio.Play();
            pMain.startSize = 5.0f;
            emissionModule.rateOverTime = 25;
            pMain.gravityModifier = -2;
        }

        else
        {
        
            hitBox.SetActive(true); 
            hitBox.transform.Translate(Vector3.up * Time.deltaTime * speed);
            pMain.startSize = 15.0f;
            pMain.startLifetime = 2.0f;
            emissionModule.rateOverTime = 200;
            pMain.gravityModifier = -5;
        }
        if (!lavaBurst.isEmitting)
        {
            hitBox.SetActive(false);
            warningTimer = defaultWarningTimer;
        }
        if (enable)
        {

            Vector3 newPosition = new Vector3(player.transform.position.x, lavaFloor.transform.position.y, player.transform.position.z)+player.transform.forward*-.1f;
            transform.position = newPosition;
     
            hitBox.transform.position = transform.position + Vector3.down * 60;



            lavaBurst.Play(true);
            enable = false;
        }

        //-2,0,0

    }
}
