using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator animation1;
    // Start is called before the first frame update
    void Start()
    {
        animation1 = GetComponent<Animator>();
        animation1.Play("Sword-Idle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
