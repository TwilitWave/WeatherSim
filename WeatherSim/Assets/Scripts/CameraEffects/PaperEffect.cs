using UnityEngine;

[ExecuteInEditMode]
public class PaperEffect : MonoBehaviour
{
    public Material PaperEffectMat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, PaperEffectMat);
    }
}
