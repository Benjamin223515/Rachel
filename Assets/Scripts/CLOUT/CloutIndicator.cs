using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CloutIndicator : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject indicator;

    int clout = 0;

    void Start()
    {
        CloutHandler.indicator = gameObject.GetComponent<CloutIndicator>();
    }

    private void Update()
    {
        text.text = "<sprite name=\"CloutIcon\">" + clout;
        clout = (int)Mathf.Lerp(clout, CloutHandler.c_Amount, Mathf.Sin(0.75f));
        if (clout > CloutHandler.c_Amount) clout--;
        if (clout < CloutHandler.c_Amount) clout++;
    }

    public void changeClout(int amount)
    {
        GameObject clone = Instantiate(indicator);
        clone.transform.SetParent(transform.parent, false);
        Text cTMP = clone.GetComponent<Text>();
        cTMP.text = amount >= 0 ? "+ " + amount : amount.ToString().Replace("-", "- ");
        cTMP.color = new Color(255, 81, 81, 1);
        clone.GetComponent<RectTransform>().localPosition = new Vector3(726,455);
        StartCoroutine(Fade(clone));
    }

    private IEnumerator Fade(GameObject clone)
    {
        Color finalColor = clone.GetComponent<Text>().color;
        finalColor.a = 0;
        yield return new WaitForSeconds(1f);
        while (clone.GetComponent<Text>().color.a > 0)
        {
            clone.GetComponent<Text>().color = Color.Lerp(clone.GetComponent<Text>().color, finalColor, 3*Time.deltaTime);
            clone.transform.localPosition -= new Vector3(0, 5);
            yield return null;
        }
        Destroy(clone);
    }
}
