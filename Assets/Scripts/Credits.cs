using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
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
        mm.PlaySound(mm.music[1]);
    }

    public void GotoTitlescreen()
    {
        sm.PlaySound(sm.sounds[1]);
        fadeInAndOut.DoFade(0);
    }
}
