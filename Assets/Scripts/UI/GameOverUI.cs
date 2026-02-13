using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI waveResultText; // opcional

    [Header("Refs")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private WaveManager waveManager; // opcional
    [SerializeField] private string gameSceneName = "03_Game";

    private bool isGameOver;

    private void Awake()
    {
        isGameOver = false;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Start()
    {
        // Encontrar Player por Tag (igual que HUD)
        if (playerHealth == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerHealth = playerObj.GetComponent<Health>();
        }

        // Encontrar WaveManager mostrar oleada
        if (waveManager == null)
            waveManager = FindFirstObjectByType<WaveManager>();

        if (playerHealth == null)
        {
            Debug.LogError("[GameOverUI] No se encontró Health en el Player.");
            return;
        }

        // Suscribirse al evento de muerte
        playerHealth.onDied.AddListener(ShowGameOver);
    }

    private void Update()
    {
        if (!isGameOver) return;

        // ENTER o CLICK para reintentar
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
            Retry();

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            Retry();
    }

    private void ShowGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Mostrar oleada alcanzada
        if (waveResultText != null && waveManager != null)
            waveResultText.text = $"Oleada alcanzada: {waveManager.CurrentWave}";

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.onDied.RemoveListener(ShowGameOver);
    }
}
