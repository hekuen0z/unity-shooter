using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public float enemyHealth = 100;
    public float speed = 1.0f;
    public float obstacleRande = 5.0f;

    public GameObject Player;

    private bool alive;
    private float timer;
    [SerializeField]
    private GameObject[] laserShotPrefab;
    private GameObject laserShot;

    void Start()
    {
        alive = true;
        gameObject.GetComponent<Animator>().SetBool("Move", true);
    }

    void Update()
    {
        if (gameObject.GetComponent<Animator>().GetBool("Move") == true && alive == true)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 11f))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (!hitObject.GetComponent<CharacterController>())
                {
                    float angleRotation = Random.Range(-90, 90);
                    transform.Rotate(0, angleRotation, 0);
                }
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (alive == true)
        {
            int value = Random.Range(0, 100);
            if (col.tag == "Player" && value < 65)
            {
                Player = col.gameObject;
                gameObject.GetComponent<Animator>().SetBool("Fire", true);
                gameObject.GetComponent<Animator>().SetBool("Move", false);
                transform.LookAt(col.transform.position);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                onFire();
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (alive == true)
        {
            if (col.tag == "Player")
            {
                gameObject.GetComponent<Animator>().SetBool("Fire", false);
                gameObject.GetComponent<Animator>().SetBool("Move", true);
            }
        }
    }

    void onFire()
    {
        timer += 1 * Time.deltaTime;
        if (timer >= 0.3f)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<FirstPersonController>())
                {

                    laserShot = Instantiate(laserShotPrefab[0]) as GameObject;
                    laserShot.transform.position = transform.TransformPoint(0.3f, 1.5f, 0.6f);
                    laserShot.transform.rotation = transform.rotation;

                }
            }
            timer = 0;
        }
    }

    public void ReactToHit(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0 && alive == true)
        {
            Debug.Log("Hi");
            gameObject.GetComponent<Animator>().SetBool("Move", false);
            gameObject.GetComponent<Animator>().SetBool("Fire", false);
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
            SetAlive(false);
            StartCoroutine(DieCoroutine(6));
        }
    }

    public void SetAlive(bool alive)
    {
        this.alive = alive;
    }

    private IEnumerator DieCoroutine(float waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
        Destroy(this.transform.gameObject);
    }
}