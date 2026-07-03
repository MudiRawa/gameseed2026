using UnityEngine;

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

    [Header("Volume")]
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

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

        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;
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
    // CHANGE VOLUME
    // =========================
    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = volume;
    }
}
