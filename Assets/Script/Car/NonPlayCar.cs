using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NonPlayCar : MonoBehaviour
{
    public List<AxleInfo> axleInfos;

    public Transform center;

    public Vector3 EndPoint;
    private Transform WayPoints;
    private Vector3 TargetPoint;
    private int WayIndex;

    private CarController playerController;
    void Start()
    {
        playerController = GameManager.instance.Player.GetComponent<CarController>();

        GetComponent<Rigidbody>().centerOfMass = center.localPosition;

        WayPoints = playerController.WayPoints;

        WayIndex = playerController.WayIndex + 3;

        if (WayIndex >= 0 && WayIndex < WayPoints.childCount)
            TargetPoint = WayPoints.GetChild(WayIndex).position;



        transform.position = WayPoints.GetChild(WayIndex).position;
        transform.position += new Vector3(0, 1.5f, 0);

        transform.LookAt(WayPoints.GetChild(WayIndex-1));

        Destroy(gameObject, 8f);
    }

    void FixedUpdate()
    {
        float motor = 1000;
        float steering = 0;
        float Break = 0;


        Vector3 WaypointDistance = transform.InverseTransformPoint(TargetPoint);
        WaypointDistance = WaypointDistance.normalized;
        steering = WaypointDistance.x * 25;

        if (Vector3.Distance(TargetPoint, transform.position) <= 15)
        {
            WayIndex--;
            TargetPoint = WayPoints.GetChild(WayIndex).position;
            if (TargetPoint == EndPoint) Destroy(gameObject);

            TargetPoint += new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));

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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Rigidbody>().mass = 20;
            Destroy(gameObject, 1.5f);
        }
    }
}
