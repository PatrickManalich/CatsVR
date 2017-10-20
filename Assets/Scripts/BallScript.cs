using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Placed on games objects with the 'Ball' tag. Simply causes the ball to bounce when colliding with other game objects.
 * Also has a function 'OnEnable()' which places the balls around the 'BallIcon' and adds a random impulse force.
 */

public class BallScript : MonoBehaviour {

    private Rigidbody rb;

	void Start ()  { }

    void Update () { }

    void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.Reflect(collision.relativeVelocity * -1 / rb.mass, collision.contacts[0].normal);
    }
    
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        if (name == "BallBlue")
            transform.position = new Vector3(-1f, 0.8f, -0.15f);
        else if (name == "BallGreen")
            transform.position = new Vector3(-1f, 0.55f,0.15f);
        else
            transform.position = new Vector3(-1f, 0.3f, 0f);
        rb.AddForce(new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(1.0f, 5.0f), Random.Range(-5.0f, 5.0f)), ForceMode.Impulse);
    }
}
