using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    public  AudioClip backgroundMusicStart;
    public  AudioClip backgroundMusicPlay;

    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ChangeMusic(backgroundMusicStart);
    }

      void OnEnable()
    {
        // Subscribe to Unity's scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Change music depending on the scene name
        if (scene.name == "Start Scene")
        {
            ChangeMusic(backgroundMusicStart);
        }
        else
        {
            ChangeMusic(backgroundMusicPlay);
        }
    }

    void ChangeMusic(AudioClip newClip)
    {
        // Only change and play if it's a different track to prevent restarting
        if (audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }


    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     musicSlider.onValueChanged.AddListener(delegate {OnValueChanged();});
    //     if(backgroundMusicStart != null)
    //     {
    //         PlayBackgroundMusic(backgroundMusicStart);
    //     }   

    // }

    public void OnValueChanged()
    {
        SetVolume(musicSlider.value);
    }

    public static void SetVolume(float volume)
    {
        Instance.audioSource.volume = volume;
    }

    // public void PlayBackgroundMusic(AudioClip audioClip = null)
    // {
    //     if(audioClip != null)
    //     {
    //         audioSource.clip = audioClip;
    //     }
    //     if (audioSource.clip != null)
    //     {
    //         audioSource.Play();
    //     }
    // }
}
