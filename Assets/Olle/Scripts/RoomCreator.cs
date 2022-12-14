using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class RoomCreator : MonoBehaviour
{
    public int roomNumber;
    public Vector3 entranceDoor;
    public Vector4[] exitDoor;
    private Object[] rooms;
    private readonly string[] roomType = {"1st Rooms","2nd Rooms","3rd Rooms"};
    public int roomOrder;
    //private bool TEST = true;

    public void Generate()
    {
        GameObject.FindGameObjectWithTag("level").GetComponent<RoomStarter>().roomOrder++;
        roomOrder = GameObject.FindGameObjectWithTag("level").GetComponent<RoomStarter>().roomOrder;
        if (exitDoor.Length > 0)
        {

            rooms = Resources.LoadAll(roomType[roomNumber], typeof(GameObject));
            for (int i = 0; i < exitDoor.Length; i++) //KÖRS FÖR VARJE EXITDOOR
            {
                Vector3 doorPosition = transform.position + Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * new Vector3(exitDoor[i].x, exitDoor[i].y, exitDoor[i].z);

                //kör bara koden under om det inte finns något där
                Debug.DrawRay(doorPosition - Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * new Vector3(exitDoor[i].x * 0.1f, 0, exitDoor[i].z * 0.1f) + new Vector3(0, -0.6f, 0), doorPosition - transform.position + new Vector3(0, -1f, 0), Color.red, 1000); //test ray                    FIX!!!!! *9 is lazy and makes it shoot wrong
                if (!Physics.Raycast(doorPosition - Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * new Vector3(exitDoor[i].x * 0.1f, 0, exitDoor[i].z * 0.1f) + new Vector3(0, -0.6f, 0), doorPosition - transform.position + new Vector3(0, -1f, 0), 3))
                {

                    //gör en lista med rum som kan generera, om listan är tom skapa ett passande connector rum
                    List<Object> list = new();

                    int[] doorsAndWalls = new int[3] { 0, 0, 0 };
                    for (int k = 90; k <= 270; k += 90)
                    {
                        Vector3 checkPosition = doorPosition - Quaternion.AngleAxis(exitDoor[i].w + transform.eulerAngles.y - 180, Vector3.up) * new Vector3(2.5f * 5,1,0); //Fixade scalen
                        Vector3 checkDoorPosition = checkPosition + Quaternion.AngleAxis(transform.eulerAngles.y + k + exitDoor[i].w - 180, Vector3.up) * new Vector3(2.5f * 5, 1, 0);

                        //Debug.DrawRay(checkDoorPosition - 0.1f * (Quaternion.AngleAxis(transform.eulerAngles.y + k + exitDoor[i].w - 180, Vector3.up) * new Vector3(2.5f * 5, 1, 0)) + new Vector3(0, -0.3f, 0), checkDoorPosition - checkPosition + new Vector3(0, -1f, 0), Color.yellow, 1000); //test ray
                        //Debug.DrawRay(checkDoorPosition - 0.1f * (Quaternion.AngleAxis(transform.eulerAngles.y + k + exitDoor[i].w - 180, Vector3.up) * new Vector3(2.5f * 5, 1, 0)) + new Vector3(0, -0.5f, 0), checkDoorPosition - checkPosition + new Vector3(0, -1f, 0), Color.yellow, 1000); //test ray

                        if (Physics.Raycast(checkDoorPosition - 0.1f * (Quaternion.AngleAxis(transform.eulerAngles.y + k + exitDoor[i].w - 180, Vector3.up) * new Vector3(2.5f * 5, 1, 0)) + new Vector3(0, -0.3f, 0), checkDoorPosition - checkPosition + new Vector3(0, -1f, 0), 3))
                        {
                            doorsAndWalls[(k / 90) - 1] = 2;//wall
                            //print(new string("WALL HIT!! roomOrder:" + roomOrder + " ExitDoor:" + i + " Direction:" + k));
                        }
                        else if (Physics.Raycast(checkDoorPosition - 0.1f * (Quaternion.AngleAxis(transform.eulerAngles.y + k + exitDoor[i].w - 180, Vector3.up) * new Vector3(2.5f * 5, 1, 0)) + new Vector3(0, -0.5f, 0), checkDoorPosition - checkPosition + new Vector3(0, -1f, 0), 3))
                        {
                            doorsAndWalls[(k / 90) - 1] = 1;//door
                            //print(new string("DOOR HIT!! roomOrder:" + roomOrder + " ExitDoor:" + i + " Direction:" + k));
                        }
                    }

                    //loopa igenom rooms och checka om varje exitdoor leder in i en vägg eller conectar med en annan dörr linar allt inte upp ta bort från listan
                    foreach (Object possibleRoom in rooms)
                    {
                        RoomCreator currentRoom = possibleRoom.GetComponent<RoomCreator>();
                        

                        
                        //loopa igenom listan, om det är false checka wall om det är true checka dörr, se till att rätt finns där

                        bool check = true;
                        for (int k = 0; k < doorsAndWalls.Length; k++)
                        {
                            bool works = true;
                            if (doorsAndWalls[k] == 1)
                            {
                                if (currentRoom.exitDoor.Length != 0)
                                {
                                    //checka så att det finns åtmindstånde en dörr där
                                    for (int l = 0; l < currentRoom.exitDoor.Length; l++)
                                    {
                                        if (currentRoom.exitDoor[l].w == (k + 1) * 90)
                                        {
                                            works = true;
                                            //print(new string("Door at right possition! roomOrder:" + roomOrder + " ExitDoor:" + i + "Number of doors:" + currentRoom.exitDoor.Length));
                                            break;
                                        }
                                        else
                                        {
                                            works = false;
                                        }
                                    }
                                }
                                else
                                {
                                    works = false;
                                }
                            }
                            else if (doorsAndWalls[k] == 2)
                            {
                                //checka så att det inte finns någon dörr där
                                for (int l = 0; l < currentRoom.exitDoor.Length; l++)
                                {
                                    if (currentRoom.exitDoor[l].w == (k + 1) * 90)
                                    {
                                        works = false;
                                        break;
                                    }
                                    else
                                    {
                                        works = true;
                                    }
                                }
                            }
                            if (!works)
                            {
                                check = false;
                                break;
                            }
                        }
                        



                        //om allt stämmer lägg till rummet till listan
                        if (check)
                        {
                            list.Add(possibleRoom);
                        }
                        else
                        {
                            //print(new string("not added! roomOrder:" + roomOrder + " ExitDoor:" + i));
                            //print(currentRoom.exitDoor.Length);
                        }

                    }



                    //Om listan är tomm skapa ett connector room igenom att kolla vart det finns dörrar och deleta den väggen
                    if (list.Count == 0)
                    {
                        Object[] ConRooms = Resources.LoadAll("Connector Rooms", typeof(GameObject));
                        Object randomConRoom = ConRooms[Random.Range(0, ConRooms.Length)];

                        GameObject spawner = Instantiate(randomConRoom, doorPosition - Quaternion.AngleAxis(exitDoor[i].w + transform.eulerAngles.y - 180, Vector3.up) * randomConRoom.GetComponent<RoomCreator>().entranceDoor, Quaternion.AngleAxis(exitDoor[i].w + transform.eulerAngles.y - 180, Vector3.up), transform) as GameObject;
                        spawner.GetComponent<RoomCreator>().roomNumber = roomNumber + 1;

                        for (int j = 0; j < doorsAndWalls.Length; j++)
                        {
                            if (doorsAndWalls[j] == 1)
                            {
                                //print(spawner.transform.Find("Walls").transform.Find(((j + 1) * 90).ToString()).GameObject());
                                Destroy(spawner.transform.Find("Walls").transform.Find(((j + 1) * 90).ToString()).GameObject());
                            }
                        }
                        //Debug.Log("CONNECTOR ROOM CREATED! roomOrder:" + roomOrder + " ExitDoor:" + i);
                    }
                    else
                    {
                        //Debug.Log(rooms.Length);
                        Object randomRoom = list[Random.Range(0, list.Count)];

                        GameObject spawner = Instantiate(randomRoom, doorPosition - Quaternion.AngleAxis(exitDoor[i].w + transform.eulerAngles.y - 180, Vector3.up) * randomRoom.GetComponent<RoomCreator>().entranceDoor, Quaternion.AngleAxis(exitDoor[i].w + transform.eulerAngles.y - 180, Vector3.up), transform) as GameObject;
                        spawner.GetComponent<RoomCreator>().roomNumber = roomNumber + 1;
                    }


                }
            }
        }
        //TEST = false;
    }
    //private void Update()
    //{
    //    if (TEST && Input.GetKeyUp(KeyCode.Space))
    //    {
    //        test();
    //    }
    //}
}
