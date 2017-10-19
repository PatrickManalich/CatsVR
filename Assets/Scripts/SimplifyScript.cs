using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplifyScript : MonoBehaviour {

    private GameObject[] allGameObjects;
    private string[] allowed;
    
	void Start () {
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        allowed = new string[11];    //change each time
        allowed[0] = "Cat";
        allowed[1] = "Wall";
        allowed[2] = "Base";
        allowed[3] = "MainCamera";
        allowed[4] = "Folder";
        allowed[5] = "Light";
        allowed[6] = "Crosshair";
        //allowed[7] = "Block";
        allowed[8] = "Ball";
        //allowed[9] = "Bed";
        allowed[10] = "Icon";

        foreach (GameObject gameObject in allGameObjects)
        {
            bool found = false;
            for(int i = 0; i < allowed.Length; i++)
            {
                if (gameObject.tag == allowed[i] || gameObject.name == allowed[i])
                    found = true;
            }
            if (!found)
                gameObject.SetActive(false);
            if (gameObject.transform.parent != null && gameObject.transform.parent.tag == "Cat")            
                gameObject.SetActive(true);
            if (gameObject.name == "Icon Controller")
                gameObject.SetActive(true);

        }

        GameObject.Find("Snowball").GetComponent<CatLaserScript>().enabled = false;

    }

    void Update () {}
}
