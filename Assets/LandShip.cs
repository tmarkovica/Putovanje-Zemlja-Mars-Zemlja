using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandShip : MonoBehaviour
{
    public bool collisionAllowed = false; // default is true

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogWarning("Collision detected");
        if (other.tag == "Player" && collisionAllowed == true)
        {
            //other.transform.SetParent(this.planet);

            other.transform.GetComponent<NewTry>().InitiateLanding();

            Debug.LogWarning("Landed on planet " + this.transform.parent.name);

            collisionAllowed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.LogWarning("Collision detected");
    }
}
