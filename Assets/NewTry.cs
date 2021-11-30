using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTry : MonoBehaviour
{
    public Transform startingPlanet;
    public Transform destinationPlanet;

    public Transform orbiter;

    public float speed;

    //
    private float a; // (Rz+Rm)/2 = 2a

    float timeSinceBeginning = 0;

    private bool rocketlaunched = false;

    private bool rocketLanded = false;

    private bool running = false;

    private int journeyState = 0;
    public bool SimulationRunning
    {
        get { return this.running; }
        set 
        { 
            this.running = value;
        }
    }

    Vector3 marsFinalLocation;

    private void Start()
    {
        //float earthToSunDistance = Vector3.Distance(startingPlanet.position, sun.position);
        //float marsToSunDistance = Vector3.Distance(destinationPlanet.position, sun.position);

        float earthToSunDistance = Vector3.Magnitude(startingPlanet.position);
        float marsToSunDistance = Vector3.Magnitude(destinationPlanet.position);

        a = (earthToSunDistance + marsToSunDistance) / 2;

        this.orbiter.GetComponent<Rigidbody>().isKinematic = true; // ??

        startTime = Time.time;

        orbiter.GetComponent<TrailRenderer>().enabled = false;

    }

    float startTime;
    Vector3 midPoint;

    void Update()
    {
        if (SimulationRunning)
        {
            switch (journeyState)
            {
                case 0: // waiting for angle
                    if (CanLaunch())
                    {
                        InitiateLaunch();
                        destinationPlanet.GetChild(0).GetComponent<LandShip>().collisionAllowed = true;
                    }
                    break;
                case 1: // trajectory to Mars
                    timeSinceBeginning += Time.deltaTime;
                    orbiter.RotateAround(midPoint, Vector3.up, speed * Time.deltaTime);
                    break;
                case 2: // waiting for angle
                    if (CanLaunch())
                    {
                        InitiateLaunchFromMars();
                        startingPlanet.GetChild(0).GetComponent<LandShip>().collisionAllowed = true;
                    }
                    break;
                case 3: // trajectory to Earth
                    timeSinceBeginning += Time.deltaTime;
                    orbiter.RotateAround(midPoint, Vector3.up, speed * Time.deltaTime);
                    break;
                default:
                    break;
            }
        }      
    }

    Vector3 earthWhenLaunched;

    //float magnitude;

    public void InitiateLaunch() 
    {
        if (rocketlaunched)
            return;

        //Debug.LogWarning("RocketLaunched");

        rocketlaunched = true;
        journeyState++;

        a = (Vector3.Magnitude(startingPlanet.position) + Vector3.Magnitude(destinationPlanet.position)) /2; // 2a

        earthWhenLaunched = startingPlanet.position;

        float angle = Mathf.Atan(earthWhenLaunched.z / earthWhenLaunched.x);
        float magnitude = Vector3.Magnitude(earthWhenLaunched);

        float temp = a - magnitude;


        midPoint = earthWhenLaunched;

        midPoint.Normalize();
        midPoint *= (-1) * temp;

        orbiter.position = earthWhenLaunched;

        orbiter.GetComponent<TrailRenderer>().enabled = true;

        startingPlanet.GetComponent<TimeTracker>().MarkLaunchDate();
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

    public bool CanLaunchFromMars()
    {
        if (Vector3.Angle(startingPlanet.position, destinationPlanet.position) > 44)
            return false;

        float earthAngle = Mathf.Atan(this.startingPlanet.position.z / this.startingPlanet.position.x) * Mathf.Rad2Deg;
        float marsAngle = Mathf.Atan(this.destinationPlanet.position.z / this.destinationPlanet.position.x) * Mathf.Rad2Deg;

        if (marsAngle > earthAngle)
        {
            if (marsAngle + earthAngle > 44)
                return true;
        }

        return false;
    }

    public void InitiateLanding()
    {
        rocketLanded = true;
        journeyState++;

        startingPlanet.GetComponent<TimeTracker>().MarkLandingDate();

        orbiter.SetParent(destinationPlanet);

        orbiter.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);//
        orbiter.localPosition = new Vector3(0, 0, 0);
        orbiter.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        orbiter.GetComponent<TrailRenderer>().enabled = false;
    }

    Vector3 marsWhenLaunched;

    public void InitiateLaunchFromMars()
    {
        journeyState++;

        orbiter.transform.parent = null;
        orbiter.transform.localScale = new Vector3(6f, 6f, 6f);//
        orbiter.localPosition = new Vector3(0, 0, 0);
        orbiter.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));


        a = (Vector3.Magnitude(startingPlanet.position) + Vector3.Magnitude(destinationPlanet.position)) / 2; // 2a

        marsWhenLaunched = destinationPlanet.position;

        float angle = Mathf.Atan(marsWhenLaunched.z / marsWhenLaunched.x);
        float magnitude = Vector3.Magnitude(marsWhenLaunched);

        float temp = a - magnitude;


        midPoint = marsWhenLaunched;

        midPoint.Normalize();
        midPoint *= (-1) * temp;

        orbiter.position = marsWhenLaunched;

        orbiter.GetComponent<TrailRenderer>().enabled = true;

        startingPlanet.GetComponent<TimeTracker>().MarkLaunchDate();

        speed = 8.9f;
    }
}
