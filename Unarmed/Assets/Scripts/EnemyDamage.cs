using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    int collided = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && collided == 0)
        {
            collided = 1;
            collision.collider.GetComponent<PlayerMov>().TakeHit();
        }
    }
}
