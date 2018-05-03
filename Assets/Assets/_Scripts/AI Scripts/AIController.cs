using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //include AI

public class AIController : MonoBehaviour {

    public enum AIStates
    {
        ChaseState,
        AttackState,
        DeathState,
    }

    public AIStates CurrentState;

    public GameObject TargetPoint; //Point to move to

    private NavMeshAgent agent;

    public float AttackRange = 10f;

    public Animator anim;

    public Rigidbody RB;

    public float AttackDelay;

    public float SetAttackDelay;

    public bool isAttacking;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>(); //Gets the component on the object that the script is attached to
        agent.SetDestination(TargetPoint.transform.position);
    }
	
    public bool InAttackRange()
    {
        
        float DistanceToTarget = agent.remainingDistance;

        if (DistanceToTarget <= AttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void updateAnim(Animator AnimationController)
    {
        if (AnimationController != null)
        {
            var localVel = transform.InverseTransformDirection(agent.velocity);
            AnimationController.SetFloat("ForwardSpeed", localVel.z);
        }
    }

    public void doChase(GameObject target)
    {
        if(target == null)
        {
            Debug.Log("Chase cannot Runt due to Null Reference of Target");
            return; 
        }

        if (agent.isStopped) { agent.isStopped = false; } //Set stopped to false

        float DistanceToTarget = agent.remainingDistance;

        if (DistanceToTarget <= AttackRange)
        {
           CurrentState = AIStates.AttackState; //Checking to see if target is within attack range...
            return;
        }

        agent.SetDestination(TargetPoint.transform.position);
    }

    public void doAttack(float DeltaTime)
    {
        if (!agent.isStopped) { agent.isStopped = true; }

        if (InAttackRange())
        {
            CurrentState = AIStates.ChaseState;
            return;
        }


 

        agent.SetDestination(TargetPoint.transform.position);
    }

    public void doDeath(bool IsDead)
    {

    }

    // Update is called once per frame
    void Update () {

        switch (CurrentState)
        {
            case AIStates.ChaseState:
                doChase(TargetPoint);
                break;

            case AIStates.AttackState:
                doAttack(Time.deltaTime);
                break;

            case AIStates.DeathState:
                doDeath(false); //TODO: Create death variable
                break;
        }

        SetAttackDelay -= DeltaTime;
        if (SetAttackDelay <= 0)
        {
            anim.SetTrigger("Attack");
            isAttacking = true;
            SetAttackDelay = AttackDelay;
        }

        updateAnim(anim);
        agent.SetDestination(TargetPoint.transform.position);
	}
}
