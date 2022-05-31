using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cor : MonoBehaviour
{
    
    private int rand_room;
    public GameObject[] rooms;
    private GameObject exit_trigger;
    private GameObject enter_trigger;
    private GameObject cor_exit_door;
    private GameObject cor_enter_door;
    public static GameObject hidden_door;
    private GameObject inside_trigger;
    private GameObject prev_room;
    private GameObject cor;
    private GameObject next_room;
    private bool enter;
    private bool exit;
    private bool inside;
    //-1.603934 - 2 позиция
    //-0.5539501 - 1 позиция
    //-1,0499839 - расстояние
    // Start is called before the first frame update
    void Start()
    {
        cor = GameObject.Find("Cor 1(Clone)");
        cor_enter_door = GameObject.Find("CorEnterDoor");
        enter_trigger = GameObject.Find("enter_trigger");
        exit_trigger = GameObject.Find("exit_trigger");
        inside_trigger = GameObject.Find("inside_trigger");
        cor_exit_door = GameObject.Find("CorExitDoor");
        rand_room = (int)Random.Range(0, 2);
        next_room = rooms[rand_room];
        prev_room = GameObject.FindGameObjectWithTag("Room");
        
    }

    void ObjectsUpdate()
    {
        //   hidden_door = GameObject.Find("HiddenDoor");
        //if(hidden_door)
        //    hidden_door.SetActive(false);
        
        cor = GameObject.Find("Cor 1(Clone)");
        cor_enter_door = GameObject.Find("CorEnterDoor");
        exit_trigger = GameObject.Find("exit_trigger");
        enter_trigger = GameObject.Find("enter_trigger");
        inside_trigger = GameObject.Find("inside_trigger");
        cor_exit_door = GameObject.Find("CorExitDoor");
        next_room = rooms[rand_room];
        prev_room = GameObject.FindGameObjectWithTag("Room");
        if (hidden_door && exit_trigger.GetComponent<BoxCollider>().enabled && !inside)
            hidden_door.SetActive(true);
        else if (hidden_door && inside)
            hidden_door.SetActive(false);
        Debug.Log(StartGame.delta_x);
    }

    void CoordinatesUpdate()
    {
        if (rand_room == 0)
        {
            if ((int)cor.transform.rotation.eulerAngles.y == 0)
            {
                StartGame.delta_x = -144.7f;
                StartGame.delta_y = +73.6f;
                StartGame.delta_z = +82.32f;
                StartGame.delta_r = +0f;
            }
            if((int)cor.transform.rotation.eulerAngles.y == 90)
            {
                StartGame.delta_x = +83.00168f;
                StartGame.delta_y = +73f;
                StartGame.delta_z = +144.7f;
                StartGame.delta_r = +90f;
            }
            if ((int)cor.transform.rotation.eulerAngles.y == 180)
            {
                StartGame.delta_x = +144f;
                StartGame.delta_y = +73.6f;
                StartGame.delta_z = -82.32f;
                StartGame.delta_r = +180f;
            }
            if ((int)cor.transform.rotation.eulerAngles.y == 270)
            {
                StartGame.delta_x = -83.00168f;
                StartGame.delta_y = +73f;
                StartGame.delta_z = -144.7f;
                StartGame.delta_r = +270f;
            }
        }
        else if (rand_room == 1)
        {
            
            if ((int)cor.transform.rotation.eulerAngles.y == 0)
            {
                StartGame.delta_x = -77.3f;
                StartGame.delta_y = -1f;
                StartGame.delta_z = +85.62f;
                StartGame.delta_r = +0f;
            }
            if ((int)cor.transform.rotation.eulerAngles.y == 90)
            {
                StartGame.delta_x = +86.9f;
                StartGame.delta_y = -0.4f;
                StartGame.delta_z = +77f;
                StartGame.delta_r = +90f;
            }
            if ((int)cor.transform.rotation.eulerAngles.y == 180)
            {
                StartGame.delta_x = +77.32f;
                StartGame.delta_y = -1f;
                StartGame.delta_z = -85.6f;
                StartGame.delta_r = +180f;
            }
            if ((int)cor.transform.rotation.eulerAngles.y == 270)
            {
                StartGame.delta_x = -86.9f;
                StartGame.delta_y = -1f;
                StartGame.delta_z = -77f;
                StartGame.delta_r = +270f;
            }
        }
        else if (rand_room == 2)
        {
            StartGame.delta_x = +32.5f;
            StartGame.delta_y = -1.059f;
            StartGame.delta_z = +84.32f;
            StartGame.delta_r = +0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ObjectsUpdate();
        if (exit_trigger.GetComponent<BoxCollider>().bounds.Intersects(StartGame.player.GetComponent<CapsuleCollider>().bounds))
        {
            exit = true;
        }
        else exit = false;
        if (inside_trigger.GetComponent<BoxCollider>().bounds.Intersects(StartGame.player.GetComponent<CapsuleCollider>().bounds))
        {
            inside = true;
        }
        else inside = false;
        if (enter_trigger.GetComponent<BoxCollider>().bounds.Intersects(StartGame.player.GetComponent<CapsuleCollider>().bounds))
        {
            enter = true;
        }
        else enter = false;
        if (enter_trigger.GetComponent<BoxCollider>().enabled && inside)
        {
            Destroy(prev_room);
            CoordinatesUpdate();
            next_room = (GameObject)Instantiate(next_room,
                new Vector3(cor.transform.position.x + StartGame.delta_x, cor.transform.position.y + StartGame.delta_y, cor.transform.position.z + StartGame.delta_z),
                new Quaternion(0, 0, 0, 0));
            next_room.transform.Rotate(0f, StartGame.delta_r, 0f);
            hidden_door = GameObject.Find("HiddenDoor");
            enter_trigger.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void OnGUI()
    {
        if (exit)
        {
            GUI.Label(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "Нажмите E, чтобы открыть дверь");
            if (Input.GetKeyDown(KeyCode.E))
            {
                cor_exit_door.GetComponent<Animator>().Play("cor_exit_door_open");
                exit_trigger.GetComponent<BoxCollider>().enabled = false;
                exit = false;
                StartCoroutine(DoorSlide());
            }
        }
        if (enter)
        {
            GUI.Label(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "Нажмите E, чтобы открыть дверь");
            if (Input.GetKeyDown(KeyCode.E))
            {
                enter_trigger.GetComponent<BoxCollider>().enabled = false;
                cor_enter_door.GetComponent<Animator>().Play("hub_door_open");
                enter = false;
                StartCoroutine(HubDoorClose());
            }
        }
    }

    public static void HiddenDoorOn()
    {
        hidden_door.SetActive(true);
    }

    IEnumerator DoorSlide()
    {
        yield return new WaitForSeconds(4);
        cor_exit_door.GetComponent<Animator>().Play("cor_exit_door_close");
        yield return new WaitForSeconds(1);
        exit_trigger.GetComponent<BoxCollider>().enabled = true;
        exit = true;
    }
    IEnumerator HubDoorClose()
    {
        yield return new WaitForSeconds(4);
        cor_enter_door.GetComponent<Animator>().Play("hub_door_close");
        yield return new WaitForSeconds(1);
        enter_trigger.GetComponent<BoxCollider>().enabled = true;
        enter = true;
    }


}
