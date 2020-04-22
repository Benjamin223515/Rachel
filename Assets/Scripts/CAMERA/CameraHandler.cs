using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraHandler : MonoBehaviour
{
    Camera cam;

    public DayNightHandler DNH;
    public GameObject Target;

    public GameObject d_T;

    [Header("Magnifications")]
    public float m_FitToScreen = 17.4f;
    public float m_ZoomToIndividual = 5f;

    [Header("Target Positions")]
    public Vector3 p_Default = new Vector3();
    // VALUES

    public ScreenStates v_FitToScreen = ScreenStates.FIT;
    private bool v_TransitionInProgress = false;

    public enum ScreenStates { FIT, INDIVIDUAL }

    //

    void Start()
    {
        cam = GetComponent<Camera>();
        Target.transform.localPosition = p_Default;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) FitToScreen();
        if (Input.GetKeyDown(KeyCode.Z)) ZoomToIndividual(d_T);
    }

    public void FitToScreen()
    {
        if(!v_TransitionInProgress)
        {
            v_TransitionInProgress = true;
            Target.transform.parent = null;
            StartCoroutine(transition(ScreenStates.FIT));
        }

    }

    public void ZoomToIndividual(GameObject target)
    {
        if(!v_TransitionInProgress)
        {
            v_TransitionInProgress = true;
            Target.transform.parent = target.transform;
            StartCoroutine(transition(ScreenStates.INDIVIDUAL));
        }
        
    }

    private IEnumerator transition(ScreenStates state)
    {
        if (state == v_FitToScreen)
        {
            v_TransitionInProgress = false;
            yield return null;
        }
        else
        {
            switch (state)
            {
                case ScreenStates.FIT:
                    v_FitToScreen = ScreenStates.FIT;
                    for (float i = 0; (double)cam.orthographicSize != (double)m_FitToScreen; i += (Time.deltaTime * DNH.timeScale))
                    {
                        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, m_FitToScreen, i);
                        Target.transform.localPosition = Vector3.Lerp(Target.transform.localPosition, p_Default, i);
                        yield return null;
                    }
                    v_TransitionInProgress = false;
                    break;

                case ScreenStates.INDIVIDUAL:
                    v_FitToScreen = ScreenStates.INDIVIDUAL;
                    for (float i = 0; (double)cam.orthographicSize != (double)m_ZoomToIndividual; i += (Time.deltaTime * DNH.timeScale))
                    {
                        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, m_ZoomToIndividual, i);
                        Target.transform.localPosition = Vector3.Lerp(Target.transform.localPosition, new Vector3(0, 0, 0), i);
                        yield return null;
                    }
                    v_TransitionInProgress = false;
                    break;
            }
        }
        
    }
}
