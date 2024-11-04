using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    private AudioSource audioSource;
    public AudioClip audioClipWinner;

    public AudioClip[] sceneMusicClips;

    //singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Iniciar la música de la escena actual al començar el joc
        PlaySceneMusic(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Iniciar la musica de la escena actual al cambiar de escena
        PlaySceneMusic(scene.buildIndex);
    }

    private void PlaySceneMusic(int sceneIndex)
    {
        // Asegurem que l'index esta dins del rang de la llista
        if (sceneIndex >= 0 && sceneIndex < sceneMusicClips.Length && sceneMusicClips[sceneIndex] != null)
        {
            audioSource.Stop();  //para la música actual
            audioSource.clip = sceneMusicClips[sceneIndex]; //cambia la música
            audioSource.Play();  // reprodueix la música
        }
    }

    public void PlayAudio(AudioClip audioClip) //no el faig anar
    {
        audioSource.PlayOneShot(audioClip);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayWinner()
    {
        audioSource.Stop();
        audioSource.clip = audioClipWinner;
        audioSource.loop = false;
        audioSource.PlayOneShot(audioClipWinner);
        audioSource.loop = true;
    }

}
