using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public sealed class HeatDistortionRenderer : PostProcessEffectRenderer<HeatDistortion>
{
    Shader shader;

    public override void Init()
    {
        shader = Shader.Find("Hidden/Custom/HeatDistortion");
    }

    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(shader);
        sheet.properties.SetFloat("_Amplitude", settings.amplitude);
        sheet.properties.SetFloat("_Frequency", settings.frequency);
        sheet.properties.SetFloat("_Speed", settings.speed);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
