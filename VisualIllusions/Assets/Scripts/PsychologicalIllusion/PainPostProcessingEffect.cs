using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing; // Für Post Processing Stack v2

public class PainPostProcessingEffect : MonoBehaviour
{
    // Das Post Process Volume, das dein PainProfile enthält
    public PostProcessVolume volume;

    // Dauer des Effekts
    public float effectDuration = 0.5f;

    // Maximale Vignette-Intensity für den Schmerz-Effekt
    public float maxVignetteIntensity = 0.6f;

    // Zielwert für die Sättigung (mehr Rot), negative Werte können ein rötlicheres Aussehen erzeugen
    public float maxRedSaturation = -20f;

    // Referenz auf den Sphere Collider des PainVolume, der standardmäßig deaktiviert ist
    public Collider painTrigger;

    private Vignette vignette;
    private ColorGrading colorGrading;

    private float defaultVignetteIntensity;
    private float defaultSaturation;

    void Start()
    {
        // Erstelle eine Instanz des Profils, damit Änderungen lokal vorgenommen werden
        volume.profile = Instantiate(volume.profile);

        // Hole die Vignette- und ColorGrading-Komponenten aus dem Profil
        volume.profile.TryGetSettings<Vignette>(out vignette);
        volume.profile.TryGetSettings<ColorGrading>(out colorGrading);

        if (vignette != null)
        {
            defaultVignetteIntensity = vignette.intensity.value;
        }
        if (colorGrading != null)
        {
            defaultSaturation = colorGrading.saturation.value;
        }

        // Sicherstellen, dass der painTrigger initial deaktiviert ist
        if (painTrigger != null)
            painTrigger.enabled = false;
    }

    // Wird ausgelöst, wenn der Spieler den PainPostProcessCube betritt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aktiviere den Sphere Collider des PainVolume
            if (painTrigger != null)
                painTrigger.enabled = true;

            ActivatePainEffect();
        }
    }

    // Beim Verlassen den Collider wieder deaktivieren
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (painTrigger != null)
                painTrigger.enabled = false;
        }
    }

    // Startet den Schmerz-Effekt
    public void ActivatePainEffect()
    {
        StopAllCoroutines();
        StartCoroutine(PainEffectCoroutine());
    }

    IEnumerator PainEffectCoroutine()
    {
        float timer = 0f;
        while (timer < effectDuration)
        {
            float t = timer / effectDuration;
            if (vignette != null)
            {
                // Erhöhe die Vignette-Intensity linear
                vignette.intensity.value = Mathf.Lerp(defaultVignetteIntensity, maxVignetteIntensity, t);
            }
            if (colorGrading != null)
            {
                // Verschiebe die Sättigung in Richtung Rot
                colorGrading.saturation.value = Mathf.Lerp(defaultSaturation, maxRedSaturation, t);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        // Kurze Pause, bevor der Effekt zurückgesetzt wird
        yield return new WaitForSeconds(0.2f);
        if (vignette != null)
        {
            vignette.intensity.value = defaultVignetteIntensity;
        }
        if (colorGrading != null)
        {
            colorGrading.saturation.value = defaultSaturation;
        }

        // Deaktiviere den painTrigger wieder
        if (painTrigger != null)
            painTrigger.enabled = false;
    }
}
