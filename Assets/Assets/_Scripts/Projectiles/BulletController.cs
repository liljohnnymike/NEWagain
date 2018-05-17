using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public GameObject Owner;

    public float DamageAmount = 10; // How much damage this bullet does

    public float BulletSpeed = 50; // How fast this bullet should shoot

    public float BulletDelay = .2f; // How often can we shoot this bullet

    public void OnTriggerEnter(Collider other) //other == object we collide with
    {
        if (other.gameObject != Owner) //Verify that bullet is not in collision with owner
        {
            HealthComponent Temp = other.GetComponent<HealthComponent>(); // Store a reference to the owners healthcomponent
            if(Temp != null) //if the owner has a health component
            {
                Temp.TakeDamage(DamageAmount); // Call take damage from healthcomponent
            }
            Destroy(gameObject);
        }
    }

}
