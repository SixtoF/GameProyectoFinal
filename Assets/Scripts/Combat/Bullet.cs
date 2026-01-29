using UnityEngine;

/// <summary>
/// Controla el movimiento y colisión de una bala simple.
/// </summary>
public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private Rigidbody2D rb;

    /// <summary>
    /// Configura la velocidad inicial y programa la destrucción automática.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Lanza la bala hacia arriba (dirección local del FirePoint) y destruye tras X segundos.
    /// </summary>
    private void Start()
    {
        rb.velocity = transform.up * speed;
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    /// Si toca un enemigo, lo destruye y se destruye a sí misma.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
