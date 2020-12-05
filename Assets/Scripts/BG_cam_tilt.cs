using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_cam_tilt : MonoBehaviour
{

    public GameObject mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(mainCam.transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }
}
