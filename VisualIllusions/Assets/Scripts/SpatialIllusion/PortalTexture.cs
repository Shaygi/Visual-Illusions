using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PortalTexture : MonoBehaviour
{
    // Kameras, deren Ansichten als Textur verwendet werden
    public Camera cameraA;
    public Camera cameraB;

    // Materialien, auf denen die gerenderten Texturen angezeigt werden
    public Material materialA;
    public Material materialB;

    void Start()
    {
        // �berpr�fe, ob cameraA bereits eine Zieltextur zugewiesen hat
        if (cameraA.targetTexture != null)
        {
            // Falls ja, gib diese Textur frei, um Speicher freizugeben
            cameraA.targetTexture.Release();
        }
        // Erstelle eine neue RenderTexture f�r cameraA mit der Bildschirmaufl�sung und einer Tiefenpuffergr��e von 24
        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // Weise die RenderTexture als Haupttextur des Materials materialA zu
        materialA.mainTexture = cameraA.targetTexture;

        // �berpr�fe, ob cameraB bereits eine Zieltextur zugewiesen hat
        if (cameraB.targetTexture != null)
        {
            // Falls ja, gib diese Textur frei, um Speicher freizugeben
            cameraB.targetTexture.Release();
        }
        // Erstelle eine neue RenderTexture f�r cameraB mit der Bildschirmaufl�sung und einer Tiefenpuffergr��e von 24
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // Weise die RenderTexture als Haupttextur des Materials materialB zu
        materialB.mainTexture = cameraB.targetTexture;
    }
}
