using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats: MonoBehaviour
{
    public TextMeshProUGUI healthbar;
    public TextMeshProUGUI coin;

    public float health = 100f;
    public int amountOfCoins = 100;
    public int attackDmg;
    public int increaseCoinsAmount;
    public float maxHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        healthbar.text = health.ToString();
        coin.text = amountOfCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            amountOfCoins -= increaseCoinsAmount;
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            amountOfCoins += increaseCoinsAmount;
        }   


        if (Input.GetKeyDown(KeyCode.K))
        {
            health -= attackDmg;
        }

        else if (Input.GetKeyDown(KeyCode.H))
        {
            health += attackDmg;
        }


        if (health < 0)
        {
            health = 0;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        coin.text = "Coin: " + amountOfCoins.ToString();
        healthbar.text = "Health: " + health.ToString() + "/" + maxHealth.ToString();
    }
}
