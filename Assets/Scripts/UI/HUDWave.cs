using UnityEngine;
using TMPro;

/// <summary>
/// HUD sencillo que muestra la oleada actual.
/// </summary>
public class HUDWave : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;

    /// <summary>
    /// Auto-asigna el texto si el script está en el mismo objeto que el TMP.
    /// </summary>
    private void Awake()
    {
        if (waveText == null)
        {
            waveText = GetComponent<TextMeshProUGUI>();
        }
    }

    /// <summary>
    /// Actualiza el texto del HUD con el número de oleada.
    /// </summary>
    public void SetWave(int waveNumber)
    {
        waveText.text = $"Wave: {waveNumber}";
    }

    /// <summary>
    /// Muestra un estado temporal (por ejemplo: "Next wave in 3...").
    /// </summary>
    public void SetStatus(string message)
    {
        waveText.text = message;
    }
}
