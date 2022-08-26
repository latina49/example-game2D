using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuControl : MonoBehaviour
{
    public Button b_setting;
    public GameObject s_audio;
    public Toggle use_music;
    public Slider music_volume_slider;
    public void OnPlayClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnPlayClickMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnSettingClick()
    {
        s_audio.SetActive(!s_audio.activeSelf);
   
    }
    // Start is called before the first frame update
    void Start()
    {
        s_audio.SetActive(false);
        b_setting.onClick.AddListener(() => OnSettingClick());
        bool off_music;
        bool.TryParse(PlayerPrefs.GetString("Music"), out off_music);
        use_music.isOn = !off_music;
        float music_volume = PlayerPrefs.GetFloat("MusicVolume");
        music_volume_slider.value = music_volume;
    }
    public void OnSliderChange(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    public void OnMusicEnable(bool enable)
    {
        PlayerPrefs.SetString("Music", (!enable).ToString());

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
