using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public static float delta_x;
    public static float delta_y;
    public static float delta_z;
    public static float delta_r;
    public GameObject hub;
    public GameObject cor;
    public GameObject player_prefab;
    public static GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        delta_x = 0f;
        delta_y = 0f;
        delta_z = 0f;
        delta_r = 0f;
        hub = (GameObject)Instantiate(hub);
        cor = (GameObject)Instantiate(cor);
        player = (GameObject)Instantiate(player_prefab, new Vector3(0, -13.70921f, 0), new Quaternion(0,0,0,0));
    }

}
