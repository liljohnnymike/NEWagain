using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
        public GameObject enemy;                // The enemy prefab to be spawned.
        public float spawnTime = 3f;            // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
        public int EnemyCount;                // The amount of enemies in the level.

    public GameObject AITagret;
        

        void Start()
        {
        AITagret = GameObject.FindObjectOfType<PlayerController>().gameObject;

              // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
              InvokeRepeating("Spawn", spawnTime, spawnTime);
        }


        void Spawn()
        {
        


        // Increases the enemy counter...Doesn't work perfectly...
        EnemyCount++;

        // If the player has no health left...
        if (EnemyCount > 5)
            {
            // ... exit the function.
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            GameObject TempAI = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            TempAI.GetComponent<AIController>().TargetPoint = AITagret;
    }
} 