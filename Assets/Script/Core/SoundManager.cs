using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Source")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM List")]
    public AudioClip[] bgm;

    [Header("SFX List")]
    public AudioClip[] sfx;

    [Header("Volume Slider")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Load volume sebelumnya
        float savedBGM = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);

        bgmSource.volume = savedBGM;
        sfxSource.volume = savedSFX;

        // Set slider value
        if (bgmSlider != null)
            bgmSlider.value = savedBGM;

        if (sfxSlider != null)
            sfxSlider.value = savedSFX;

        // Listener slider
        if (bgmSlider != null)
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // =========================
    // PLAY BGM
    // =========================
    public void PlayBGM(int index)
    {
        if (index < 0 || index >= bgm.Length)
        {
            Debug.LogWarning("BGM index out of range!");
            return;
        }

        if (bgmSource.clip == bgm[index])
            return;

        bgmSource.clip = bgm[index];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // =========================
    // STOP BGM
    // =========================
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // =========================
    // PLAY SFX
    // =========================
    public void PlaySFX(int index)
    {
        if (index < 0 || index >= sfx.Length)
        {
            Debug.LogWarning("SFX index out of range!");
            return;
        }

        sfxSource.PlayOneShot(sfx[index]);
    }

    // =========================
    // VOLUME CONTROL
    // =========================
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
