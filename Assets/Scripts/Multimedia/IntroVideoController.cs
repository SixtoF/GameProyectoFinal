using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

/// <summary>
/// Controla el vídeo de intro. Al terminar, vuelve al menú.
/// </summary>
public class IntroVideoController : MonoBehaviour
{
    [Header("Scene Names")]
    public string mainMenuSceneName = "02_MainMenu";

    private VideoPlayer videoPlayer;

    /// <summary>
    /// Inicializa el VideoPlayer y se suscribe al evento de finalización.
    /// </summary>
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    /// <summary>
    /// Al terminar el vídeo, carga el menú principal.
    /// </summary>
    private void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    /// <summary>
    /// Permite saltar con ESC (opcional).
    /// </summary>
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    /// <summary>
    /// Limpieza de eventos al destruir el objeto.
    /// </summary>
    private void OnDestroy()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
