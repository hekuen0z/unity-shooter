using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public float timer;
    public bool ispuse;
    public bool guipuse;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            Time.timeScale = 0;
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            Time.timeScale = 1;
            ispuse = false;
        }
        if (ispuse)
        {
            Time.timeScale = 0;
        }
        if (!ispuse)
        {
            Time.timeScale = 1;
        }
    }
    public void OnGUI()
    {
        if (ispuse == true)
        {
            Cursor.visible = true;// включаем отображение курсора

            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 150f, 45f), "Продолжить"))
            {
                Cursor.visible = false;
                ispuse = false;
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "Сохранить"))
            {

            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, 150f, 45f), "Загрузить"))
            {

            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "В Меню"))
            {
                SceneManager.LoadScene(0);
                ispuse = false;
            }
        }
    }
}