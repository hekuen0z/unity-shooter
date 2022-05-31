using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTag : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 5;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        PlayerCharacteristics player = other.GetComponent<PlayerCharacteristics>();
        if (player != null)
        {
            player.Hurt(damage);
        }

        Destroy(this.gameObject);
    }

}