using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public PlayerSettings playerSettings;
    // object of the player in the scene
    public GameObject playerObject;
    public Camera cam;
    public List<GameObject> forceSpheres;
    public Rigidbody playerRigidbody;


    private Player player;
    private InputClass inputClass;
    private List<ForceSphere> sphereList = new List<ForceSphere>();
    private int sphereWithPlayer = 0;
    //defines how many sphere can player hold , if limit is reached then every other captured sphere will go to waste

    private int maxSphereCapacity ;

    private  Vector3 movementInput = new Vector3(0,0,0);
    private Vector2 aimInput = new Vector2(0,0);
    void Start()
    {
        inputClass = new InputClass();
        player = new Player(playerObject, cam,playerRigidbody);
        maxSphereCapacity = playerSettings.maxSphereCapacity;
        // we need to link the spheres form the game scene to the forceSphere object so that they can interact
        foreach(GameObject sphere in forceSpheres)
        {
            sphereList.Add(new ForceSphere(sphere));
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        getInput();
        player.Move(inputClass);
        player.Aim(inputClass);
        playerJump();
        foreach(ForceSphere sphere in sphereList)
        {
            //for every non captured sphere check if it is colleding with player
            if(!sphere.isCaptured)
            {
                if(sphere.checkForCollision())
                {
                    sphereWithPlayer++;
                    if (sphereWithPlayer > maxSphereCapacity)
                        sphereWithPlayer = maxSphereCapacity;
                }
            }
        }
    }

    void getInput()
    {
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.z = Input.GetAxisRaw("Vertical");
            aimInput.x = Input.GetAxis("Mouse X");
            aimInput.y = Input.GetAxis("Mouse Y");
        }
        

        movementInput.Normalize();
        movementInput *= playerSettings.speed * Time.deltaTime;
        aimInput *= playerSettings.sensitivity * Time.deltaTime;
        inputClass.movementInput = movementInput;
        inputClass.aimInput = aimInput;
    }

    void playerJump()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(sphereWithPlayer>0)
            {
                Debug.Log("Jump");
                player.Jump(calulateJumpForce());
                sphereWithPlayer = 0;
            }
        }
    }

    float calulateJumpForce()
    {
        //jump force depends upon number of forceSphere does player have
        //jump force is calculated by hooke's law , more forceSphere captured more the internal spring will be compressed 
        // and jump will be higher
        return playerSettings.springConstant * sphereWithPlayer;
    }
}
