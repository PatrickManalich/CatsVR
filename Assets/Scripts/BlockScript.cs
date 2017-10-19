using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update() { }

    void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.Reflect(collision.relativeVelocity * -1 / rb.mass, collision.contacts[0].normal);
    }
}
