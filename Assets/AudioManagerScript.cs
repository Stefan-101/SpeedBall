using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip mainMenuMusic;
    public AudioClip gameSceneMusic;
    public AudioClip clickSound;
    public AudioClip collisionWithBallSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; //adding audio after the scene fully loaded
        }
        else
        {
            Destroy(gameObject); //no duplicates
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartMenu")
        {
            PlayBackgroundMusic(mainMenuMusic);
        }
        else if (scene.name == "SampleScene")
        {
            PlayBackgroundMusic(gameSceneMusic);
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) //if the game already plays a sound
            return;

        //else play the sound
        musicSource.clip = clip; 
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip); //the sound is played only once
    }
    public void PlayClickSound()
    {
        PlaySFX(clickSound);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
