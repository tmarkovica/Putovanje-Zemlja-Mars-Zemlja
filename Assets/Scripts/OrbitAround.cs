using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    public Transform centerBody;

    public int orbitTimeInEarthDays;
    
    [System.NonSerialized]
    public float speed; // speed of the orbiting

    private const float marsOrbitTimeInSimulation = 60f; // seconds
    private const float marsOrbitTimeInEarthDays = 687f; // days

    [System.NonSerialized]
    public Vector3 planetOriginPoint;

    private bool running = false;
    public bool SimulationRunning
    {
        get { return this.running; }
        set { this.running = value; }
    }

    void Start()
    {
        float orbitTimeInSeconds = marsOrbitTimeInSimulation / (marsOrbitTimeInEarthDays / orbitTimeInEarthDays);

        this.speed = 360 / orbitTimeInSeconds;

        this.planetOriginPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (SimulationRunning)
        {
            transform.RotateAround(centerBody.transform.position, Vector3.up, speed * Time.deltaTime); // works when orbiting stationary object
        }
    }
}
