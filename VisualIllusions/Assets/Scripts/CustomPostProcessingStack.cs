using UnityEngine;

public class CustomPostProcessingStack : MonoBehaviour
{
    public Material[] effects;
    public bool effectActive = false; // Wird vom Trigger gesteuert

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Wenn der Effekt nicht aktiv ist, das Originalbild ausgeben
        if (!effectActive)
        {
            Graphics.Blit(source, destination);
            return;
        }

        RenderTexture currentSource = source;
        RenderTexture temp = RenderTexture.GetTemporary(source.width, source.height);

        // Alle Effekte in der definierten Reihenfolge anwenden
        for (int i = 0; i < effects.Length; i++)
        {
            if (i == effects.Length - 1)
            {
                Graphics.Blit(currentSource, destination, effects[i]);
            }
            else
            {
                Graphics.Blit(currentSource, temp, effects[i]);
                currentSource = temp;
            }
        }
        RenderTexture.ReleaseTemporary(temp);
    }
}
