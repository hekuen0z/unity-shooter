using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] enemyPrefab_;
    private GameObject enemy_;

    private void Update()
    {

        if(enemy_ == null)
        {
            int randEnemy = Random.Range(1, enemyPrefab_.Length);
            enemy_ = Instantiate(enemyPrefab_[randEnemy]) as GameObject; 
            enemy_.transform.position = new Vector3(0, 3, 0);
            float angle = Random.Range(0, 360);
            enemy_.transform.Rotate(0, angle, 0);
        }
    }
}
