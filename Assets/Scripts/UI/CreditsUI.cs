using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    [Header("References")]
    public GameObject controlsPanel;
    public GameObject creditsGroup;

    private void Start()
    {
        if (controlsPanel != null) controlsPanel.SetActive(false);
        if (creditsGroup != null) creditsGroup.SetActive(true);
    }

    public void ShowControls()
    {
        if (controlsPanel != null) controlsPanel.SetActive(true);
        if (creditsGroup != null) creditsGroup.SetActive(false);
    }

    public void HideControls()
    {
        if (controlsPanel != null) controlsPanel.SetActive(false);
        if (creditsGroup != null) creditsGroup.SetActive(true);
    }
}


/*using UnityEngine;

/// <summary>
/// Controla la UI de créditos: abrir/cerrar panel de controles.
/// </summary>
public class CreditsUI : MonoBehaviour
{
    public GameObject controlsPanel;

    /// <summary>
    /// Muestra el panel de controles.
    /// </summary>
    public void ShowControls()
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(true);
    }

    /// <summary>
    /// Oculta el panel de controles.
    /// </summary>
    public void HideControls()
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }
}*/
