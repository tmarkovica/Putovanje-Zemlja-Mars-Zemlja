using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBodies : MonoBehaviour
{
    public GameObject sun;

    public List<GameObject> planets = new List<GameObject>();

    public List<GameObject> moons = new List<GameObject>();

    public GameObject rocket;

    public GameObject tempObj;

    public GameObject test;

    private bool running = false;

    private bool simulationInProgress = false;

    void Update()
    {
        if (simulationInProgress == false)
        {
            GetComponent<TimeShifter>().AlignPlanetsByDate(planets);
        }
    }

    public void SimulationRunning(bool state)
    {
        this.running = state;

        if (state)
            StartSimulation();

        try
        {
            this.rocket.GetComponent<NewTry>().SimulationRunning = this.running;
            this.test.GetComponent<NewTry>().SimulationRunning = this.running;

            this.tempObj.GetComponent<TimeTracker>().SimulationRunning = this.running;

            //this.rocket.GetComponent<OrbitAround_time>().SimulationRunning = this.running;

            foreach (GameObject planet in this.planets) 
            {
                planet.GetComponent<OrbitAround>().SimulationRunning = this.running;
                planet.GetComponent<RotateAroundAxis>().SimulationRunning = this.running;
            }

            foreach (GameObject moon in this.moons)
                moon.GetComponent<OrbitAroundPlanet>().SimulationRunning = this.running;
        }
        catch
        {
            Debug.Log("Exception caught");
        }
    }


    private void StartSimulation()
    {
        simulationInProgress = true;
    }
}
