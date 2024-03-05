using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class wheel : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float BreakForce;

    public Transform center;

    public bool IsPlayer;

    public Transform WayPoints;
    private Transform TargetPoint;
    private int WayIndex = 0;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = center.localPosition;

        if (!IsPlayer) TargetPoint = WayPoints.GetChild(WayIndex);
    }
    // finds the corresponding visual wheel
    // correctly applies the transform
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
            if(Vector3.Distance(TargetPoint.position,transform.position) <= 10 && WayPoints.childCount > WayIndex+1)
            {
                WayIndex++;
                TargetPoint = WayPoints.GetChild(WayIndex);

                if (WayPoints.childCount - 1 == WayIndex) WayIndex = 0;
            }

            Vector3 waypointRelativeDistance = transform.InverseTransformPoint(TargetPoint.position);
            waypointRelativeDistance /= waypointRelativeDistance.magnitude;
            steering = (waypointRelativeDistance.x / waypointRelativeDistance.magnitude) * 25;

        } //더러우니 수정 필요, 그대로 복사하지 말것. 절대.


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

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            rb.AddForce(transform.forward * 20000, ForceMode.Impulse);
        }
    }
}