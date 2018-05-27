using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    bool isSpider;

    // Use this for initialization
    void Start()
    {
        if (player.GetComponent<PLayerController>())
            isSpider = true;

        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (isSpider)
        {
            float temp = player.transform.position.x + offset.x;
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = new Vector3(temp , temp * (533.0f / 1920) - 350, transform.position.z);

        }
        else
        {
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = player.transform.position + offset;

        }


    }
}
