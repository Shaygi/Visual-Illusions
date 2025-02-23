using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RoomEffectManager : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        // Die entsprechenden Einstellungen aus dem Volume-Profile bekommen
        volume.profile.TryGetSettings(out vignette);
        volume.profile.TryGetSettings(out chromaticAberration);
    }

    public void SetRoomEffect(float vignetteIntensity, float chromaticIntensity)
    {
        if (vignette != null) vignette.intensity.value = vignetteIntensity;
        if (chromaticAberration != null) chromaticAberration.intensity.value = chromaticIntensity;
    }
}
