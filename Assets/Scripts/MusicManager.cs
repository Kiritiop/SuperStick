using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioClip MenuTheme;
    public AudioClip Arena1Theme;
    public AudioClip Arena2Theme;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
     AudioSource audioSource = GetComponent<AudioSource>();

        if (scene.name == "MainMenu" || scene.name == "LevelSelection")
        {
            // Only start the music if it isn't already playing
            if (audioSource.clip != MenuTheme)
            {
                audioSource.clip = MenuTheme;
                audioSource.Play();
            }
        }
        else if (scene.name == "Arena1")
        {
            audioSource.clip = Arena1Theme;
            audioSource.Play();
        }
        else if (scene.name == "Arena2")
        {
            audioSource.clip = Arena2Theme;
            audioSource.Play();
        }
    }
}