using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum States
    {
        OutOfCombatState,
        CombatState
    }

    public States CurrentState;

    private Rigidbody RB;
    public Animator Anim;
    public float MovementSpeed = 100;

    public bool Sprint;
    public float WalkSpeed = 200;
    public float SprintSpeed = 400;
    public float MaxSpeed;
    public float MaxWalkSpeed;
    public float MaxSprintSpeed;
    public float SmoothDamp = 10;

    public float BulletSpeed = 10;
    public GameObject bullet;

    public Vector3 IP; // Movement Input

    public Camera cam;

    float DT;

    private float AttackDelay = 1.0f;
    public float SetAttackDelay = .01f;
    public bool Attacking;
    // Use this for initialization
    void Start()
    {

        RB = GetComponent<Rigidbody>(); //Gets Rigidbody on object
                                        // anim passed in through editor
    }

    public void KeyInput()
    {
        IP.x = Input.GetAxisRaw("Horizontal");
        IP.z = Input.GetAxisRaw("Vertical");
        Sprint = Input.GetKey(KeyCode.LeftShift);
        Attacking = Input.GetMouseButton(0);
    }

    public void MovementInput(float DeltaTime, Vector3 MoveInput, Transform DirTrans)
    {
        if (RB != null)
        {
            Vector3 Forward = DirTrans.forward * MoveInput.z; // Transform forward multiplied by our Vertical input (W,S)
            Forward.y = 0;
            Forward.Normalize();


            Vector3 Right = DirTrans.right * MoveInput.x;

            RB.AddForce(Forward * MovementSpeed * DT);
            RB.AddForce(Right * MovementSpeed * DT);
        }
    }

    public void updateAnim(Animator AnimationController)
    {
        if (AnimationController != null)
        {
            var localVel = transform.InverseTransformDirection(RB.velocity);
            AnimationController.SetFloat("ForwardSpeed", localVel.z);
            AnimationController.SetFloat("RightSpeed", localVel.x);
        }
    }


    public void doLook() //Rotate our player towards the mouse cursor
    {
        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Creating Ray from mouse point on screen

        if (Physics.Raycast(ray, out hit, 1000000)) //Casting out a ray and populating our hit value
        {

            Vector3 forward = (transform.position - hit.point) * -1; // getting the direction between our postion and our hitpoint position
            forward.y = 0; // zeroing out the y to stay upright
            forward.Normalize(); // Normalize to calculate direction

            transform.forward = Vector3.MoveTowards(transform.forward, forward, Time.deltaTime * SmoothDamp); // move our forward towards the direction between the positions


        }

    }
    public void Movement()
    {
        MaxSpeed = (Sprint) ? MaxSprintSpeed : MaxWalkSpeed;
        MovementInput(DT, IP, cam.transform);

        float ClampedHorzVel = Mathf.Clamp(RB.velocity.x, -MaxSpeed, MaxSpeed);
        float ClampedVertVel = Mathf.Clamp(RB.velocity.z, -MaxSpeed, MaxSpeed);

        RB.velocity = new Vector3(Mathf.Clamp(RB.velocity.x, -MaxSpeed, MaxSpeed), RB.velocity.y, Mathf.Clamp(RB.velocity.z, -MaxSpeed, MaxSpeed));

        Temp = RB.velocity;
    }

    public Vector3 Temp;

    public void doOutOfCombat()
    {
        Movement();
    }

    public void DoFIre()
    {
       
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation);

        BulletController TempBC = temp.GetComponent<BulletController>();

        temp.GetComponent<Rigidbody>().velocity = transform.forward * TempBC.BulletSpeed;

        AttackDelay = TempBC.BulletDelay;

        TempBC.Owner = gameObject;
    }

    public void doCombat()
    {
        Movement();
        AttackDelay -= DT;
        if (Attacking && AttackDelay <= 0)
        {
            DoFIre();
        }
    }
    // Update is called once per frame
    void Update()
    {

        DT = Time.deltaTime;
        KeyInput();

        updateAnim(Anim);
    }

    private void FixedUpdate()
    {
        doLook();
        switch (CurrentState)
        {
            case States.OutOfCombatState:
                doOutOfCombat();
                break;
            case States.CombatState:
                doCombat();
                break;
        }
    }
}
