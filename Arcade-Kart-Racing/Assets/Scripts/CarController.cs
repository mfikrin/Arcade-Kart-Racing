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


    public bool canControl;

    public TrackZone curTrackZone;
    public int zonesPassed;
    public int racePosition;
    public int curLap;

    void Start()
    {
        startModelOffset = carModel.transform.localPosition; 
        GameManager.instance.cars.Add(this);
        transform.position = GameManager.instance.spawnPoints[GameManager.instance.cars.Count - 1].position;
    }

    void FixedUpdate()
    {
        if (!canControl)
        {
            return;
        }


        if (accelerateInput)
        {
            rig.AddForce(carModel.forward * acceleration, ForceMode.Acceleration);
        }            
    }
    void Update()
    {
        if (!canControl)
        {
            turnInput = 0.0f ;
        }
        float turnRate = Mathf.Abs(Vector3.Dot(rig.velocity.normalized,carModel.forward));

        curYRot += turnInput * turnSpeed * turnRate * Time.deltaTime;

        carModel.transform.position = transform.position + startModelOffset;
        //carModel.transform.eulerAngles = new Vector3(0,curYRot,0);

        CheckGround();
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

    void CheckGround()
    {
        // shoot raycast
        Ray ray = new Ray(transform.position + new Vector3(0,-0.75f,0), Vector3.down);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,1f))
        {
            carModel.up = hit.normal;
        }
        else
        {
            carModel.up = Vector3.up;
        }

        carModel.Rotate(new Vector3(0, curYRot, 0),Space.Self);

    }


}



