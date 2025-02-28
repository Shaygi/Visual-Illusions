using UnityEngine;

public class Floating : MonoBehaviour
{
    [Header("Rotation")]
    public Vector3 rotationSpeed = new Vector3(10f, 20f, 15f);

    [Header("Schwebeeffekt")]
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;

    [Header("Pulsierender Scale-Effekt")]
    public float scaleAmplitude = 0.1f;  
    public float scaleFrequency = 1f;  

    private Vector3 initialPosition;
    private Vector3 initialScale;

    private void Start()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        // Rotation um alle drei Achsen
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);

        // Auf-/Abbewegung
        float newY = initialPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);

        // Pulsierender Scale-Effekt
        float scaleFactor = 1 + Mathf.Sin(Time.time * scaleFrequency) * scaleAmplitude;
        transform.localScale = initialScale * scaleFactor;
    }
}
