using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addCoins : MonoBehaviour
{
    PlayerStats playerStats;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coins")
        {
            playerStats.amountOfCoins += 10;
        }
    }
}
