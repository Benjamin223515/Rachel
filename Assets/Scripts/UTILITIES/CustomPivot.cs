using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPivot : MonoBehaviour
{
    public float Size = 50f;
    public Color Color = Color.yellow;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color;
        Gizmos.DrawWireSphere(transform.position, Size);
    }
}
