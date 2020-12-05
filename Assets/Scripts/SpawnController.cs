using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    //the difference between the rotation of two walls in degree
    public int deltaWallRotation = 10;

    //the number of obstacles generated on startup
    public int numberObstaclesGenerated = 100;

    //current difficulty
    private int level = 0;

    //the obstacles that can be generated
    public TimingObstacle timingObstacle;
    public SimpleObstacle simpleObstacle;
    public WallObstacle wallObstacle;
    public HorizontalCrawler horizontalCrawler;
    public SpeedBoost speedBoost;
    public InvulnerablilityBoost invulnerablilityBoost;
    public CoinMov coin;

    //the overall density of Obstacles. Higher value equals less obstacles, first field of array belongs to first level and so on
    public float[] obstacleDistance;

    //the player transform
    public Transform player;

    //the height up to which the obstacles have already been generated
    private float generatedHeight=0;

    //the heigth of the first obstacle
    public float startHeigth;


    //the amount of obstacles to be generated at once
    public float chunkSize;


    //the diameter of the tower
    public float towerDiameter;

    //the point the game rotates around
    public Transform rotationPoint;

    //the distance the player has to climb before the difficulty will increase
    public float difficultyIncreaseDistance=50;

    //the point at which the next increase in difficulty will happen
    private float nextIncrease=50;


    //percentage of powerups
    public int powerUpChance=5;

    //percentage of coins
    public int coinChance = 5;


    // Start is called before the first frame update
    void Start()
    {

        TimingObstacle.unUsedObstacles = new List<TimingObstacle>();
        TimingObstacle.usedObstacles = new List<TimingObstacle>();
        SimpleObstacle.usedObstacles = new List<SimpleObstacle>();
        SimpleObstacle.unUsedObstacles = new List<SimpleObstacle>();
        HorizontalCrawler.unUsedObstacles = new List<HorizontalCrawler>();
        HorizontalCrawler.usedObstacles = new List<HorizontalCrawler>();
        WallObstacle.usedObstacles = new List<WallObstacle>();
        WallObstacle.unUsedObstacles = new List<WallObstacle>();


        
        for (int i = 0; i < numberObstaclesGenerated; i++)
        {

            TimingObstacle t=Instantiate<TimingObstacle>(timingObstacle, generatePos(rotationPoint.position.y), Quaternion.identity);
            TimingObstacle.unUsedObstacles.Add(t);
            t.SetRadius(towerDiameter);
            HorizontalCrawler h=Instantiate<HorizontalCrawler>(horizontalCrawler, generatePos(rotationPoint.position.y), Quaternion.identity);
            HorizontalCrawler.unUsedObstacles.Add(h);
            h.SetRadius(towerDiameter);
            WallObstacle w=Instantiate<WallObstacle>(wallObstacle, generatePos(rotationPoint.position.y), Quaternion.identity);
            WallObstacle.unUsedObstacles.Add(w);
            w.SetRadius(towerDiameter);
            SimpleObstacle s=Instantiate<SimpleObstacle>(simpleObstacle, generatePos(rotationPoint.position.y), Quaternion.identity);
            SimpleObstacle.unUsedObstacles.Add(s);
            s.SetRadius(towerDiameter);
        }

       generatedHeight=startHeigth;

        GenerateChunk();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (player.transform.position.y > generatedHeight - chunkSize * obstacleDistance[level] / 2)
        {
            
            GenerateChunk();
        }
        if (player.position.y >= nextIncrease)
        {
            if (level < obstacleDistance.Length-1)
            {
                level++;
            }
            
            nextIncrease = player.position.y + difficultyIncreaseDistance;
           
        }
    }

    private Vector3 generatePos(float height)
    {
        
        

        return new Vector3(rotationPoint.position.x, -10, rotationPoint.position.z);



    }



    private void GenerateChunk()
    {
        Debug.Log(WallObstacle.unUsedObstacles.Count + "  " + WallObstacle.usedObstacles.Count);
        for (int i = 0; i < chunkSize; i++)
        {
            


            Obstacle obstacle = chooseObstacle();
            obstacle.transform.position = new Vector3(obstacle.transform.position.x,i * obstacleDistance[level]+generatedHeight, obstacle.transform.position.z);

          
                obstacle.transform.Rotate(Vector3.up, Random.Range(0, 360));
            
            
            
        }
        generatedHeight += chunkSize * obstacleDistance[level];
        Debug.Log("new chunk "+generatedHeight);
        
    }
 




    private Obstacle chooseObstacle()
    {

        
        Obstacle obstacle;

        int r = Random.Range(0, 100);

        if (r <= powerUpChance)
        {
            obstacle= Instantiate<SpeedBoost>(speedBoost, generatePos(rotationPoint.position.y), Quaternion.identity);
            obstacle.SetRadius(towerDiameter);
            return obstacle;

        }
        if (r <= 2*powerUpChance)
        {
            obstacle = Instantiate<InvulnerablilityBoost>(invulnerablilityBoost, generatePos(rotationPoint.position.y), Quaternion.identity);
            obstacle.SetRadius(towerDiameter);
            return obstacle;

        }
        if(r<2* powerUpChance + coinChance)
        {
            obstacle = Instantiate<CoinMov>(coin, generatePos(rotationPoint.position.y), Quaternion.identity);
            obstacle.SetRadius(towerDiameter);
            return obstacle;
        }

        int a = Random.Range(0, level);
        switch (a)
        {
            case 0:
                if (SimpleObstacle.unUsedObstacles.Count > 0)
                {
                    obstacle = SimpleObstacle.unUsedObstacles[0];
                    SimpleObstacle.unUsedObstacles.Remove((SimpleObstacle)obstacle);
                    SimpleObstacle.usedObstacles.Add((SimpleObstacle)obstacle);

                }
                else
                {
                    obstacle = SimpleObstacle.usedObstacles[0];
                }
                
          
                break;
            case 1:
                if (WallObstacle.unUsedObstacles.Count > 0)
                {
                    obstacle = WallObstacle.unUsedObstacles[0];
                    WallObstacle.unUsedObstacles.Remove((WallObstacle)obstacle);
                    WallObstacle.usedObstacles.Add((WallObstacle)obstacle);
                }
                else
                {
                    obstacle = WallObstacle.usedObstacles[0];
                }
                break;
            case 2:
                if (HorizontalCrawler.unUsedObstacles.Count > 0)
                {
                    obstacle = HorizontalCrawler.unUsedObstacles[0];
                    HorizontalCrawler.unUsedObstacles.Remove((HorizontalCrawler)obstacle);
                    HorizontalCrawler.usedObstacles.Add((HorizontalCrawler)obstacle);
                }
                else
                {
                    obstacle= HorizontalCrawler.usedObstacles[0];
                }
                break;
            case 3:
                if (TimingObstacle.unUsedObstacles.Count > 0)
                {
                    obstacle = TimingObstacle.unUsedObstacles[0];
                    TimingObstacle.unUsedObstacles.Remove((TimingObstacle)obstacle);
                    TimingObstacle.usedObstacles.Add((TimingObstacle)obstacle);
                }
                else
                {
                    obstacle = TimingObstacle.usedObstacles[0];
                }
                break;
            default:
                if (SimpleObstacle.unUsedObstacles.Count > 0)
                {
                    obstacle = SimpleObstacle.unUsedObstacles[0];
                    SimpleObstacle.unUsedObstacles.Remove((SimpleObstacle)obstacle);
                    SimpleObstacle.usedObstacles.Add((SimpleObstacle)obstacle);
                }
                else
                {
                    obstacle = SimpleObstacle.usedObstacles[0];
                }
                break;

        }

        return obstacle;
        
    }



}
