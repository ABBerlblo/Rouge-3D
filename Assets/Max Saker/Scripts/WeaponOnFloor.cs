using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOnFloor : MonoBehaviour
{
    public BoxCollider boxColliderSize;

    private void Start()
    {
        boxColliderSize = GetComponent<BoxCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            boxColliderSize.size = new Vector3(0.2183672f, 0.2922171f, 1.01738f);
        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            boxColliderSize.size = new Vector3(0.3925778f, 1.191031f, 1.746516f);
        }
    }
}
