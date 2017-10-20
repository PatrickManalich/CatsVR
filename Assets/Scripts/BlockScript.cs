using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Placed on game objects with the 'Block' tag. Simply causes the block to bounce when colliding with other game objects.
 */ 

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
