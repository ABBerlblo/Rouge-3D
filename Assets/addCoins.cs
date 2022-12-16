using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addCoins : MonoBehaviour
{
    PlayerStats playerStats;
    private void OnTriggerEnter(Collider other)
    {
        playerStats = this.GetComponent<PlayerStats>();
        if(other.gameObject.tag == "Coins")
        {
            playerStats.amountOfCoins += 10;
            Destroy(other.gameObject);
        }
    }
}
