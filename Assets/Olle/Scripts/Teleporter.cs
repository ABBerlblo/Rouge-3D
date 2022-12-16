using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //print("TELEPORT!!!");
        //GameObject player = other.gameObject;
        //if (player.GetComponent<PlayerStats>().amountOfCoins >= 100)
        //{
        //    player.GetComponent<PlayerStats>().amountOfCoins -= 100;

        //    GameObject level = GameObject.FindGameObjectWithTag("level");
        //    Destroy(level);

        //    Instantiate(new GameObject("Level", typeof(RoomStarter)));

        //    player.transform.position = new Vector3(0, 2, 0);
        //}
    }
}
