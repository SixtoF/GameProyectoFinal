using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Componente de vida genérico reutilizable (Player, Enemy, etc.).
/// Permite recibir daño, curar y lanzar eventos al cambiar vida o morir.
/// </summary>
public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 5;

    [Header("Events")]
    public UnityEvent<int, int> onHealthChanged; // (current, max)
    public UnityEvent onDied;

    private int currentHealth;

    /// <summary>
    /// Devuelve la vida actual.
    /// </summary>
    public int CurrentHealth => currentHealth;

    /// <summary>
    /// Devuelve la vida máxima.
    /// </summary>
    public int MaxHealth => maxHealth;

    /// <summary>
    /// Inicializa la vida al máximo al arrancar.
    /// </summary>
    private void Awake()
    {
        currentHealth = maxHealth;
        NotifyHealthChanged();
    }

    /// <summary>
    /// Aplica daño. Si llega a 0, ejecuta la muerte.
    /// </summary>
    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        NotifyHealthChanged();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Cura vida hasta el máximo.
    /// </summary>
    public void Heal(int amount)
    {
        if (amount <= 0) return;
        if (currentHealth <= 0) return;

        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        NotifyHealthChanged();
    }

    /// <summary>
    /// Lanza el evento de cambio de vida para actualizar UI/HUD.
    /// </summary>
    private void NotifyHealthChanged()
    {
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    /// <summary>
    /// Lógica de muerte: lanza evento y destruye el objeto (por defecto).
    /// </summary>
    private void Die()
    {
        onDied?.Invoke();
        Destroy(gameObject);
    }
}

