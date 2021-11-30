using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    public Transform target;
    public float speed;

    private bool running = false;
    public bool SimulationRunning
    {
        get { return this.running; }
        set { this.running = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (SimulationRunning)
            transform.RotateAround(transform.position, target.transform.up, speed * Time.deltaTime);
        //transform.RotateAround(target.transform.position, target.transform.up, speed*Time.deltaTime);
    }
}
