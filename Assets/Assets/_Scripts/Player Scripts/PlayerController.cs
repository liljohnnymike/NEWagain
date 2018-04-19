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
    public float SmoothDamp = 10;

    public Camera cam;

    public Vector3 IP; // Movement Input

    float DT;

    public GameObject bullet;
    private float AttackDelay = 0.8f;
    public float SetAttackDelay;
    public bool attacking;

	// Use this for initialization
	void Start () {

        RB = GetComponent<Rigidbody>(); //Gets Rigidbody on object
        // anim passed in through editor
	}
	
    public void KeyInput()
    {
        IP.x = Input.GetAxisRaw("Horizontal");
        IP.z = Input.GetAxisRaw("Vertical");
        attacking = Input.GetMouseButton(0);
      }


    //Movement Input...
    public void MovementInput(float DeltaTime, Vector3 MoveInput, Transform DirTrans)
    {
        if (RB != null)
        {

            Vector3 Forward = DirTrans.forward * MoveInput.z; //Transform forward multiplied by our Vertical Input...
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

    public void doLook()
    {
        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Creating ray from mouse on screen...

        if(Physics.Raycast(ray,out hit, 1000000)) //Casting out ray and populating our 'hit' value...
        {
            Vector3 forward = (transform.position - hit.point) * -1; //Getting the direction between our position and our hit point position...
            forward.y = 0; //Zeroing out the Y...
            forward.Normalize(); //Normalize to calculate direction...
            transform.forward = Vector3.MoveTowards(transform.forward, forward, Time.deltaTime * SmoothDamp); //Move our forward toward the direction between the position...
            
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
        MovementInput(DT, IP, cam.transform);
    }
    public void doCombat()
    {
        if (attacking)
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }


    // Update is called once per frame
    void Update()
    {
       
        DT = Time.deltaTime;

        updateAnim(Anim);
    }


    private void FixedUpdate()
    {

      
        doLook();
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
