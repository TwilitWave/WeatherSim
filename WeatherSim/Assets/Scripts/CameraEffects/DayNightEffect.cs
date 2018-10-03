using UnityEngine;

[ExecuteInEditMode]
public class DayNightEffect : MonoBehaviour
{
    private Material DayNightMaterial;

    public Color DayColor;
    public Color NightColor;
    [Range(0, 1)]
    public float SunPosition;

    private void Awake()
    {
        DayNightMaterial = new Material(Shader.Find("Hidden/DayNightEffect"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, DayNightMaterial);
    }

    private void Update()
    {
        DayNightMaterial.SetFloat("_SunPosition", SunPosition);
        DayNightMaterial.SetColor("_DayColor", DayColor);
        DayNightMaterial.SetColor("_NightColor", NightColor);
    }
}
