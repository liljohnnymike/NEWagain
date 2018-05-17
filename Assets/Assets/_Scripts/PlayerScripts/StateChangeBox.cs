using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeBox : MonoBehaviour {

    public GameObject HoldPoint;
    public CameraController.States StoredState;

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().cam.GetComponent<CameraController>().CurrentState = StoredState;
            other.GetComponent<PlayerController>().cam.GetComponent<CameraController>().HoldPoint = HoldPoint.transform;
        }
    }

}
