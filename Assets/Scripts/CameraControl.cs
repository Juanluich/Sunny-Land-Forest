using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;
    private Vector3 relativePosition;

    //Position used to calculate the difference between camera position and player position
    void Start()
    {
        
        relativePosition = transform.position - player.transform.position;
    }
    void Update()
    {

        transform.position = player.transform.position + relativePosition;

        if (transform.position.y >= -0.25f)
        {
            transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);
        }
        
    }
}
