using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Student : System.Object
{
    [Header("Student")]
    public GameObject s_Object;
    [Header("Workspace")]
    public Vector3 w_Direction;
    public Vector3 w_Position;
    [Header("Chair")]
    public GameObject c_Object;
    public GameObject c_Sit;
    [Header("Navigation")]
    public NavMeshAgent agent;
}

public class WalkOut : MonoBehaviour
{
    [Header("VARIABLES")]
    public DayNightHandler DNH;
    [Header("Teacher")]
    public GameObject t_Object;
    public GameObject t_Chair;
    public GameObject t_ChairSit;
    [Header("Students")]
    public List<Student> Students = new List<Student>();
    [Header("Door")]
    public GameObject d_Object;

    [Space(10)]
    public PLM plm;
    public AbilityHitDetector AHD;
    public Challenge c;
    public playerMovement pm;

    [Header("POPUPS")]
    public CanvasGroup ScanPopup;
    public CanvasGroup popup;

    public bool force = false;

    public AudioClip Door, Ambiance, Typing;

    void Start()
    {

        popup.alpha = 0;
        popup.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(1141,291);
        ScanPopup.alpha = 0;
        plm = FindObjectOfType<PLM>();
        d_Object.transform.localEulerAngles= new Vector3(0, -90, -90);
        foreach(Student s in Students)
        {
            s.agent = s.s_Object.GetComponent<NavMeshAgent>();
            s.c_Sit = s.c_Object.transform.GetChild(s.c_Object.transform.childCount - 1).gameObject;
        }
        StartCoroutine(Walkout());

        if (!plm) SceneManager.LoadScene(0);
        else plm.beginWalkout = true;
    }

    private void Update()
    {
        foreach (Student s in Students)
        {
            s.agent.speed = 3.5f * DNH.timeScale;
        }
    }

