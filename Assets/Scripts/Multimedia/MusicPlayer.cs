using UnityEngine;

/// <summary>
/// Controla la música de fondo del juego.
/// Se mantiene entre escenas.
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    /// <summary>
    /// Evita duplicados y mantiene la música entre escenas.
    /// </summary>
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
