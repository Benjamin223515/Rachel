using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preserve : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
