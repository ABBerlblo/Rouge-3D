using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public bool real = false;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            if (!real)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            real = true;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
