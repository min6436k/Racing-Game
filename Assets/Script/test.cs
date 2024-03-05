using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public AudioClip carSound;
    public float motorTorque;
    public float maxSteer;
    public float breakTorque;
    public Transform com;
    public float skidMarkSens = 0.3f;

    public Transform wheelTransformFR;
    public Transform wheelTransformFL;
    public Transform wheelTransformBR;
    public Transform wheelTransformBL;

    public TrailRenderer wheelFRTrail;
    public TrailRenderer wheelFLTrail;
    public TrailRenderer wheelBRTrail;
    public TrailRenderer wheelBLTrail;

    public WheelCollider wheelColliderFR;
    public WheelCollider wheelColliderFL;
    public WheelCollider wheelColliderBR;
    public WheelCollider wheelColliderBL;

    public int steerAngleReductionMultiplyer;
    public int speedWhenAngleReductionValueKicks;

    bool breaksON = false;
    bool isSkiding;

    Rigidbody rb;

    float x;
    float y;

    void Start()
    {
        rb = transform.gameObject.GetComponent<Rigidbody>();
        transform.GetComponent<Rigidbody>().centerOfMass = com.localPosition;
    }

    void FixedUpdate()
    {
        if (!wheelColliderFL.isGrounded && !wheelColliderFR.isGrounded && !wheelColliderBL.isGrounded && !wheelColliderBR.isGrounded) balance();
        float currSpeed = rb.velocity.magnitude * 2; //X2 for making it equal to speedometer speed
                                                     //Debug.Log(currSpeed);
                                                     //steerangle reduction not working properly
        float steerAngleReduction = 1;
        if (currSpeed > speedWhenAngleReductionValueKicks) { steerAngleReduction = steerAngleReductionMultiplyer / currSpeed; }
        steerAngleReduction = Mathf.Clamp(steerAngleReduction, 0.2f, 1f);
        //Debug.Log(steerAngleReduction);
        x = Input.GetAxis("Vertical");
        float currTorque = x * motorTorque;
        wheelColliderBL.motorTorque = currTorque;
        wheelColliderBR.motorTorque = currTorque;
        wheelColliderFL.motorTorque = currTorque;
        wheelColliderFR.motorTorque = currTorque;

        y = Input.GetAxis("Horizontal");
        float currSteer = maxSteer * y;
        wheelColliderFL.steerAngle = currSteer * steerAngleReduction;
        wheelColliderFR.steerAngle = currSteer * steerAngleReduction;

        breaking();



        bool completelyInAir = !wheelColliderBL.isGrounded && !wheelColliderBR.isGrounded && !wheelColliderFL.isGrounded && !wheelColliderFR.isGrounded;

        if (Mathf.Abs(transform.GetComponent<Rigidbody>().velocity.magnitude) < 5) isSkiding = false;


    }

    void Update()
    {

        Vector3 posFR = Vector3.zero;
        Quaternion rotFR = Quaternion.identity;
        wheelColliderFR.GetWorldPose(out posFR, out rotFR);
        wheelTransformFR.SetPositionAndRotation(posFR, rotFR);

        Vector3 posFL = Vector3.zero;
        Quaternion rotFL = Quaternion.identity;
        wheelColliderFL.GetWorldPose(out posFL, out rotFL);
        wheelTransformFL.SetPositionAndRotation(posFL, rotFL);

        Vector3 posBL = Vector3.zero;
        Quaternion rotBL = Quaternion.identity;
        wheelColliderBL.GetWorldPose(out posBL, out rotBL);
        wheelTransformBL.SetPositionAndRotation(posBL, rotBL);

        Vector3 posBR = Vector3.zero;
        Quaternion rotBR = Quaternion.identity;
        wheelColliderBR.GetWorldPose(out posBR, out rotBR);
        wheelTransformBR.SetPositionAndRotation(posBR, rotBR);

        //makeTrail();

    }

    void breaking()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            wheelColliderBL.brakeTorque = breakTorque;
            wheelColliderBR.brakeTorque = breakTorque;
            breaksON = true;
        }
        else
        {
            wheelColliderBL.brakeTorque = 0;
            wheelColliderBR.brakeTorque = 0;
            breaksON = false;
        }
    }

    void makeTrail()
    {
        Vector3 vel = transform.GetComponent<Rigidbody>().velocity.normalized;
        Vector3 obj = transform.forward;

        isSkiding = Mathf.Abs(vel.x - obj.x) * sign(x) > skidMarkSens || Mathf.Abs(vel.y - obj.y) * sign(x) > skidMarkSens || Mathf.Abs(vel.z - obj.z) * sign(x) > skidMarkSens;

        //when the car is moving in reverse direction it shows its skidding


        if ((wheelColliderBL.isGrounded && isSkiding) || (breaksON && wheelColliderBL.motorTorque == 0 && wheelColliderBL.isGrounded))
        {
            wheelBLTrail.emitting = true;
        }
        else
        {
            wheelBLTrail.emitting = false;
        }

        if ((wheelColliderBR.isGrounded && isSkiding) || (breaksON && wheelColliderBR.motorTorque == 0 && wheelColliderBR.isGrounded))
        {
            wheelBRTrail.emitting = true;
        }
        else
        {
            wheelBRTrail.emitting = false;
        }

        if (wheelColliderFL.isGrounded && isSkiding)
        {
            wheelFLTrail.emitting = true;
        }
        else
        {
            wheelFLTrail.emitting = false;
        }

        if (wheelColliderFR.isGrounded && isSkiding)
        {
            wheelFRTrail.emitting = true;
        }
        else
        {
            wheelFRTrail.emitting = false;
        }

    }



    int sign(float x)
    {
        if (x < 0) return -1;
        else return 1;
    }

    public void stopCar()
    {
        wheelColliderBL.brakeTorque = breakTorque * 20;
        wheelColliderBR.brakeTorque = breakTorque * 20;
    }

    void balance()
    {
        if (rb.velocity.magnitude > 10)
        {
            Quaternion target = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
        }
    }

}