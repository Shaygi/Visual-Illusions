using System.Collections;
using UnityEngine;

public class TooltipFadeIn : MonoBehaviour
{
    public Transform player;               // Referenz zum Spieler-Transform
    public float activationDistance = 5f;    // Distanz, ab der der Tooltip angezeigt werden soll
    public float fadeDuration = 0.5f;        // Dauer des Fades
    private CanvasGroup canvasGroup;
    private bool isVisible = false;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < activationDistance && !isVisible)
        {
            ShowTooltip();
        }
        else if (distance >= activationDistance && isVisible)
        {
            HideTooltip();
        }
    }

    public void ShowTooltip()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f, fadeDuration));
        isVisible = true;
    }

    public void HideTooltip()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f, fadeDuration));
        isVisible = false;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            cg.alpha = Mathf.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cg.alpha = end;
    }
}
