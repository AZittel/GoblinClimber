using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget;
    public float speed = 5.0f;
    public float yOffset = 2.5f;
    public float zOffset = 3.5f;
    public Transform floor;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(playerTarget);
        floor = GameObject.FindWithTag("Floor").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //sync rotation Camera + Player
        Quaternion updateRotation = Quaternion.LookRotation(playerTarget.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, updateRotation, speed * Time.deltaTime * 20);

        //sync movement Camera + Player, raise Camera always above floor level
        if (transform.position.y < floor.position.y + 2.0f)
        {
            PlayerMovement.closeToLava = true;
        }
        Vector3 updatePosition = playerTarget.transform.position - playerTarget.transform.forward * zOffset - playerTarget.transform.up * yOffset;
        transform.position = Vector3.Slerp(transform.position, updatePosition, Time.deltaTime * speed);

    }
}
