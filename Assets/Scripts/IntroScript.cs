using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour {

    private ControllerScript controllerScript;
    private GameObject snowball;
    private CatLaserScript snowballLaserScript;
    private Rigidbody snowballRigidBody;
    private AudioSource snowballAudioSource;
    private Animator snowballAnim;
    private int introHash;
    private GameObject laserPointer;
    private GameObject[] balls;
    private GameObject[] blocks;
    private Rigidbody snowballBedRigidBody;
    private AudioSource mainSongAudioSource;
    private AudioSource introSongAudioSource;
    private Light pointLight;
    private GameObject crosshair;
    private GameObject iconController;
    private GameObject[] icons;
    private GameObject[] quads;

    private GameObject[] pathPointGameObjects;
    private Transform[] pathPointTransforms;
    private int currPathPoint;
    private Transform targetPathPoint;
    private bool pathFinished;
    private bool introSongFinished;

    void Awake () {
        controllerScript = GameObject.Find("Main Camera").GetComponent<ControllerScript>();
        snowball = GameObject.Find("Snowball");
        snowballLaserScript = (CatLaserScript)snowball.GetComponent(typeof(CatLaserScript));
        snowballLaserScript.enabled = false;
        snowballRigidBody = snowball.GetComponent<Rigidbody>();
        snowballRigidBody.isKinematic = true;
        snowballAudioSource = snowball.GetComponent<AudioSource>();
        snowballAnim = snowball.GetComponent<Animator>();
        introHash = Animator.StringToHash("Intro");
        laserPointer = GameObject.Find("LaserPointer");
        laserPointer.SetActive(false);
        balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach(GameObject ball in balls) { ball.SetActive(false); }
        blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks) { block.SetActive(false); }
        snowballBedRigidBody = GameObject.Find("SnowballBed").GetComponent<Rigidbody>();
        snowballBedRigidBody.isKinematic = true;
        mainSongAudioSource = GameObject.Find("Main Song").GetComponent<AudioSource>();
        introSongAudioSource = GameObject.Find("Intro Song").GetComponent<AudioSource>();
        introSongAudioSource.Play();
        pointLight = GameObject.Find("Point Light").GetComponent<Light>();
        pointLight.intensity = 0;
        crosshair = GameObject.Find("Crosshair");
        crosshair.SetActive(false);
        iconController = GameObject.Find("Icon Controller");
        iconController.SetActive(false);
        icons = GameObject.FindGameObjectsWithTag("Icon");
        foreach(GameObject icon in icons) { icon.SetActive(false); }
        quads = GameObject.FindGameObjectsWithTag("Quad");
        foreach (GameObject quad in quads) { quad.SetActive(false); }

        pathPointGameObjects = GameObject.FindGameObjectsWithTag("Path Point");
        pathPointTransforms = new Transform[pathPointGameObjects.Length];
        for (int i = 0; i < pathPointTransforms.Length; i++) {  pathPointTransforms[int.Parse(pathPointGameObjects[i].name.Substring(12, 1))] = pathPointGameObjects[i].transform; }
        currPathPoint = 0;
        targetPathPoint = null;
        pathFinished = false;
        introSongFinished = false;

        snowballAnim.Play(introHash);
    }
	

	void Update () {
        if (!introSongFinished)
        {
            if (introSongAudioSource.time > 31)  //play audio source of cat meowing after 31 seconds into the intro song
            {
                snowballAudioSource.Play();
                introSongFinished = true;
            }
            else if (introSongAudioSource.time > 24)  //start moving in position after 24 seconds into the intro song
            {
                if (!pathFinished)
                    FollowPath();
                else
                    snowball.transform.forward = Vector3.RotateTowards(snowball.transform.forward, Vector3.back, 2.0f * Time.deltaTime, 0.0f);
            }
            else if(introSongAudioSource.time < 4)  //brighten lights during the first 4 seconds of the intro song
            {
                if (pointLight.intensity < 1)
                    pointLight.intensity += 0.01f;
                else
                    pointLight.intensity = 1;
            }
        }
        else
        {
            if (Time.fixedTime > 40)
                Initialize();
        }
        
    }

    void FollowPath()
    {
        if (targetPathPoint == null) //first iteration
            targetPathPoint = pathPointTransforms[currPathPoint];

        snowball.transform.forward = Vector3.RotateTowards(snowball.transform.forward, targetPathPoint.position - snowball.transform.position, 2.0f * Time.deltaTime, 0.0f);
        snowball.transform.position = Vector3.MoveTowards(snowball.transform.position, targetPathPoint.position, 0.5f * Time.deltaTime);

        if (snowball.transform.position == targetPathPoint.position)
        {
            currPathPoint++;
            if (currPathPoint == pathPointTransforms.Length)
                pathFinished = true;
            else
                targetPathPoint = pathPointTransforms[currPathPoint];
        }
    }

    void Initialize()
    {
        snowballLaserScript.enabled = true;
        snowballRigidBody.isKinematic = false;
        laserPointer.SetActive(true);
        foreach (GameObject ball in balls) { ball.SetActive(true); }
        foreach (GameObject block in blocks) { block.SetActive(true); }
        snowballBedRigidBody.isKinematic = false;
        mainSongAudioSource.Play();
        foreach(GameObject pathPoint in pathPointGameObjects) { pathPoint.SetActive(false); }
        controllerScript.enabled = true;
        crosshair.SetActive(true);
        iconController.SetActive(true);
        foreach (GameObject icon in icons) { icon.SetActive(true); }
        foreach (GameObject quad in quads) { quad.SetActive(true); }
        this.gameObject.SetActive(false);
    }
}
