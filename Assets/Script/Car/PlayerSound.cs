using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource Engine;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            Engine.pitch = Mathf.Lerp(Engine.pitch, Mathf.Clamp(rb.velocity.magnitude * 3.6f / 20, 1, 1.25f), 2 * Time.deltaTime);
            
        }
        else
        {
            Engine.pitch = Mathf.Lerp(Engine.pitch, 1, 2 * Time.deltaTime);
        }
    }
}
