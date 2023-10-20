using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabDetect : MonoBehaviour
{
    // check whether the object is being grabbed
    public bool GunGrabbed;

    // Indicate which hand is grabbing this GameObject
    [HideInInspector]
    public OVRGrabber hand;

    // Floating Arrow point at gun 
    public GameObject Arrow;

    // check whether hand is grabbing object
    private OVRGrabbable ovrGrab;
    private bool isGrabGun = false;

    private void Start()
    {
        ovrGrab = GetComponent<OVRGrabbable>();
        hand = GetComponent<OVRGrabber>();
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
        
        hand = ovrGrab.grabbedBy;

        // Arrow must follow the gun's (x, z) coordination
        Arrow.transform.position = new Vector3(transform.position.x, Arrow.transform.position.y, transform.position.z);
    }
    
    public bool isCurrentGrabGun()
    {
        return isGrabGun;
    }
}
