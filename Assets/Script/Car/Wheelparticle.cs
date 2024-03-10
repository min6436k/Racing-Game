using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelparticle : MonoBehaviour
{
    public ParticleSystem[] WheelParticle;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        ParticleSystem.EmissionModule Emission;

        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            foreach (var p in WheelParticle)
            {
                Emission = p.emission;

                if (hit.distance > 2 || rb.velocity.magnitude * 3.6f < 40)
                    Emission.rateOverTime = 0;
                else
                {
                    Emission.rateOverTime = Mathf.Clamp(rb.velocity.magnitude * 3.6f, 50, 100);
                }

            }
        }
    }
}
