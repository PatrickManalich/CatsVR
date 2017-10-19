using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

    void Start()
    {  }

	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.down, 50 * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, 50 * Time.deltaTime);
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Rotate(Vector3.left, 50 * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.Rotate(Vector3.right, 50 * Time.deltaTime);
    }
}
