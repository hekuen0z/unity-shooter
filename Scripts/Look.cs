using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public static bool cursorLock = true; 

    public Transform player;
    public Transform cmra;

    public float xSensetivity;
    public float ySensetivity;
    public float maxAngle;

    private Quaternion camCenter;

    void Start()
    {
        camCenter = cmra.localRotation;
    }

    void Update()
    {
        SetY();
        SetX();
        UpdateCursorLock();
    }

    void SetY()
    {
        float p_input = Input.GetAxis("Mouse Y") * ySensetivity * Time.deltaTime;
        Quaternion p_adj = Quaternion.AngleAxis(p_input, -Vector3.right);
        Quaternion p_delta = cmra.localRotation * p_adj;

        if (Quaternion.Angle(camCenter, p_delta) < maxAngle)
        {
            cmra.localRotation = p_delta;
        }
    }

    void SetX()
    {
        float p_input = Input.GetAxis("Mouse X") * xSensetivity * Time.deltaTime;
        Quaternion p_adj = Quaternion.AngleAxis(p_input, Vector3.up);
        Quaternion p_delta = player.localRotation * p_adj;
        player.localRotation = p_delta;        
    }

    void UpdateCursorLock()
    {
        if(cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape))
                cursorLock = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown(KeyCode.Escape))
                cursorLock = true;
        }
    }
}
