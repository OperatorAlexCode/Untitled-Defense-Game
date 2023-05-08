using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // AudioClips
    public AudioClip DayTheme;
    public AudioClip NightTheme;

    // Other
    public AudioSource MusicPlayer;
    public PlayerSettings Settings;

    // Start is called before the first frame update
    void Awake()
    {
        Settings = GameObject.Find("PlayerSettings").gameObject.GetComponent<PlayerSettings>();
        PlayNightTheme();
    }

    void Update()
    {
        MusicPlayer.volume = Settings.MusicVolume;
    }

    public void PlayDayTheme()
    {
        MusicPlayer.Stop();
        MusicPlayer.clip = DayTheme;
        MusicPlayer.Play();
    }

    public void PlayNightTheme()
    {
        MusicPlayer.Stop();
        MusicPlayer.clip = NightTheme;
        MusicPlayer.Play();
    }
}
