using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Carga escenas por nombre. Se usa desde botones UI.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Carga una escena por su nombre EXACTO (por ejemplo: "03_Game").
    /// </summary>
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Cierra el juego (en el editor no cierra, pero en build sí).
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame() llamado (en Editor no se cierra).");
    }
}
