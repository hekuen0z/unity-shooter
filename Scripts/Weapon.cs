using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 2;
    public float fireRate = 1f;
    public float fireRange = 15f;
    public int cAmmo = 20;
    public float reloadTime = 1f;
    public float force = 155f;

    public Transform bulletSpawn;
    public GameObject muzzleFlash;
    public AudioClip shotSFX;
    public AudioSource audioSource;
    private GameObject hitEffect;

    public Camera _camera;
    private float fireDelay = 0f;
    private float reloadDelay = 0f;
    private int cAmmoTemp = 0;

    void Start()
    {
        hitEffect = GameObject.Find("Hit Effect");
        if (hitEffect)
            Debug.Log("Alexey PIDOR");
        if (!hitEffect)
            Debug.Log("Anton BATON");
        cAmmoTemp = cAmmo;
    }

    void Update()
    { 
        if (Input.GetButton("Fire1") && Time.time > fireDelay && Time.time > reloadDelay)
        {
            fireDelay = Time.time + 1f / fireRate;
            Shoot();
            cAmmoTemp--;
        }
        if (cAmmoTemp == 0)
        {
            reloadDelay = Time.time + reloadTime;
            Debug.Log("Reload...");
            cAmmoTemp = cAmmo;
        }
    }

    void Shoot()
    {
        RaycastHit raycastHitTry;
        GameObject hitTry;
        if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out raycastHitTry, fireRange))
        {
            return;
        }

        hitTry = raycastHitTry.transform.gameObject;
        if (hitTry.GetComponent<BoxCollider>() != null)
        {
            hitTry.GetComponent<BoxCollider>().enabled = false;
        }

        audioSource.PlayOneShot(shotSFX);
        Instantiate(muzzleFlash, bulletSpawn.position, bulletSpawn.rotation);
        RaycastHit raycastHit;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out raycastHit, fireRange))
        {
            GameObject hitObject = raycastHit.transform.gameObject;
            EnemyAI target = hitObject.GetComponent<EnemyAI>();
            if (target != null)
            {
                target.ReactToHit(damage);
            }
            GameObject hitResult = raycastHit.transform.gameObject;
            hitResult = Instantiate(hitEffect, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
            Destroy(hitResult, 2f);
        }
        hitTry = raycastHitTry.transform.gameObject;
        if (hitTry.GetComponent<BoxCollider>() != null)
        {
            hitTry.GetComponent<BoxCollider>().enabled = true;
        }
    
    }
}
