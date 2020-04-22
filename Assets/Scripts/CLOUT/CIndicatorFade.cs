using UnityEngine;
using UnityEngine.UI;

public class CIndicatorFade : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().localPosition -= new Vector3(0, 5);
        GetComponent<Text>().color -= new Color(0, 0, 0, 1);

        if (GetComponent<Text>().color.a <= 0) Destroy(gameObject);
    }
}
