using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Placed on the game object 'Laser', which contains the laser dot sprite. Raycasts from the
 * 'mainCamera' and computes the new position. Also tells the 'catLaserScript' to either
 * follow the laser or pounce on the laser.
 */

public class LaserScript : MonoBehaviour {

    private GameObject mainCamera;
    private GameObject[] cats;
    private GameObject currCat;
    private CatLaserScript catLaserScript;

    void Start () {

        mainCamera = GameObject.Find("Main Camera");
        cats = GameObject.FindGameObjectsWithTag("Cat");

        foreach (GameObject cat in cats)
        {
            currCat = cat;
            catLaserScript = (CatLaserScript)currCat.GetComponent(typeof(CatLaserScript));
        }
    }
    
	void Update () {
        RaycastHit hit;

        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            if (hit.collider)
            {
                if (hit.normal == Vector3.right)
                    transform.position = new Vector3(hit.point.x + 0.001f, hit.point.y, hit.point.z);
                else if (hit.normal == Vector3.left)
                    transform.position = new Vector3(hit.point.x - 0.001f, hit.point.y, hit.point.z);
                else if (hit.normal == Vector3.up)
                    transform.position = new Vector3(hit.point.x, hit.point.y + 0.001f, hit.point.z);
                else if (hit.normal == Vector3.down)
                    transform.position = new Vector3(hit.point.x, hit.point.y - 0.001f, hit.point.z);
                else if (hit.normal == Vector3.forward)
                    transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.001f);
                else if (hit.normal == Vector3.back)
                    transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.001f);
                else
                    transform.position = hit.point;
                transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);

                if (hit.collider.gameObject.tag == "Cat")
                    catLaserScript.Pounce();
                else
                    catLaserScript.Follow(transform.position, hit.normal);
            }
        }
    }
}
