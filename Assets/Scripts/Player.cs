using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private GameObject playerObject = null;
    private Camera fppCamera = null;
    private Rigidbody rigidbody = null;

    float playerYRotation, fppCameraXRotation;

    //constructor to connect the player object from the scene to this object
    public Player(GameObject obj,Camera _cam,Rigidbody _rigidbody)
    {
        playerObject = obj;
        fppCamera = _cam;
        rigidbody = _rigidbody;
        playerYRotation = playerObject.transform.eulerAngles.y;
        fppCameraXRotation = fppCamera.transform.eulerAngles.x;
    }


    public void Move(InputClass inp)
    {
        if(playerObject == null)
        {
            Debug.LogError("No player object found");
            return;
        }
        playerObject.transform.Translate(inp.movementInput);
    }

    public void Aim(InputClass inp)
    {
        if (fppCamera == null)
        {
            Debug.LogError("No fpp camera found found");
            return;
        }
        //player's y rotation will change according to x input 
        playerYRotation += inp.aimInput.x;

        //camera's x rotation will change according to y input
        fppCameraXRotation -= inp.aimInput.y;

        //clamp so we can not see backwards
        fppCameraXRotation = Mathf.Clamp(fppCameraXRotation, -90f, 90f);

        //rotation
        playerObject.transform.rotation = Quaternion.Euler(0f, playerYRotation, 0f);
        //change camera's local rotation as camera is the child of player object
        fppCamera.transform.localRotation = Quaternion.Euler(fppCameraXRotation, 0f, 0f);
    }

    public void Jump(float force)
    {
        rigidbody.AddForce(playerObject.transform.up*force, ForceMode.Impulse);
    }
}