    IEnumerator Walkout()
    {
        if(!force)
        {
            yield return new WaitForSeconds(1f);
            while(!plm.beginWalkout) yield return null;

            SoundManager.AddSound(Ambiance);
            SoundManager.ChangeVolume(Ambiance, 0);
            SoundManager.AddSound(Typing, false);
            SoundManager.ChangeVolume(Typing, 0);
            for (float f = 0; Mathf.Clamp(f, 0, 1) != 1; f += (Time.deltaTime * DNH.timeScale)) SoundManager.ChangeVolume(Ambiance, Mathf.Lerp(0, 0.032f, f));


            Vector3 curRot = new Vector3(0, -90, -90);

            for(float i = 0; (int)curRot.y != -200; i += (Time.deltaTime * DNH.timeScale))
            {
                float l = Mathf.Lerp(90, 200, Mathf.Clamp(i, 0, 1));
                curRot = new Vector3(0, l-l-l, -90);
                d_Object.transform.localEulerAngles = curRot;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            foreach(Student s in Students)
            {
                s.agent.SetDestination(s.c_Sit.transform.position);
                yield return new WaitForSeconds(0.2f);
            }
            showControls();
            int i2 = 0;
            while(i2 != Students.Count)
            {
                foreach(Student s in Students)
                {
                    if(s.s_Object.transform.parent != s.c_Sit.transform && Vector3.Distance(s.s_Object.transform.position, s.c_Sit.transform.position) <= 3)
                    {
                        if(s == Students[0]) Students[0].agent.ResetPath();
                        i2++;
                        s.s_Object.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                        s.agent.enabled = false;
                        s.s_Object.transform.parent = s.c_Sit.transform;
                        s.s_Object.transform.localPosition = new Vector3(0, 0, 0);
                        s.s_Object.transform.localEulerAngles = Vector3.forward;
                    }
                    yield return null;
                }
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            t_Object.GetComponent<NavMeshAgent>().SetDestination(new Vector3(-7, 7, 0));

            for (float f = 1; Mathf.Clamp(f, 0, 1) != 0; f -= (Time.deltaTime * DNH.timeScale)) SoundManager.ChangeVolume(Ambiance, Mathf.Lerp(0, 0.032f, f));
            bool close = false;
            while(!close)
            {
                if (Vector3.Distance(t_Object.transform.position, t_Object.GetComponent<NavMeshAgent>().destination) <= 0.5f) close = true;
                yield return null;
            }
            for (float i = 1; (int)curRot.y != -90; i -= (Time.deltaTime * DNH.timeScale))
            {
                float l = Mathf.Lerp(90, 200, Mathf.Clamp(i, 0, 1));
                curRot = new Vector3(0, l - l - l, -90);
                d_Object.transform.localEulerAngles = curRot;
                yield return null;
            }
            SoundManager.AddSound(Door);
            SoundManager.ChangeVolume(Door, 0.1f);
            // PLAY PARTICLE
            yield return new WaitForSeconds(3f);
            t_Object.GetComponent<NavMeshAgent>().SetDestination(t_ChairSit.transform.position);
            while(t_Object.transform.parent != t_ChairSit.transform)
            {
                if(Vector3.Distance(t_Object.transform.position, t_Object.GetComponent<NavMeshAgent>().destination) <= 3)
                {
                    t_Object.GetComponent<NavMeshAgent>().enabled = false;
                    t_Object.transform.parent = t_ChairSit.transform;
                    t_Object.transform.localPosition = new Vector3();
                    t_Object.transform.localEulerAngles = Vector3.forward;
                }
                yield return null;
            }

            for (float f = 0; Mathf.Clamp(f, 0, 1) != 1; f += (Time.deltaTime * DNH.timeScale)) SoundManager.ChangeVolume(Typing, Mathf.Lerp(0, 0.02f, f));
        }
        else
        {
            foreach (Student s in Students)
            {
                s.s_Object.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                s.agent.enabled = false;
                s.s_Object.transform.parent = s.c_Sit.transform;
                s.s_Object.transform.localPosition = new Vector3(0, 0, 0);
                s.s_Object.transform.localEulerAngles = Vector3.forward;
            }
            t_Object.GetComponent<NavMeshAgent>().enabled = false;
            t_Object.transform.parent = t_ChairSit.transform;
            t_Object.transform.localPosition = new Vector3();
            t_Object.transform.localEulerAngles = Vector3.forward;
        }

        StartCoroutine(Tutorial());
    }

    private void showControls()
    {
        StartCoroutine(sControls());
    }

    IEnumerator sControls()
    {
        yield return null;
        for (float f = 0; Mathf.Clamp(f, 0, 1) != 1; f += (Time.deltaTime * 2))
        {
            popup.alpha = Mathf.Clamp(f, 0, 1);
            popup.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Lerp(popup.gameObject.GetComponent<RectTransform>().localPosition.x, 780, Mathf.Clamp(f, 0, 1)), 291);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        for (float f = 1; Mathf.Clamp(f, 0, 1) != 0f; f -= (Time.deltaTime * 2))
        {
            popup.alpha = Mathf.Clamp(f, 0, 1);
            popup.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Lerp(popup.gameObject.GetComponent<RectTransform>().localPosition.x, 1141, Mathf.Clamp(f, 0, 1)), 291);
            yield return null;
        }
        popup.alpha = 0;
    }

    IEnumerator Tutorial()
    {

        StartCoroutine(t_Object.transform.GetChild(0).gameObject.GetComponent<Scan>().scan(1,1,0.1f, true));
        yield return new WaitForSeconds(1f);

        for(float f = 0; Mathf.Clamp(f,0,1) != 1; f += (Time.deltaTime * DNH.timeScale))
        {
            ScanPopup.alpha = Mathf.Clamp(f, 0, 1);
            yield return null;
        }
        yield return new WaitForSeconds(10f);
        for (float f = 1; Mathf.Clamp(f, 0, 1) != 0; f -= (Time.deltaTime * DNH.timeScale))
        {
            ScanPopup.alpha = Mathf.Clamp(f, 0, 1);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        ScanPopup.alpha = 0;
        AHD.addChallenge(c);
        pm.canWalk = true;
    }
}
