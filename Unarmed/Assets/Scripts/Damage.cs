using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    int collided = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy") && collided == 0)
        {
            collided = 1;
            collision.collider.GetComponent<Enemy>().TakeDamage();
        }
    }
}
