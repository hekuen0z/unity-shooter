using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room01Interaction : MonoBehaviour
{
    public GameObject cor;
    private GameObject room_01;
    private GameObject cor_exit_trigger;
    private GameObject room_inside_trigger;
    private GameObject prev_room;
    private GameObject hidden_door;
    private bool inside;

    // Start is called before the first frame update
    void Start()
    {
        room_01 = GameObject.Find("Room_01(Clone)");
        room_inside_trigger = GameObject.Find("room_inside_trigger");
        cor_exit_trigger = GameObject.Find("exit_trigger");
        prev_room = GameObject.FindGameObjectWithTag("Cor");
    }
    void ObjectsUpdate()
    {
        
        room_01 = GameObject.Find("Room_01(Clone)");
        room_inside_trigger = GameObject.Find("room_inside_trigger");
        
    }

    void CoordinatesUpdate()
    {
        if ((int)room_01.transform.rotation.eulerAngles.y == 0)
        {
            StartGame.delta_x = +145f;
            StartGame.delta_y = -73.5f;
            StartGame.delta_z = +82.7f;
            StartGame.delta_r = +0f;
        }
        if ((int)room_01.transform.rotation.eulerAngles.y == 90)
        {
            StartGame.delta_x = +84.1183f;
            StartGame.delta_y = -73.5f;
            StartGame.delta_z = -145.02f;
            StartGame.delta_r = +90f;
        }
        if ((int)room_01.transform.rotation.eulerAngles.y == 180)
        {
            StartGame.delta_x = -145f;
            StartGame.delta_y = -73.5f;
            StartGame.delta_z = -82.7f;
            StartGame.delta_r = +180f;
        }
        if ((int)room_01.transform.rotation.eulerAngles.y == 270)
        {
            StartGame.delta_x = -84.1183f;
            StartGame.delta_y = -73.5f;
            StartGame.delta_z = +145.02f;
            StartGame.delta_r = +270f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ObjectsUpdate();
        if (room_inside_trigger.GetComponent<BoxCollider>().bounds.Intersects(StartGame.player.GetComponent<CapsuleCollider>().bounds))
        {
            inside = true;
        }
        else inside = false;
        if (cor_exit_trigger)
            if (cor_exit_trigger.GetComponent<BoxCollider>().enabled && inside)
            {
                Cor.HiddenDoorOn();
                Destroy(prev_room);
                CoordinatesUpdate();
                cor = (GameObject)Instantiate(cor,
                        new Vector3(room_01.transform.position.x + StartGame.delta_x, room_01.transform.position.y + StartGame.delta_y, room_01.transform.position.z + StartGame.delta_z),
                        new Quaternion(0, 0, 0, 0));
                cor.transform.Rotate(0f, StartGame.delta_r, 0f);
                GameObject.Find("ProfileDoor (4)").SetActive(false);
            }
    }
   
}
