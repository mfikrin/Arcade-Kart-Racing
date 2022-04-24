using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float acceleration;
    public float turnSpeed;

    public Transform carModel;

    private Vector3 startModelOffset;

    public float groundCheckRate;
    private float lastGroundCheckTime;

    private float curYRot;
    private bool accelerateInput;
    private float turnInput;

    public Rigidbody rig;


    //public bool canControl;

    //public TrackZone curTrackZone;
    //public int zonesPassed;
    //public int racePosition;
    //public int curLap;

    void Start()
    {
        startModelOffset = carModel.transform.localPosition;    
    }

    void FixedUpdate()
    {
        if (accelerateInput)
        {
            rig.AddForce(carModel.forward * acceleration, ForceMode.Acceleration);
        }            
    }
    void Update()
    {
        carModel.transform.position = transform.position + startModelOffset;
        carModel.transform.eulerAngles = new Vector3(0,carModel.eulerAngles.y,0);
    }
    public void OnAccelerateInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            accelerateInput = true;
        }
        else
        {
            accelerateInput = false;
        }
    }

    public void OnTurnInput(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();

    }


}



