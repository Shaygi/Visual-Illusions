using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Game UI")]
    [Tooltip("Das Panel, das die Spiel-UI enthält.")]
    public GameObject gameUI;

    [Header("Pause Menü UI")]
    [Tooltip("Das Panel, das das Pause-Menü enthält.")]
    public GameObject pauseMenuUI;

    [Header("Szenen Einstellungen")]
    [Tooltip("Der Name der Menü-Szene, wie in den Build Settings eingetragen.")]
    public string menuSceneName = "Menu";

    private bool isPaused = false;

    void Update()
    {
        // Drücke Escape, um zwischen Pausieren und Fortsetzen zu wechseln
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        gameUI.SetActive(false);
        pauseMenuUI.SetActive(true);  // Zeige das Pause-Menü
        Time.timeScale = 0f;          // Pausiere das Spiel
        isPaused = true;
        // Cursor freischalten, damit der Spieler die Buttons bedienen kann
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        gameUI.SetActive(true);
        pauseMenuUI.SetActive(false); // Verberge das Pause-Menü
        Time.timeScale = 1f;          // Setze das Spiel fort
        isPaused = false;
        // Sperre den Cursor wieder, damit die First-Person-Steuerung funktioniert
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Diese Methode wird vom Menu-Button aufgerufen
    public void LoadMenu()
    {
        Time.timeScale = 1f;  // Stelle sicher, dass das Spiel fortgesetzt wird
        SceneManager.LoadScene(menuSceneName);
    }

    // Diese Methode wird vom Quit-Button aufgerufen
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        // Falls im Editor gespielt wird, beende auch den Play-Modus
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
