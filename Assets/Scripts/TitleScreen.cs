using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScreen : MonoBehaviour
{
    public Button playButton;

    private FadeInAndOut fadeInAndOut;

    MusicManager mm;
    SoundManager sm;    

    void Awake() 
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void Start()
    {
        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
        mm.PlaySound(mm.music[0]);
        mm.SetMusicVolume(.5f);
        sm.SetSoundVolume(.5f);
    }

    public void PlayButton()
    {
        sm.PlaySound(sm.sounds[1]);
        fadeInAndOut.DoFade(2);
    }

    public void InstructionsButton()
    {
        sm.PlaySound(sm.sounds[1]);
        fadeInAndOut.DoFade(1);
    }

    public void CreditsButton()
    {
        sm.PlaySound(sm.sounds[1]);
        fadeInAndOut.DoFade(5);
    }

    public void QuitButton()
    {
        sm.PlaySound(sm.sounds[1]);
        Application.Quit();
    }
}
