using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public enum Zone
{
    Top,
    Bottom
}

public class HoopSensor : MonoBehaviour
{
    public Zone zone;
    static bool ballPassedTop;

    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();

        if (ball != null)
        {
            if (zone == Zone.Top)
            {
                ballPassedTop = true;
            }
        }
        else if (zone == Zone.Bottom)
        {
            ballPassedTop = false; //Reset
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(zone == Zone.Bottom)
        {
            Ball ball = other.GetComponent<Ball>();

            if (ball != null && ballPassedTop)
            {
                // Ball has passed through the hoop
                Debug.Log("Ball passed through the hoop!");
            }
        }
    }
}
