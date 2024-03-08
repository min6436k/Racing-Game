using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;

[Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float BreakForce;

    public bool Drift = false;

    public Transform center;

    public bool IsPlayer;

    public Transform WayPoints;
    public bool bPassLastPoint = false;
    private Transform TargetPoint;
    private int WayIndex = 0;

    private Rigidbody rigid;


    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.centerOfMass = center.localPosition;

        TargetPoint = WayPoints.GetChild(WayIndex);

        if (gameObject.CompareTag("Player")) IsPlayer = true;
        else IsPlayer = false;

        if (Reverse) WayIndex = WayPoints.childCount - 1;
    }
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = 1000;
        float steering = 0;
        float Break = 0;

        if (IsPlayer)
        {
            motor = maxMotorTorque * Input.GetAxis("Vertical");
            steering = maxSteeringAngle * Input.GetAxis("Horizontal");
            Break = Input.GetKey(KeyCode.Space) ? BreakForce : 0;
        }
        else
        {
            Vector3 WaypointDistance = transform.InverseTransformPoint(TargetPoint.position);
            WaypointDistance = WaypointDistance.normalized;
            steering = WaypointDistance.x * 25;
        }

        if (Vector3.Distance(TargetPoint.position, transform.position) <= 10)
        {
            if (WayPoints.childCount > WayIndex + 1)
            {
                WayIndex++;

                if (WayIndex == WayPoints.childCount - 1)
                {
                    WayIndex = 0;
                    bPassLastPoint = true;
                }
                TargetPoint = WayPoints.GetChild(WayIndex);
            }
        }

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            axleInfo.leftWheel.brakeTorque = Break;
            axleInfo.rightWheel.brakeTorque = Break;

            WheelFrictionCurve temp = axleInfo.leftWheel.sidewaysFriction;

            if (Drift) temp.stiffness = 1;
            else temp.stiffness = 5;

            axleInfo.leftWheel.sidewaysFriction = temp;
            axleInfo.rightWheel.sidewaysFriction = temp;


            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    private void Update()
    {
        if (IsPlayer)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                rigid.AddForce(transform.forward * 20000, ForceMode.Impulse);
            } //µð¹ö±ë

            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.position = WayPoints.GetChild(WayIndex == 0 ? WayPoints.childCount-2 : WayIndex - 1).position;
                transform.position += new Vector3(0, 1.5f, 0);
                rigid.velocity = Vector3.zero;

                transform.LookAt(TargetPoint.position);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Drift = true;
            }
            else Drift = false;

        }

    }
}