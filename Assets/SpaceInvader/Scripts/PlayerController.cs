using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Bullet velocity
    public float bulletSpeed = 10;

    // Gun shoot out position
    public GameObject ShootOutput_Gun1;
    public GameObject ShootOutput_Gun2;
    [HideInInspector]
    public GameObject ShootOutput;

    //public ObjectGrabDetect gun;
    public GameObject gun1;
    public GameObject gun2;

    // bullet prefab
    public GameObject bulletPrefab;

    // haptic clip
    public AudioClip HapticAudioClip;

    [Range(0.01f, 1f)]
    public float speedH = 1.0f;
    [Range(0.01f, 1f)]
    public float speedV = 1.0f;
    
    GameManager gm;
    

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        GunActionManager();
    }

    void OnFire(GameObject ShootOutput)
    {
        // spawn a new bullet
        GameObject newBullet = Instantiate(bulletPrefab);

        // pass the game manager
        newBullet.GetComponent<BulletController>().gm = gm;

        // position will be that of the gun
        newBullet.transform.position = ShootOutput.transform.position;

        // get rigid body
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

        // let the bullet face to the forward when shoot
        newBullet.transform.LookAt(ShootOutput.transform.right * 30f);

        // give the bullet velocity
        bulletRb.velocity = ShootOutput.transform.forward * bulletSpeed;
    }


    void GunActionManager()
    {
        // shoot gun
        // Case 1: both guns were grabbed
        if (gun1.GetComponent<ObjectGrabDetect>().GunGrabbed == true && gun2.GetComponent<ObjectGrabDetect>().GunGrabbed == true)
        {
            if (gun1.GetComponent<ObjectGrabDetect>().hand.name == "DistanceGrabHandLeft")
            {
                GrabLeftHandGunFire(ShootOutput_Gun1);
                GrabRightHandGunFire(ShootOutput_Gun2);
            }
            else
            {
                GrabRightHandGunFire(ShootOutput_Gun1);
                GrabLeftHandGunFire(ShootOutput_Gun2);
            }
        }
        
        // Case 2: gun1 is grabbed
        else if (gun1.GetComponent<ObjectGrabDetect>().GunGrabbed == true)
        {
            if (gun1.GetComponent<ObjectGrabDetect>().hand.name == "DistanceGrabHandLeft")
            {
                GrabLeftHandGunFire(ShootOutput_Gun1);
            }

            else if ((gun1.GetComponent<ObjectGrabDetect>().hand.name == "DistanceGrabHandRight"))
                GrabRightHandGunFire(ShootOutput_Gun1);
        }

        // Case 3: gun2 is grabbed
        else if (gun2.GetComponent<ObjectGrabDetect>().GunGrabbed == true)
        {
            if (gun2.GetComponent<ObjectGrabDetect>().hand.name == "DistanceGrabHandLeft")
            {
                GrabLeftHandGunFire(ShootOutput_Gun2);
            }

            else if ((gun2.GetComponent<ObjectGrabDetect>().hand.name == "DistanceGrabHandRight"))
                GrabRightHandGunFire(ShootOutput_Gun2);
        }
    }

    // call this function when current grabbing hand is Left Hand
    void GrabLeftHandGunFire(GameObject ShootOutput)
    {
        // pull the trigger
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            // OVR vibration feedback
            OVRHapticsClip HaptiClip = new(HapticAudioClip);
            OVRHaptics.LeftChannel.Preempt(HaptiClip);

            // gun shot audio effect
            FindObjectOfType<AudioManager>().Play("BulletShot");

            // generate bullet on the gun muzzle
            OnFire(ShootOutput);
        }
    }

    // call this function when current grabbing hand is Right Hand
    void GrabRightHandGunFire(GameObject ShootOutput)
    {
        // pull the trigger
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // OVR vibration feedback
            OVRHapticsClip HaptiClip = new(HapticAudioClip);
            OVRHaptics.RightChannel.Preempt(HaptiClip);

            // gun shot audio effect
            FindObjectOfType<AudioManager>().Play("BulletShot");

            // generate bullet on the gun muzzle
            OnFire(ShootOutput);
        }
    }
}
