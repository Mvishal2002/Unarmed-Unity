using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    GameObject player1;
    public Transform firePoint;
    int equiped = 0;
    public GameObject bullet;
    float nextFire;
    float fireRate = 0.2f;

    private void Start()
    {
        PlayerPrefs.DeleteKey("score");
        player1 = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMov>().weaponInstantiated = 0;
            other.GetComponent<PlayerMov>().bullets = 15;
            transform.parent = other.transform;
            transform.position = other.transform.position;
            transform.rotation = other.transform.localRotation;
            equiped = 1;
        }
    }

    void Update()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
        if (player1 != null && enemy != null)
        {
            
           
                if (player1.GetComponent<PlayerMov>().bullets > 0 && player1.GetComponent<PlayerMov>().isFiring == true)
                {
                    if (nextFire < Time.time)
                    {
                        nextFire = Time.time + fireRate;
                        GameObject Bullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
                        FindObjectOfType<AudioManager>().Play("PlayerBullet");
                        player1.GetComponent<PlayerMov>().bullets--;
                        Bullet.GetComponent<Rigidbody>().velocity = (enemy.transform.position - firePoint.position).normalized * 10f;
                        Destroy(Bullet, 2f);
                    }
                }
                if (player1.GetComponent<PlayerMov>().bullets <= 0 && equiped == 1)
                {
                    equiped = 0;
                    Destroy(gameObject);
                }
                if (transform.parent != player1.transform)
                {
                    transform.Rotate(new Vector3(0f, 0.8f, 0f));
                }
           
        }

    }
}
