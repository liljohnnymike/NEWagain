using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {

    public float MaxHealth = 50;
    public float CurrentHealth;
    private bool damaged;

    // Use this for initialization
    void Start() {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
            CurrentHealth -= amount;
    }

	// Update is called once per frame
	void Update () {
    if (CurrentHealth <= 0)
    {
        this.gameObject.SetActive(false);
            Debug.Log("Dead");
    }
	}
}
