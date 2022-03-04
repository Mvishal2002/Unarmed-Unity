using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;
    public NavMeshAgent nav;
    public Transform player;
    float myColliderRadius;
    float TargetColliderRadius;
    public GameObject bullet;
    //public GameObject enemy;
    float nextTime;
    float fireRate ;
    PlayerMov PlayerMovement;
    int health = 50;
    int damage = 10;
    public Text text;
    public Text highScore;
    public Camera cam;
    public Material dissolve;
    float initial = 1f;

    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        myColliderRadius = GetComponent<CapsuleCollider>().radius;
        if(player != null)
        {
            TargetColliderRadius = player.GetComponent<CapsuleCollider>().radius;
            PlayerMovement = player.GetComponent<PlayerMov>();
        }
        StartCoroutine(stop());
        StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve()
    {
        while (initial > 0)
        {
            initial -= 0.05f;
            dissolve.SetFloat("Vector1_430AD54F", initial);
            yield return null;
        }
    }
    IEnumerator stop()
    {
        yield return new WaitForSeconds(0.5f);
        text.GetComponent<Animator>().SetBool("open", false);
    }
    // Update is called once per frame
    void Update()
    {
        fireRate = (210 - 0.5f * Time.time) / 300;

        if(player != null)
        {
            if(player.position.y > 0f)
            {
                transform.LookAt(player);
                Vector3 target = player.position - (player.position - transform.position).normalized * (myColliderRadius + TargetColliderRadius + 2f);
                nav.SetDestination(target);
                if (nextTime < Time.time)
                {
                    nextTime = Time.time + fireRate;
                    GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);
                    Bullet.GetComponent<Rigidbody>().velocity = (player.position - transform.position).normalized * 10f;
                    Destroy(Bullet, 2f);
                }
            }
        }
    }

    public void TakeDamage()
    {
        health -= damage;
        if(health == 0)
        {
            StartCoroutine(shake());
        }
    }

    public IEnumerator shake()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.FromToRotation(Vector3.forward, (transform.position - player.position))), 2f);
        Vector3 originalPos = cam.transform.localPosition;

        float elapsed = 0;
        while (elapsed < 0.15f)
        {
            elapsed += Time.deltaTime;
            cam.transform.localPosition += new Vector3(Random.Range(-1f, 1f) * 0.4f, Random.Range(-1f, 1f) * 0.4f, 0);
            yield return null;
        }
        cam.transform.localPosition = originalPos;
        Instantiate(gameObject, new Vector3(Random.Range(-20, 20), transform.position.y, Random.Range(-5.5f, 5.5f)), Quaternion.identity);
        PlayerMovement.enemiesKilled++;
        text.text = PlayerMovement.enemiesKilled.ToString("0");
        PlayerPrefs.SetInt("score", PlayerMovement.enemiesKilled);
        text.GetComponent<Animator>().SetBool("open", true);
        if (PlayerMovement.enemiesKilled > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerMovement.enemiesKilled);
            highScore.text = PlayerMovement.enemiesKilled.ToString();
        }
        Destroy(gameObject);
    }
}

