using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla la música de fondo del juego.
/// Se mantiene entre escenas.
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    private AudioSource audioSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip menuAndCreditsMusic; // 02_MainMenu + 04_Credits
    [SerializeField] private AudioClip gameMusic;           // 03_Game

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        // Escuchar cambios de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "03_Game")
        {
            PlayClip(gameMusic);
        }
        else if (scene.name == "02_MainMenu" || scene.name == "04_Credits")
        {
            PlayClip(menuAndCreditsMusic);
        }
    }

    private void PlayClip(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;

        if (audioSource.clip == clip) return; // ya está sonando este clip
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}



/*using UnityEngine;

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
}*/
