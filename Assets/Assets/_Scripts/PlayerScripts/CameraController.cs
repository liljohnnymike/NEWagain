using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public enum States
    {
        TopDown,
        PostUpInRoom
    }

    public States CurrentState;

    public Transform HoldPoint;

    public float Speed = 10; // Move speed for camera
    public float LookSpeed = 10; // Move speed for camera

    public PlayerController Player; // Player in world

    public Vector3 Offset; // Offset for camera to sit at
    public Quaternion Rot;
    void CameraMove(Vector3 Location, Quaternion Rotation)
    {
        transform.position = Vector3.MoveTowards(transform.position, Location, Speed * Time.deltaTime); // Moves us towards location
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotation, LookSpeed); // Rotates us towards rotation
    }

    // Use this for initialization
    void Start () {
        Offset = transform.position - Player.transform.position; // Gets the offset of camer at start
        Rot = transform.rotation; // Storing reference to starting rotation
	}
	
	// Update is called once per frame
	void LateUpdate () {
        
        switch(CurrentState)
        {
            case States.TopDown:
                CameraMove(Player.transform.position + Offset, Rot); // Moveing to start offset and rotation
                break;
            case States.PostUpInRoom:
                CameraMove(HoldPoint.transform.position, HoldPoint.transform.rotation); // Moveing towards holdpoint position and rotation
                break;
        }
    }
}
