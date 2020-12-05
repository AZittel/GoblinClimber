using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{

    public int difficultyLevel;
    public Transform body;


   

    public int getDifficultyLevel()
    {
        return difficultyLevel;
    }

    public abstract void SetRadius(float radius);
   
        
        
        
}
