using UnityEngine;
using TMPro;

/// <summary>
/// Escucha el UnityEvent onHealthChanged del componente Health del Player
/// y actualiza el texto del HUD.
/// </summary>
public class HUDHealthEvent : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    /// <summary>
    /// Cachea el TextMeshPro si el script está en el mismo objeto que el texto.
    /// </summary>
    private void Awake()
    {
        if (healthText == null)
            healthText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Encuentra el Player por Tag y se suscribe al evento de cambio de vida.
    /// </summary>
    private void Start()
    {
        if (playerHealth == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerHealth = playerObj.GetComponent<Health>();
        }

        if (playerHealth == null)
        {
            Debug.LogError("[HUDHealthEvent] No se encontró Health en el Player.");
            return;
        }

        playerHealth.onHealthChanged.AddListener(UpdateText);
        UpdateText(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    /// <summary>
    /// Actualiza el texto del HUD con la vida actual.
    /// </summary>
    private void UpdateText(int current, int max)
    {
        if (healthText == null) return;
        healthText.text = $"HP: {current}/{max}";
    }

    /// <summary>
    /// Limpia la suscripción al destruir el objeto.
    /// </summary>
    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.onHealthChanged.RemoveListener(UpdateText);
    }
}
