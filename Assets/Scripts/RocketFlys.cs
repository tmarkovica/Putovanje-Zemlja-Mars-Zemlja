using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFlys : MonoBehaviour
{
    public Transform rocket;
    public Rigidbody rb;
    public float speed = 10.0f;

    public float orbitSpeed;

    public AudioSource audioSource;

    public GameObject engine;

    public Transform startingPlanet;

    public Transform destinationPlanet;

    public Transform sun;

    public float timeSinceBeginning = 0;

    private float startTime;

    private float journeyLength;

    private bool running = false;
    public bool SimulationRunning
    {
        get { return this.running; }
        set 
        { 
            this.running = value;
            this.engine.SetActive(value);    
        }
    }

    private bool rocketLaunched = false;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SimulationRunning)
        {
            if (Input.GetKey("c"))
                StartRocket();

            if (rocketLaunched)
                FlyRocket();


            /*if (this.audioSource.isPlaying == false)
                this.audioSource.Play();*/
        }
        else
            this.audioSource.Stop();
    }

    private float CalculateDistanceToSun(Transform planet)
    {
        float distance = Vector3.Distance(planet.position, this.sun.position);

        Debug.Log("distance = " + distance);

        return distance;
    }

    public void StartRocket()
    {
        if (CanLaunch() == false) return;

        //lastFwd = transform.forward;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startingPlanet.position, destinationPlanet.position);

        Debug.Log("journeyLength = " + journeyLength);
        this.gameObject.transform.position = startingPlanet.transform.position;

        this.rocketLaunched = true;
    }

    private void FlyRocket()
    {
        timeSinceBeginning += Time.deltaTime;

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        this.gameObject.transform.position = Vector3.Lerp(startingPlanet.position, destinationPlanet.position, fractionOfJourney);
    }

    public bool CanLaunch()
    {
        if (Vector3.Angle(startingPlanet.position, destinationPlanet.position) > 44)
            return false;

        float earthAngle = Mathf.Atan(this.startingPlanet.position.z / this.startingPlanet.position.x) * Mathf.Rad2Deg;
        float marsAngle = Mathf.Atan(this.destinationPlanet.position.z / this.destinationPlanet.position.x) * Mathf.Rad2Deg;

        if (marsAngle > earthAngle)
        {
            if (marsAngle + earthAngle > 44)
                return false;
        }

        return true;
    }
}