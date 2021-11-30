using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAroundPlanet : MonoBehaviour
{
    public Transform moon;
    public Transform centerBody;
    public float speed; // speed of the orbiting

    private bool running = false;
    public bool SimulationRunning
    {
        get { return this.running; }
        set { this.running = value; }
    }

    private Vector3 startingPosition;
    private void Start()
    {
        this.startingPosition = this.moon.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (SimulationRunning)
        {
            moon.RotateAround(centerBody.position, transform.up, speed * Time.deltaTime);
            Vector3 delta = moon.position - centerBody.position;
            delta.y = 0; // Keep same Y level
            moon.position = centerBody.position + delta.normalized * this.startingPosition.x;
        }
    }
}
