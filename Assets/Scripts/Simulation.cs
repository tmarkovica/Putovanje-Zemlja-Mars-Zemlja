using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    private bool running = false;

    [SerializeField]
    public int daysFromToday = 0;

    public GameObject simulationController;

    private System.DateTime today = System.DateTime.Now;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    public void StartButtonClick()
    {
        RunSimulation();
        ChangeButtonText();
    }

    private void RunSimulation()
    {
        this.running = !this.running;
        Debug.LogWarning("Simulation starts at date: " + today.AddDays(daysFromToday).ToString("dd/MM/yyyy"));

        this.simulationController.GetComponent<CelestialBodies>().SimulationRunning(this.running);
    }

    public UnityEngine.UI.Text buttonText;

    private void ChangeButtonText()
    {
        if (this.running)
            this.buttonText.text = "Stop";
        else
            this.buttonText.text = "Start";
    }
}
