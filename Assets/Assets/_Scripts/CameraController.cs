using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Naming States...
    //public enum States
    //{
    //    LookAround,
    //    Default,
    //}

    //States CurrentState;

    public float Speed = .1f; // camera move speed
    public PlayerController Player;
    public Vector3 Offset; //Offset for camera to sit in world

    void CameraMove(Vector3 Location)
    {
        transform.position = Vector3.MoveTowards(transform.position, Location, Speed);
    }


    // Use this for initialization
    void Start()
    {
        Offset = transform.position - Player.transform.position;
    }

    //do Look around State...
    //public void doDefault()
    //{

    //}

    //public void doLookAround()
    //{
    //    if (Input.GetKeyDown("space"))
    //    {
    //        CurrentState = States.LookAround;
    //    }
    //    else
    //    {
    //        CurrentState = States.Default;
    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CurrentState);

        //CameraMove(Player.transform.position + Offset);

        if (Input.GetKey("space"))
        {
            CameraMove(Player.transform.position + Offset * 2);
        }
            else
            {
                CameraMove(Player.transform.position + Offset);
            }

        //Switching States...
        //switch (CurrentState)
        //{
        //    case States.Default:
        //        doDefault();
        //        break;

        //    case States.LookAround:
        //        doLookAround();
        //        break;
        }
    }
//}