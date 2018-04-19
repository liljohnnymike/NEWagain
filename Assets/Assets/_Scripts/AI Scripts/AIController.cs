using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    public GameObject TargetPoint;
    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>(); //Gets the component on the object script attached to
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(TargetPoint.transform.position);
	}
}
