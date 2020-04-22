using UnityEngine;

public class CameraReplacer : MonoBehaviour
{
    public Material GreenReplacerMaterial;

    void Start()
    {
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponent<Camera>().targetTexture = rt;
        GreenReplacerMaterial.SetTexture("_TexReplacer", rt);
    }
}