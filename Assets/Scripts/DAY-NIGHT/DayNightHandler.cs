using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightHandler : MonoBehaviour
{
    [Header("TIME CYCLE")]
    public cycleStages cycleStage;
    public enum cycleStages { NIGHT, TRANSITION_DAY, DAY, TRANSITION_NIGHT };
    [Range(0, 23)] public int hour = 9;
    [Range(0, 59)] public int minute = 0;

    [Header("TIME VARIABLES")]
    public float AMRot = 80;
    public float PMRot = 15;
    public float actualTime = 0f;
    [Range(0,10)]public int timeScale = 1;

    [Header("LIGHTS")]
    public List<Light> lights = new List<Light>();
    public Light DirLight;
    public bool lightsOn = false;


    void Start()
    {
        cycleStage = cycleStages.DAY;
        actualTime = (float) hour + ((float) minute / 60);
        StartCoroutine(TimeHandler());
    }

    void Update()
    {
        actualTime = (float)hour + ((float)minute / 60);

        if (actualTime >= 8 && actualTime < 15) cycleStage = cycleStages.DAY;
        else if (actualTime >= 5 && actualTime < 8) cycleStage = cycleStages.TRANSITION_DAY;
        else if (actualTime >= 15 && actualTime < 18) cycleStage = cycleStages.TRANSITION_NIGHT;
        else cycleStage = cycleStages.NIGHT;
        
        switch(cycleStage)
        {
            case cycleStages.NIGHT:
                DirLight.intensity = 0;
                break;

            case cycleStages.TRANSITION_DAY:
                DirLight.intensity = 1 - ((7.983f - actualTime) / 2.983f);
                if (actualTime >= 6.5f && lightsOn) StartCoroutine(LightsOff());
                break;

            case cycleStages.TRANSITION_NIGHT:
                DirLight.intensity = (17.983f - actualTime) / 2.983f;
                if (actualTime >= 15.5f && !lightsOn) StartCoroutine(LightsOn());
                break;

            case cycleStages.DAY:
                DirLight.intensity = 1;
                if (actualTime < 11.5f) DirLight.transform.localEulerAngles = new Vector3(Mathf.Lerp(7,25, 1f-((11.5f - actualTime)/3.5f)), 90, 0);
                else DirLight.transform.localEulerAngles = new Vector3(Mathf.Lerp(7, 25, (14 - actualTime) / 3.5f), 90, 0);
                break;
        }
    }

    IEnumerator TimeHandler()
    {
        while (true)
        {
            if(timeScale != 0) minute++;
            if (hour == 23 && minute == 60)
            {
                hour = 0;
                minute = 0;
            }
            if (minute == 60)
            {
                minute = 0;
                hour++;
            }
            if(timeScale != 0) yield return new WaitForSeconds(0.5f / (float) timeScale);
            else yield return null;
        }
    }

    IEnumerator LightsOn()
    {
        lightsOn = true;
        for(int i = 0; i < (lights.Count / 2); i++)
        {
            lights[i].enabled = true;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 3; i < lights.Count; i++)
        {
            lights[i].enabled = true;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator LightsOff()
    {
        lightsOn = false;
        for (int i = 0; i < 3; i++)
        {
            lights[i].enabled = false;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 3; i < lights.Count; i++)
        {
            lights[i].enabled = false;
            yield return new WaitForSeconds(0.01f);
        }
    }
}