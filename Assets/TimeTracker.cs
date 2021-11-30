using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    public Transform planet;

    private bool running = false;
    public bool SimulationRunning
    {
        get { return this.running; }
        set { this.running = value; }
    }

    float startTime;

    float daysPassed = 0f;

    float timeSinceBeginning;

    private float fullOrbitTimeInSeconds;

    private float dayLengthInSeconds;

    public System.DateTime startingDate = System.DateTime.Now;

    private int dayPassedCheck = 0;

    private void Start()
    {
        startTime = Time.time;

        fullOrbitTimeInSeconds = 360 / planet.GetComponent<OrbitAround>().speed; // seconds

        dayLengthInSeconds = fullOrbitTimeInSeconds / 365; // seconds/day
    }

    void Update()
    {
        if (SimulationRunning)
        {
            timeSinceBeginning += Time.deltaTime;

            daysPassed = timeSinceBeginning / fullOrbitTimeInSeconds * 365;

            if (Mathf.FloorToInt(daysPassed) - dayPassedCheck == 1)
            {
                dayPassedCheck++;
            }

            //Debug.LogWarning("Days passed: " + daysPassed);
            //Debug.Log(startingDate.AddDays(daysPassed).ToString("dd/MM/yyyy"));
        }
    }

    private System.DateTime launchingDate;
    private System.DateTime landingDate;

    public void MarkLaunchDate()
    {
        launchingDate = startingDate.AddDays(daysPassed);

        Debug.Log("Rocket launched: " + launchingDate.ToString("dd/MM/yyyy"));
    }

    public void MarkLandingDate()
    {
        landingDate = startingDate.AddDays(daysPassed);

        Debug.Log("Rocket landed: " + landingDate.ToString("dd/MM/yyyy"));

        System.TimeSpan journeyLength = landingDate - launchingDate;

        Debug.Log("Journey length: " + journeyLength);
    }
}
