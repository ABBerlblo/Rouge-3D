using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addCoins : MonoBehaviour
{
    PlayerStats playerStats;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerStats.amountOfCoins += 10;
        }
    }
}
