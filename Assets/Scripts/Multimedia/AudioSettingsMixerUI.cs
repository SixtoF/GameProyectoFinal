using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Controla poder ajustar música y efectos
/// </summary>

public class AudioSettingsMixerUI : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string musicParam = "MusicVol";
    [SerializeField] private string sfxParam = "SfxVol";

    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Slider sliderMusic; // 0..1
    [SerializeField] private Slider sliderSfx;   // 0..1

    private const string PrefMusic = "VOL_MUSIC";
    private const string PrefSfx = "VOL_SFX";

    private void Start()
    {
        panel.SetActive(false);

        float music = PlayerPrefs.GetFloat(PrefMusic, 1f);
        float sfx = PlayerPrefs.GetFloat(PrefSfx, 1f);

        sliderMusic.value = music;
        sliderSfx.value = sfx;

        Apply(musicParam, music);
        Apply(sfxParam, sfx);

        sliderMusic.onValueChanged.AddListener(v =>
        {
            PlayerPrefs.SetFloat(PrefMusic, v);
            PlayerPrefs.Save();
            Apply(musicParam, v);
        });

        sliderSfx.onValueChanged.AddListener(v =>
        {
            PlayerPrefs.SetFloat(PrefSfx, v);
            PlayerPrefs.Save();
            Apply(sfxParam, v);
        });
    }

    public void Open() => panel.SetActive(true);
    public void Close() => panel.SetActive(false);

    private void Apply(string param, float value01)
    {
        value01 = Mathf.Clamp(value01, 0.0001f, 1f);
        float db = Mathf.Log10(value01) * 20f;   // 0..1 -> dB
        db = Mathf.Clamp(db, -80f, 0f);
        mixer.SetFloat(param, db);
    }
}