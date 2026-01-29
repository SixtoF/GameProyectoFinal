using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controla el movimiento, apuntado y disparo del jugador usando el New Input System.
/// Requisito: todos los métodos comentados.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Audio")]
    public AudioClip shootSfx;
    [Range(0f, 1f)] public float shootVolume = 0.8f;

    private AudioSource audioSource;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 aimScreenPos;

    /// <summary>
    /// Se ejecuta al iniciar el objeto. Guarda la referencia del Rigidbody2D y del AudioSource.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Aplica el movimiento usando física 2D.
    /// </summary>
    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    /// <summary>
    /// Rota el jugador hacia la posición del ratón (PC).
    /// </summary>
    private void Update()
    {
        if (aimScreenPos.sqrMagnitude > 0.1f)
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(aimScreenPos.x, aimScreenPos.y, 0f));
            Vector2 dir = (world - transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f;
        }
    }

    /// <summary>
    /// Recibe el input de movimiento desde el Input System.
    /// Método llamado automáticamente por PlayerInput (Send Messages).
    /// </summary>
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    /// <summary>
    /// Recibe el input de apuntado (posición del ratón).
    /// Método llamado automáticamente por PlayerInput (Send Messages).
    /// </summary>
    public void OnAim(InputValue value)
    {
        aimScreenPos = value.Get<Vector2>();
    }

    /// <summary>
    /// Dispara una bala desde el FirePoint.
    /// Método llamado automáticamente por PlayerInput (Send Messages).
    ///  Dispara una bala desde el FirePoint y reproduce el SFX del disparo.
    /// </summary>
    public void OnFire()
    {
        if (bulletPrefab == null || firePoint == null) return;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        PlayShootSfx();
    }

    /// <summary>
    /// Reproduce el sonido de disparo usando PlayOneShot (no corta sonidos anteriores).
    /// </summary>
    private void PlayShootSfx()
    {
        if (audioSource == null) return;
        if (shootSfx == null) return;

        audioSource.PlayOneShot(shootSfx, shootVolume);
    }

}
