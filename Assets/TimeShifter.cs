using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShifter : MonoBehaviour
{
    public float daysFromToday = 0;

    private System.DateTime today = System.DateTime.Now;

    public GameObject sun;

    public void AlignPlanetsByDate(List<GameObject> planets)
    {
        foreach (GameObject planet in planets)
        {
            Vector3 planetOriginPoint = planet.GetComponent<OrbitAround>().planetOriginPoint;

            float percentegeOfYearPassed = daysFromToday / planet.GetComponent<OrbitAround>().orbitTimeInEarthDays;
            percentegeOfYearPassed -= Mathf.FloorToInt(percentegeOfYearPassed);

            float planetAngle = percentegeOfYearPassed * 360; // percentege of full circle

            Quaternion angle = Quaternion.Euler(0, planetAngle, 0);


            planet.transform.position = angle * planetOriginPoint;
        }
    }
}
