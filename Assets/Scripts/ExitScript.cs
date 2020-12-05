using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{


    public void DoExitGame()
    {
        Debug.Log("application quitting");
        Application.Quit();
    }
}
