using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject towerCurrent;
    public GameObject towerPrefab;
    public Transform playerTransform;
    public Transform topCenter;
    public Transform floorTransform;
    public float floorSpeed;

    private float towerHeight;
    private GameObject towerTemp;
    private bool generateCooldown = false;
    private Vector3 defaultMovement;
    private float playerY;
    private float floorY;
    private Vector3 rubberband;

    private Queue<GameObject> towerQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        towerHeight = towerCurrent.GetComponent<TowerConfig>().towerHeight;
        //queues the Towers up. FIFO
        Vector3 spawnPos = topCenter.position - Vector3.up * towerHeight * 4 - Vector3.left * 2;
        for(int i=0; i<=5; i++)
        {
            towerQueue.Enqueue(Instantiate(towerPrefab, spawnPos, Quaternion.identity));
        }
        //initial Starting Segment goes in last
        towerQueue.Enqueue(towerCurrent);
        topCenter.position = towerCurrent.transform.position;
        topCenter.Translate(Vector3.up * towerHeight);
    }

    // Update is called once per frame
    void Update()
    {
        autoScrollTower();
        autoScrollLava();
        checkLoseCondition();
    }

    void autoScrollTower()
    {

        if((topCenter.position.y - playerTransform.position.y) <= towerHeight && generateCooldown==false)
        {
            generateCooldown = true;
            generateNextSegment();
        }

        if(playerTransform.position.y > (topCenter.position.y-towerHeight))
        {
            generateCooldown = false;
        }
    }

    //move an older/lower Segment to the Top
    void generateNextSegment()
    {
        //take the oldest tower from queue, set it to current, move it up
        towerTemp = towerQueue.Dequeue();
        Vector3 updatePosition = topCenter.position;
        towerTemp.transform.position = updatePosition;
        towerCurrent = towerTemp;
        towerQueue.Enqueue(towerTemp);
        
        //raise the topCenter (spawn place for the next tower)
        topCenter.position = towerCurrent.transform.position;
        topCenter.Translate(Vector3.up * towerHeight);
    }

    //rubberbands the lava to the player
    // Percentage of the difference between player and floor + defaultMovement (usually 0,1,0) -> clamped between 1x and 10x base-speed
    void autoScrollLava()
    {
        defaultMovement = Vector3.up * floorSpeed;
        floorY = floorTransform.position.y;
        playerY = playerTransform.position.y;
        rubberband.y = Mathf.Clamp((playerY - floorY) * 0.4f + defaultMovement.y, floorSpeed, floorSpeed*10.0f);
        floorTransform.Translate(rubberband*Time.deltaTime);
    }

    void checkLoseCondition()
    {
        if(playerTransform.position.y < floorTransform.position.y - .2f)
        {
            GameObject.FindWithTag("GameController").GetComponent<LoseCondition>().EndGame();
        }
    }
}
