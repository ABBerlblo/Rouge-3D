using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomStarter : MonoBehaviour
{
    public int roomOrder = 0;

    public int maxRooms = 30;
    public int minRooms = 18;
    public int maxTries = 10;
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
        for (int i = 0; i <= maxTries; i++)
        {
            if (roomOrder <= maxRooms && roomOrder >= minRooms)
            {
                print(new string("Number of tries: " + (i + 1)));
                break;
            }
            else
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
                roomOrder = 0;
                GenerateLevel();
            }
            if (i==maxTries)
            {
                print("No valid level generated");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("r"))
        {//reload scene, for testing purposes
            Object player = GameObject.Find("FirstPersonPlayer");
            print("Coin "+player.GetComponent<PlayerStats>().amountOfCoins);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //SceneManager.MoveGameObjectToScene(player.GameObject(), SceneManager.GetActiveScene());
            //GameObject newPlayer = ;
            //print("Coin "+newPlayer.GetComponent<PlayerStats>().amountOfCoins);
        }
    }
    void GenerateLevel()
    {
        Object[] startRooms = Resources.LoadAll("Start Rooms", typeof(GameObject));
        GameObject startRoom = Instantiate(startRooms[Random.Range(0, startRooms.Length)], transform) as GameObject;
        startRoom.GetComponent<RoomCreator>().Generate();

        List<GameObject> furthestRooms = new List<GameObject>();
        furthestRooms.Add(startRoom);
        for (int i = 0; i < 3; i++)
        {
            List<GameObject> soonToBeRooms = new List<GameObject>();
            for (int j = 0; j < furthestRooms.Count; j++)
            {
                for (int k = 0; k < furthestRooms[j].transform.childCount; k++)
                {
                    if (furthestRooms[j].transform.GetChild(k).name != "This Room")
                    {
                        soonToBeRooms.Add(furthestRooms[j].transform.GetChild(k).gameObject);
                    }
                }
            }
            if (soonToBeRooms.Count > 0)
            {
                furthestRooms = soonToBeRooms;
                foreach (var room in furthestRooms)
                {
                    room.GetComponent<RoomCreator>().Generate();
                }
            }
        }

        //skapa nytt rum, ta sedan bort det gamla

        List<GameObject> roomOptions = furthestRooms;
        foreach(var room in furthestRooms.ToArray())
        {
            if(room.CompareTag("Connector"))
            {
                roomOptions.Remove(room);
                print("Connector Removed!");
            }
        }
        GameObject currentRoom = roomOptions[Random.Range(0, roomOptions.Count)];

        List<Object> endRooms = new List<Object>(Resources.LoadAll("End Rooms", typeof(GameObject)));
        Object randomEndRoom = endRooms[Random.Range(0, endRooms.Count)];


        GameObject spawner = Instantiate(randomEndRoom, currentRoom.transform.position, currentRoom.transform.rotation, currentRoom.transform.parent) as GameObject;

        Destroy(currentRoom);
    }
}
