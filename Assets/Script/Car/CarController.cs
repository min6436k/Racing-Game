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

    public Transform center;

    public bool IsPlayer;

    public Transform WayPoints;
    public bool bPassLastPoint = false;
    public int WayIndex = 0;
    public Vector3 TargetPoint;

    private Rigidbody rigid;


    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.centerOfMass = center.localPosition;

        TargetPoint = WayPoints.GetChild(WayIndex).position;

        if (gameObject.CompareTag("Player")) IsPlayer = true;
        else IsPlayer = false;

        switch(GameInstance.instance.ShopItems.Find(x => x.type == EnumTypes.Shop.Engine).ShopItemRank)
        {
            case 1:
                maxMotorTorque *= 1.4f;
                break;
            case 2:
                maxMotorTorque *= 2f;
                break;

        }
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
        WheelMove();


    }

    private void WheelMove()
    {
        float motor = maxMotorTorque;
        float steering = 0;
        float Break = 0;

        if (IsPlayer)
        {
            motor = maxMotorTorque * Input.GetAxis("Vertical");
            steering = maxSteeringAngle * Input.GetAxis("Horizontal");
            Break = Input.GetKey(KeyCode.Space) ? BreakForce : 0;

            if (GameInstance.instance.CurrentStage > GameInstance.instance.ShopItems.Find(x => x.type == EnumTypes.Shop.Tier).ShopItemRank)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit))
                {
                    if (hit.collider.name == "SlowTerrain")
                    {
                        motor /= 3;
                    }
                }
            }
        }
        else
        {
            Vector3 WaypointDistance = transform.InverseTransformPoint(TargetPoint);
            WaypointDistance = WaypointDistance.normalized;
            steering = WaypointDistance.x * maxSteeringAngle;
        }

        if (Vector3.Distance(TargetPoint, transform.position) <= (IsPlayer ? 35 : 15))
        {
            if (WayPoints.childCount > WayIndex + 1)
            {
                WayIndex++;

                if (WayIndex == WayPoints.childCount - 1)
                {
                    WayIndex = 0;
                    bPassLastPoint = true;
                }

                GameManager.instance.SpawnNPC(TargetPoint);

                TargetPoint = WayPoints.GetChild(WayIndex).position;
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

                transform.LookAt(TargetPoint);
            }

        }
    }
}