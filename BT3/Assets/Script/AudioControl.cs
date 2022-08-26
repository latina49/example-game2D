using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public static AudioControl instance;
    public AudioSource musicSourse;
    public AudioClip musicClip;
    public AudioSource ffxSource;
    public AudioClip shoot_sound;
    public bool use_music;
    // Start is called before the first frame update
    void Start()
    {
        GetAudioData();
        instance = this;
        musicSourse.clip = musicClip;
        if (use_music) musicSourse.Play();
        //musicSourse = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play_Shoot()
    {
        ffxSource.PlayOneShot(shoot_sound);
    }

    void GetAudioData()
    {
        bool.TryParse(PlayerPrefs.GetString("Music"),out use_music);
        musicSourse.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
