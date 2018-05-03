using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamaheHitbox : MonoBehaviour {

    public float damageAmount;

    public void OntriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<HealthComponent>().TakeDamage(damageAmount);
        }
    }

}