using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Placed on a game object with the 'Cat' tag. When active, the cat will chase after the laser dot sprite
 * when the 'Follow()' function is being called. The cat will jump at the laser dot sprite when the 'Jump()'
 * function is called. The cat will pounce on the laser dot sprite when the 'Pounce()' function is called.
 */ 

public class CatLaserScript : MonoBehaviour {

    private float speed;
    private Animator anim;
    private int tailCurlHash;
    private Rigidbody rb;
    private float prevLaserHeight;
    private int time;
    private int timeWait;
    private bool free;


    private void Start () {
        speed = 1.0f;
        anim = GetComponent<Animator>();
        tailCurlHash = Animator.StringToHash("TailCurl");
        rb = GetComponent<Rigidbody>();
        free = true;
        prevLaserHeight = 0;
        time = 0;
        timeWait = 0;
    }
	
	private void Update () {
        if (time > timeWait)
        {
            time = 0;
            free = true;
        } else
            time++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Base" && free)
            Jump();
    }

    public void Follow(Vector3 target, Vector3 targetNormal)
    {
        if (free)
        {
            if(targetNormal != Vector3.down)
            {
                transform.LookAt(new Vector3(target.x, -1, target.z));
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, -1, target.z), speed * Time.deltaTime);
                prevLaserHeight = target.y + 2.0f; // 2 is half of wall height
            }
        }
    }

    public void Jump()  // Prevents a double jump if 'time' reaches 'timeWait' at exact moment of jumping
    {
        time = 0;
        timeWait = 65;
        free = false;
        if (Random.Range(0, 2) == 0)
            anim.Play(tailCurlHash);
        if (prevLaserHeight > 2)
        {
            rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            rb.AddForce(Vector3.up * prevLaserHeight * 2, ForceMode.Impulse);
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void Pounce()
    {
        if (free)   // Prevents a double pounce if 'time' reaches 'timeWait' at exact moment of jumping
        {
            time = 0;
            timeWait = 50;
            free = false;
            rb.AddForce(Vector3.up * 2 + transform.forward * 0.2f, ForceMode.Impulse);
            rb.AddTorque(transform.right * 0.05f, ForceMode.Impulse);
        }
    }

    public void SetFree()
    {
        free = true;
    }
}
