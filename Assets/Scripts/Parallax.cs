using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed; //This variable makes choose the speed movement between parallax elements from the inspector

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform; //Assign main camera transform
        previousCameraPosition = cameraTransform.position; //Previous camera position to the new camera position
    }

    void LateUpdate()
    {
        //Calculate the previous position of the camera in a few frames difference for the sensation of movement
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * parallaxSpeed; //Multiply 0.5 make the elements move at half speed of the background

        transform.Translate(new Vector3(deltaX, 0, 0)); //Elements transform translation
        previousCameraPosition = cameraTransform.position;
    }
}
