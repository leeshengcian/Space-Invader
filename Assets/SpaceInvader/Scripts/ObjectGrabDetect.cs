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
        Arrow.GetComponent<Animation>().Play();
    }

    private void Update()
    {
        Arrow.transform.position = transform.position;
        Arrow.SetActive(true);
        if (ovrGrab.isGrabbed) {
            isGrabGun = true;
            Arrow.SetActive(false);
        } 
        else isGrabGun = false;

        GunGrabbed = isGrabGun;
    }
    
    public bool isCurrentGrabGun()
    {
        return isGrabGun;
    }
}
