using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource playerSource;
    public AudioSource enemySource;
    public AudioSource tileSource;
    public AudioSource waveChangeSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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

    public void PlayEnemy(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;

        // Maak een tijdelijk GameObject voor geluid
        GameObject tempGO = new GameObject("EnemySFX");
        tempGO.transform.position = position;

        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.spatialBlend = 1f; // 3D geluid
        aSource.rolloffMode = AudioRolloffMode.Linear;
        aSource.minDistance = 1f;
        aSource.maxDistance = 30f;

        aSource.PlayOneShot(clip, volume);
        Destroy(tempGO, clip.length); // verwijder automatisch
    }


    public void PlayTileDrop(AudioClip clip)
    {
        if(clip == null) return;
        tileSource.PlayOneShot(clip);
    }

    public void PlayWaveChange(AudioClip clip)
    {
        if (clip == null) return;
        waveChangeSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayTileSound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;

        // Maak een tijdelijk GameObject voor geluid
        GameObject tempGO = new GameObject("TileSFX");
        tempGO.transform.position = position;

        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.spatialBlend = 1f; // 3D geluid
        aSource.rolloffMode = AudioRolloffMode.Linear;
        aSource.minDistance = 1f;
        aSource.maxDistance = 18f;

        aSource.PlayOneShot(clip, volume);
        Destroy(tempGO, clip.length); // verwijder automatisch
    }
}
