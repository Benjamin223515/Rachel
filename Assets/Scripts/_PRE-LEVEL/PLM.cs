using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLM : MonoBehaviour
{
    /// <summary>
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }

    /// </summary>

    public bool beginWalkout = false;
}
