using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
[PostProcess(typeof(HeatDistortionRenderer), PostProcessEvent.BeforeStack, "Hidden/Custom/HeatDistortion")]
public sealed class HeatDistortion : PostProcessEffectSettings
{
    [Range(0f, 0.1f), Tooltip("Amplitude der Verzerrung.")]
    public FloatParameter amplitude = new FloatParameter { value = 0.0f };

    [Range(0f, 100f), Tooltip("Frequenz der Verzerrung.")]
    public FloatParameter frequency = new FloatParameter { value = 0f };

    [Range(0f, 10f), Tooltip("Geschwindigkeit der Verzerrung.")]
    public FloatParameter speed = new FloatParameter { value = 0f };
}
