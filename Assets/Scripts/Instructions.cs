using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public GameObject[] pages;
    public Button play;
    public Button previous;
    public Button next;
    public Image panel;

    private FadeInAndOut fadeInAndOut;
    private int pageIndex = 0;

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
    }

    public void PlayButton()
    {
        sm.PlaySound(sm.sounds[1]);
        FadeToLevel();
    }

    public void PreviousButton()
    {
        sm.PlaySound(sm.sounds[1]);
        next.interactable = true;
        pages[pageIndex].SetActive(false);
        pageIndex--;
        pages[pageIndex].SetActive(true); 
        if (pageIndex == 0)
            previous.interactable = false;
    }

    public void NextButton()
    {
        sm.PlaySound(sm.sounds[1]);
        previous.interactable = true;
        pages[pageIndex].SetActive(false);
        pageIndex++;
        pages[pageIndex].SetActive(true);
        if (pageIndex == pages.Length-1)
            next.interactable = false;
    }

    private void FadeToLevel()
    {
        previous.interactable = false;
        play.interactable = false;
        next.interactable = false;
        
        fadeInAndOut.DoFade(2);
    }

}
