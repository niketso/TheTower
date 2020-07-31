using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Range(0f,1f)]
    [SerializeField] public  float startingVolume; 

    //[SerializeField] public AudioMixer mixer;
    public  Sound[] sounds;

    public static AudioManager Instance
    {
        get
        {
            if (instance== null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
            return instance;
        }

    }
    private void Awake()
    {
        instance = this;
        
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", startingVolume);
        }
        else
        {
            startingVolume = PlayerPrefs.GetFloat("volume");
        }
        
    }

    private void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = startingVolume;
        }

        AudioManager.instance.Play("MusicGameplay",true);       
    }
  
    public void Play(string name, bool loop)
    {
       
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.loop = loop;
                s.source.Play();
                
                //Debug.Log("Reproducciendo sonido" + s.clip.name +"A este volumen:" + s.source.volume);
            }
        }
    }

    public void UpdateVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }

        
    }

    public bool SoundPlaying(string name)
    {
        bool isPlaying = false;

        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                 isPlaying = s.source.isPlaying;
            }
        }
        return isPlaying;
    }
}
