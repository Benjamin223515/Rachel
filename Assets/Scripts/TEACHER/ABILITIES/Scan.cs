using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Scan : MonoBehaviour
{
    private bool Active = false;

    [System.Serializable]
    public class HitEvent : UnityEvent {}
    [SerializeField]
    private HitEvent hitEvent = new HitEvent();

    public DayNightHandler DNH;
    public Material M;

    public HitEvent onHit
    {
        get { return hitEvent; }
        set { hitEvent = value; }
    }

    void Start()
    {
        M.color = new Color(M.color.r, M.color.g, M.color.b, 0);
        GetComponent<MeshCollider>().enabled = false;
    }

    public IEnumerator scan(int Amount, int Width, float speed, bool startLeft)
    {
        Debug.Log("scan");
        Vector3 start = new Vector3(), destination = new Vector3(), curRot = new Vector3();

        if(startLeft)
        {
            start = new Vector3(-90,0,-90);
            destination = new Vector3(-90,0,-180);
        }
        else
        {
            start = new Vector3(-90, 0, -180);
            destination = new Vector3(-90, 0, -90);
        }

        transform.localEulerAngles = start;
        curRot = start;

        if (!Active)
        {
            GetComponent<MeshCollider>().enabled = true;
            for (float t = 0; Mathf.Clamp(t, 0, 1) != 1; t += (Time.deltaTime * (DNH.timeScale)))
            {
                M.color = new Color(M.color.r, M.color.g, M.color.b, Mathf.Lerp(M.color.a, 0.53725490196f, Mathf.Clamp(t,0,1)));
                yield return null;
            }
            Active = true;
            for(int i = 0; i < Amount; i++)
            {
                hit = false;
                for (float t = 0; (int)curRot.z != (int)destination.z; t += (Time.deltaTime * (speed * DNH.timeScale)))
                {
                    curRot = new Vector3(-90, 0, Mathf.Lerp(curRot.z, destination.z, Mathf.Clamp(t, 0, 1)));
                    transform.localEulerAngles = curRot;
                    yield return null;
                }
                hit = false;
                yield return new WaitForSeconds(1f);
                for (float t = 0; (int)curRot.z != (int)start.z; t += (Time.deltaTime * (speed * DNH.timeScale)))
                {
                    curRot = new Vector3(-90, 0, Mathf.Lerp(curRot.z, start.z, Mathf.Clamp(t, 0, 1)));
                    transform.localEulerAngles = curRot;
                    yield return null;
                }
                yield return new WaitForSeconds(1f);
            }
            Active = false;
            for (float t = 1; Mathf.Clamp(t, 0, 1) != 0; t -= (Time.deltaTime * (DNH.timeScale)))
            {
                M.color = new Color(M.color.r, M.color.g, M.color.b, Mathf.Lerp(M.color.a, 0f, Mathf.Clamp(t, 0, 1)));
                yield return null;
            }

            GetComponent<MeshCollider>().enabled = false;
        }
        else yield return null;
    }

    bool hit = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("STUDENT") && !hit)
        {
            hit = true;
            onHit.Invoke();
        }
    }

    public void _Debug()
    {
        Debug.Log("HIT");
    }
}