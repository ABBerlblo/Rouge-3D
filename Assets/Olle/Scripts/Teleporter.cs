using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameObject player = GameObject.Find("FirstPersonPlayer");
        print("Colition happened");
        if (other.gameObject.name == "FirstPersonPlayer" && player.GetComponent<PlayerStats>().amountOfCoins >= 100)
        {
            print("Colition sucessfull");
            player.GetComponent<PlayerStats>().amountOfCoins -= 100;
            player.transform.position = new Vector3(0, 2, 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            print("Colition tp: " + player.transform.position);
            //player.transform.eulerAngles = new Vector3(0, 0, 0);
            print("Colition tp: " + player.transform.position);




            //GameObject player = GameObject.Find("FirstPersonPlayer");
            //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //        player.transform.position = new Vector3(0, 2, 0);
            //        player.transform.eulerAngles = new Vector3(0,0,0);
        }
    }
}
