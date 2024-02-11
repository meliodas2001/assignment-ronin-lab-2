using UnityEngine;

public class ForceSphere 
{
    //holds the graphic of the forceSphere that will be rendered 
    private GameObject sphereGraphic;
    //checks if the sphere is captured or not by player
    public bool isCaptured { get; private set; }

    public ForceSphere(GameObject sphereObject)
    {
        sphereGraphic = sphereObject;
        isCaptured = false;
    }

    public bool checkForCollision()
    {
        //check collosion with the help of checksphere of same size and position as our graphic
        //Gizmos.DrawSphere(position, sphereGraphic.transform.localScale.x / 2);
        if(Physics.CheckSphere(sphereGraphic.transform.position, sphereGraphic.transform.localScale.x / 2))
        {
            isCaptured = true;
            sphereGraphic.SetActive(false);
            return true;
        }
        return false;
    }
}
