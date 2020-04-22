using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public Material Material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, this.Material);
    }
}