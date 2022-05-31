using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    public int playerHealth;
    private bool isAlive;
    
    void Start()
    {
        isAlive = true;
    }

    public void Hurt(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Player health: " + playerHealth);
        if(playerHealth <= 0)
        {
            isAlive = false;
        }
    }
}
