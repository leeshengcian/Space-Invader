using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabDetect : MonoBehaviour
{
    // check whether the object is being grabbed
    public bool GunGrabbed;

    // check whether hand is grabbing object
    private OVRGrabbable ovrGrab;
    private bool isGrabGun = false;

    private void Start()
    {
        ovrGrab = GetComponent<OVRGrabbable>();
    }

    private void Update()
    {
        if (ovrGrab.isGrabbed) isGrabGun = true;
        else isGrabGun = false;

        GunGrabbed = isGrabGun;
    }
    
    public bool isCurrentGrabGun()
    {
        return isGrabGun;
    }
}
