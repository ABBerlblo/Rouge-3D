using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PickUpController : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponController weaponScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, fpsCam;
    [SerializeField] public Transform weaponHolder;
    

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
         

        if (!equipped)
        {
            weaponScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            weaponScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }
    private void Update()
    {
        //Chekc if player is in range and "E" is pressed
        Vector3 distanceToPlayer = PlayerManager.instance.player.transform.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickupWeapon();
        }

        if(equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }
    
    private void PickupWeapon()
    {
        equipped = true;
        slotFull = true;

        rb.isKinematic = true;
        coll.isTrigger = true;

       // 

        transform.SetParent(weaponHolder);
        transform.localPosition = new Vector3(0.6920568f, -0.2499975f, 1.000639f);
        transform.localRotation = Quaternion.Euler(90f,0f,-14.45f);
        transform.localScale = new Vector3(0.8f,0.8f,0.8f);


        weaponScript.enabled = true;
    }

    private void Drop()
    {
        
       // 
        equipped = false;
        slotFull = false;

        rb.isKinematic = false;
        coll.isTrigger = false;
        transform.SetParent(null);

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.forward * dropUpwardForce, ForceMode.Impulse);

        weaponScript.enabled = false;
    }
}
