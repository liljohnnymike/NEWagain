using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //include AI

public class AIController : MonoBehaviour {

    public enum AIStates
    {
        ChaseState,
        AttackState,
        DeathState
    }

    public AIStates CurrentState;

    public GameObject TargetPoint; //Point to move towards

    private NavMeshAgent agent;

    public float AttackRange = 10.0f;

    public Rigidbody RB;

    public Animator anim;

    public bool isAttacking = false;

    public float AttackDelay;
    public float SetAttackDelay;

    public int EnemyCount;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>(); //Gets the component on the object that the script is attached to.
        agent.SetDestination(TargetPoint.transform.position);

        //TODO: Make each instance of the AI increase Enemy Count by one, one time
        EnemyCount++;
    }

    public void updateAnim(Animator AnimationController)
    {
        if (AnimationController != null)
        {
            var localVel = transform.InverseTransformDirection(agent.velocity);
           
            AnimationController.SetFloat("ForwardSpeed", localVel.z);
        }
    }

    public bool InAttackRange()
    {

        float DistanceToTarget = agent.remainingDistance; //getting the remaining distance of our AI path

        if (DistanceToTarget <= AttackRange) //checking to see if target is within attackrange
        {
            return true; //this function returns true
        }
        else //if target is not within attack range move towards it
        {
            return false; //this function returns false
        }

    }

    public void doChase(GameObject target)
    {
        if(target == null) //make sure we have a target
        {
            Debug.Log("Chase Can Not Run Due To Null Reference of Target");
            return; //if we dont have a target then return out of function
        }

        if(agent.isStopped) { agent.isStopped = false; } // Set stopped to false

        if (InAttackRange()) //checking to see if target is within attackrange
        {
            CurrentState = AIStates.AttackState;
            return;
        }

        agent.SetDestination(target.transform.position); //move to target
        
    }

    public void doAttack(float DeltaTime)
    {
        if (!agent.isStopped) { agent.isStopped = true; } // Set stopped to false

        if (!InAttackRange() && !isAttacking) //checking to see if target is within attackrange
        {
            CurrentState = AIStates.ChaseState;
            return;
        }

        if(SetAttackDelay <= 0)
        {
            anim.SetTrigger("Attack");
            isAttacking = true;
            SetAttackDelay = AttackDelay;
        }

        agent.SetDestination(TargetPoint.transform.position); //move to target
    }

    public void doDeath(bool isDead)
    {

    }

	// Update is called once per frame
	void Update () {

        switch (CurrentState) // Current State = ...
        {
            case AIStates.ChaseState: // AIStates.ChaseState ???
                doChase(TargetPoint);
                break;
            case AIStates.AttackState: // AIStates.AttackState ???
                doAttack(Time.deltaTime);
                break;
            case AIStates.DeathState: // AIStates.DeathState ???
                doDeath(false); //TODO: Create death variable to check
                break;
        }

        SetAttackDelay -= Time.deltaTime;

        updateAnim(anim);

	}
}
