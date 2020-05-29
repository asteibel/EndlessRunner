using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;

    public float timeOffset;
    public Vector3 positionOffset;

    private Vector3 velocity;

    public float mainMenuMovementSpeed;
    public CameraMode cameraMode;

    // Update is called once per frame
    void Update()
    {
        if (cameraMode == CameraMode.followPlayer)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + positionOffset, ref velocity, timeOffset);
        } else if (cameraMode == CameraMode.mainMenu)
        {
            transform.position = new Vector3(transform.position.x + mainMenuMovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    public enum CameraMode
    {
        followPlayer, mainMenu
    }
}
