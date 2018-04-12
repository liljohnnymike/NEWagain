using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

   public enum States
    {
        OutOfCOmbatState,
        MovementState,
        CombatState,
    }

    States CurrentState;

    private Rigidbody RB;
    public Animator Anim;
    public float MovementSpeed = 100;


    public GameObject Camera;
    public Vector3 IP; // Movement Input

    float DT;

	// Use this for initialization
	void Start () {

        RB = GetComponent<Rigidbody>(); //Gets Rigidbody on object
        // anim passed in through editor
	}
	
    public void KeyInput()
    {
        IP.x = Input.GetAxisRaw("Horizontal");
        IP.z = Input.GetAxisRaw("Vertical");
      }


    //Movement Input...
    public void MovementInput(float DeltaTime, Vector3 MoveInput, Transform DirTrans)
    {
        if (RB != null)
        {

            Vector3 Forward = DirTrans.forward * MoveInput.z;
            Forward.y = 0;
            Forward.Normalize();

            Vector3 Right = DirTrans.right * MoveInput.x;

            RB.AddForce(Forward * MovementSpeed * DT);
            RB.AddForce(Right * MovementSpeed * DT);

            ////RB.AddForce(MoveInput * MovementSpeed * DeltaTime);
            //float StoredYVelocity = RB.velocity.y;
            //Vector3 NewVelocity = MoveInput * MovementSpeed * DeltaTime;
            //Vector3 Vel = new Vector3(NewVelocity.x, StoredYVelocity, NewVelocity.z);
            //RB.velocity = Vel;
        }
       
    }

    //Animation...
    public void updateAnim(Animator AnimationController)
    {
        if (AnimationController != null)
        {
            var localVel = transform.InverseTransformDirection(RB.velocity);
            AnimationController.SetFloat("ForwardSpeed", localVel.z);
            AnimationController.SetFloat("RightSpeed", localVel.x);
        }
    }


    //Combat States...
    public void dodoOutOfCombat()
    {
        if(RB.velocity.magnitude != 0)
        {
            CurrentState = States.MovementState;
        }
    }
    public void doOutOfCombat()
    {
        MovementInput(DT, IP, Camera.transform);
    }
    public void doCombat()
    {

    }

    // Update is called once per frame
    void Update()
    {

        DT = Time.deltaTime;

        updateAnim(Anim);
    }
        private void FixedUpdate()
    {
        switch (CurrentState)
        {
            case States.OutOfCOmbatState:
                doOutOfCombat();
                break;

            case States.MovementState:
                break;

            case States.CombatState:
                break;
        }

        KeyInput();
        updateAnim(Anim);
    }
       
	
}
