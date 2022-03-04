using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMov : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject weapon;
    float speed = 5f;
    Vector3 move;
    int health = 50;
    int damage = 2;
    public int bullets = 0;
    public int weaponInstantiated = 0;
    public int weaponEquiped = 0;
    float nextFire;
    float fireRate = 0.2f;
    public Joystick joystick;
    public int enemiesKilled = 0;
    public bool isFiring;
    float horizontalMov;
    float verticalMov;
    public Animator loadlevel;

    public void PointerDown()
    {
        isFiring = true;
    }

    public void PointerUp()
    {
        isFiring = false;
    }

    private void Update()
    {
        if(joystick.Horizontal < 0.2 && joystick.Horizontal > -0.2)
        {
            horizontalMov = 0;
        }
        else
        {
            horizontalMov = joystick.Horizontal;
        }
        if(joystick.Vertical < 0.2 && joystick.Vertical > -0.2)
        {
            verticalMov = 0;
        }
        else
        {
            verticalMov = joystick.Vertical;
        }
        move = new Vector3(horizontalMov, 0, verticalMov).normalized * speed;
        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");

        if(enemy != null)
        {
            if (bullets > 0 && isFiring == true)
            {
                if (nextFire < Time.time)
                {
                    transform.LookAt(enemy.transform);
                    nextFire = Time.time + fireRate;
                }
            }
        }


        if (transform.position.x > 23f || transform.position.x < -23f || transform.position.z > 7.5f || transform.position.z < -7.5f)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            StartCoroutine(levelload());
        }


        if (bullets <= 0 && weaponInstantiated == 0)
        {
            weaponInstantiated = 1;
            GameObject weaponn = Instantiate(weapon, new Vector3(enemy.transform.position.x, 1f, enemy.transform.position.z), Quaternion.identity);
        }

    }

    IEnumerator levelload()
    {
        loadlevel.SetBool("close", true);

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * Time.deltaTime);
        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    public void TakeHit()
    {
        health -= damage;
        if(health <= 0)
        {
            StartCoroutine(levelload());
        }
    }
}
