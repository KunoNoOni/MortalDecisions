using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour 
{
    private MusicManager mm;
    private SoundManager sm;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider soundSlider;
    private bool activatePauseMenu = false;

    public GameObject panel;

    private void Awake()
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void OnEnable()
    {
        musicSlider.value = mm.channel.volume;
        soundSlider.value = sm.channels[0].volume;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            activatePauseMenu = !activatePauseMenu;
            panel.SetActive(activatePauseMenu);
        }

        if(activatePauseMenu)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void SetMusicVolume()
    {
        mm.SetMusicVolume(musicSlider.value);
    }

    public void SetSoundVolume()
    {
        sm.SetSoundVolume(soundSlider.value);
    }
}
