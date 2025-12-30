using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource playerSource;
    public AudioSource enemySource;
    public AudioSource tileSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySpell(AudioClip clip)
    {
        if (clip == null) return;
        playerSource.PlayOneShot(clip);
    }

    public void PlayFootstep(AudioClip clip)
    {
        if (clip == null) return;
        playerSource.pitch = Random.Range(0.95f, 1.05f);
        playerSource.PlayOneShot(clip);
    }

    public void PlayHurt(AudioClip clip)
    {
        if (clip == null) return;
        playerSource.PlayOneShot(clip);
    }

    public void PlayEnemy(AudioClip clip)
    {
        if (clip == null) return;
        enemySource.PlayOneShot(clip);
    }

    public void PlayTileDrop(AudioClip clip)
    {
        if(clip == null) return;
        tileSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
