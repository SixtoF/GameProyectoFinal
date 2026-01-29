using UnityEngine;

/// <summary>
/// Controla el comportamiento del enemigo:
/// - Persigue al jugador (por Tag "Player").
/// - Rota para mirar hacia la dirección de movimiento (top-down).
/// - Aplica daño por contacto con un cooldown.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Chase")]
    public float moveSpeed = 2.5f;

    [Header("Rotation")]
    public float rotationOffsetDegrees = -90f;

    [Header("Damage")]
    public int contactDamage = 1;
    public float damageCooldown = 0.5f;

    private Rigidbody2D rb;
    private Transform playerTarget;
    private float nextDamageTime;

    /// <summary>
    /// Se ejecuta al iniciar el objeto.
    /// Guarda la referencia del Rigidbody2D para movimiento/rotación.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Se ejecuta al comenzar la escena.
    /// Busca al jugador por Tag "Player" y guarda su Transform como objetivo.
    /// </summary>
    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTarget = playerObj.transform;
        }
    }

    /// <summary>
    /// Se ejecuta en el bucle de física.
    /// Mueve al enemigo hacia el jugador y rota mirando hacia la dirección de avance.
    /// </summary>
    private void FixedUpdate()
    {
        if (playerTarget == null)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Dirección normalizada hacia el jugador
        Vector2 dir = ((Vector2)playerTarget.position - rb.position).normalized;

        // Movimiento del enemigo
        rb.velocity = dir * moveSpeed;

        // Rotación del enemigo para mirar hacia donde se mueve
        if (dir.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.rotation = angle + rotationOffsetDegrees;
        }
    }

    /// <summary>
    /// Se ejecuta mientras el enemigo está en contacto físico con otro collider.
    /// Si el objeto es el jugador, aplica daño con cooldown para evitar daño constante cada frame.
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (Time.time < nextDamageTime) return;
        nextDamageTime = Time.time + damageCooldown;

        Health playerHealth = collision.gameObject.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
        }
    }
}
