using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabDetect : MonoBehaviour
{
    // check whether the object is being grabbed
    public bool GunGrabbed;
    public GameObject Arrow;

    // check whether hand is grabbing object
    private OVRGrabbable ovrGrab;
    private bool isGrabGun = false;

    private void Start()
    {
        ovrGrab = GetComponent<OVRGrabbable>();
    }

    private void Update()
    {
        Arrow.SetActive(true);

        if (ovrGrab.isGrabbed) {
            isGrabGun = true;
            Arrow.SetActive(false);
        } 
        else isGrabGun = false;

        GunGrabbed = isGrabGun;

        Arrow.transform.position = new Vector3(transform.position.x, Arrow.transform.position.y, transform.position.z);
        
        Debug.Log("Gun Transform is: " + transform.position);
        Debug.Log("Arrow Transform is: " + Arrow.transform.position);
    }
    
    public bool isCurrentGrabGun()
    {
        return isGrabGun;
    }
}
