using UnityEngine;

public class VignetteTrigger : MonoBehaviour
{
    public RoomEffectManager effectManager;
    public float vignetteIntensity = 0.5f;
    public float chromaticIntensity = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            effectManager.SetRoomEffect(vignetteIntensity, chromaticIntensity);
        }
    }
}
