using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public float scrollSpeed;
    public Renderer background;
    public float multiply;
    public static float playerHorizontal;
    public static float playerVertical;


    private void Start()
    {
        if (multiply.Equals(null))
        {
            multiply = 1.0f;
        }
        playerHorizontal = 1.0f;
        background = GameObject.FindWithTag("BG_plane").GetComponent<Renderer>();
    }

    private void Update()
    {
        //player direction is influenced by PlayerMovement.cs, syncs the cloud movement with player turn-direction
        transform.position += (Vector3.right * scrollSpeed * playerHorizontal + Vector3.down * playerVertical * 3.0f) * Time.deltaTime;

        checkOutOfBounds();
    }

    //if the player is too far out the right or left side of the BG_pane, place him on the opposite side
    private void checkOutOfBounds()
    {
        //right ooB
        if (transform.position.x > background.transform.position.x + background.bounds.extents.x * 1.5f)
        {
            transform.position = new Vector3(transform.position.x - background.bounds.extents.x * 3f, transform.position.y, transform.position.z);
            return;
        }
        //left ooB
        if (transform.position.x < background.transform.position.x - background.bounds.extents.x * 1.5f)
        {
            transform.position = new Vector3(transform.position.x + background.bounds.extents.x * 3f, transform.position.y, transform.position.z);
            return;
        }
        //upper ooB
        if (transform.position.y > background.transform.position.y + background.bounds.extents.y * 2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - background.bounds.extents.y * 4f, transform.position.z);
            return;
        }
        //lower ooB
        if (transform.position.y < background.transform.position.y - background.bounds.extents.y * 2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + background.bounds.extents.y * 4f, transform.position.z);
            return;
        }
    }
}
