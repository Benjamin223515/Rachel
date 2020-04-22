using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    [Header("Imports")]
    public DayNightHandler DayNightHandler;

    [Header("Clock Parts")]
    public GameObject hoursHand;
    public GameObject minutesHand;

    private void Update()
    {
        minutesHand.transform.localEulerAngles = new Vector3(-90, 0, (DayNightHandler.minute * 6) - 100);
        hoursHand.transform.localEulerAngles = new Vector3(-90, 0, (DayNightHandler.actualTime * 30) - 10);
    }
}
