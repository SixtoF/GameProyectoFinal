using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

/// <summary>
/// Controla el menu de pausa (ESC) y sus botones.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    [Header("Scene Names")]
    public string mainMenuSceneName = "02_MainMenu";

    private bool isPaused;

    /// <summary>
    /// Asegura que el panel arranca oculto.
    /// </summary>
    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        isPaused = false;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Detecta ESC para pausar/continuar.
    /// </summary>
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Alterna entre pausa y juego.
    /// </summary>
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    /// <summary>
    /// Botón: continuar.
    /// </summary>
    public void Resume()
    {
        if (!isPaused) return;
        TogglePause();
    }

    /// <summary>
    /// Botón: volver al menú principal.
    /// </summary>
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    /// <summary>
    /// Botón: salir del juego.
    /// </summary>
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("QuitGame() llamado (en Editor no se cierra).");
    }
}
