using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSettings", menuName = "Settings/Player Settings", order = 1)]
public class PlayerSettings : ScriptableObject
{
    [Header("Gameplay Settings")]
    public float sensitivity = 1.0f;
    public float speed = 10f;
    //public float jumpForce = 5f;
    public float springConstant = 5f;
    public int maxSphereCapacity = 5;


}
