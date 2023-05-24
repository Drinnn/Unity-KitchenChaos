using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    
    public static MusicManager Instance { get; private set; }
    
    public float Volume => _volume;

    private AudioSource _audioSource;
    
    private float _volume = .3f;

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        _volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
        _audioSource.volume = _volume;
    }

    public void ChangeVolume()
    {
        if (_volume > 1f)
        {
            _volume = 0;
        }
        else
        {
            _volume += .1f;
        }

        _audioSource.volume = _volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, _volume);
        PlayerPrefs.Save();
    }
}
