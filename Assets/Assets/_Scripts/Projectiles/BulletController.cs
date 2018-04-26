using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public GameObject Owner;

    public float DamageAmount = 10;

    public float BulletSpeed = 50;

    public float BulletDelay = .2f;

    public void OnTriggerEnter(Collider other) //other == object we collide with...
    {
        

        if (other .gameObject != Owner) //Verifies bullet is not colliding with owner...
        {
            var hit = other.gameObject;
            var health = hit.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.TakeDamage(10);
            }
            Destroy(gameObject);
        }
        
    }

}