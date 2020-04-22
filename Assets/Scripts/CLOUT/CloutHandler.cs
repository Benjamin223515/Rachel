using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CloutHandler
{
    public static int c_Amount = 0;
    [SerializeField]public static CanvasGroup pop;
    public static CloutIndicator indicator;

    static CloutHandler()
    {
        c_Amount = PlayerPrefs.GetInt("Clout");
    }

    public static void alterClout(int amount)
    {
        Debug.LogWarning("CHANGE");
        c_Amount += amount;
        indicator.changeClout(amount);
    }
}
