using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScript : MonoBehaviour {

    public GameObject winpanel1;
    public GameObject winpanel2;
    public GameObject winpanel3;
    public GameObject ending1;
    public GameObject ending2;

    public Text winpanel3Text;
    public Text ending1Text;
    public Text ending2Text;

    private JobDatabase jobDatabase;
    private string familyMember;
    private Registry registry;
    private int numPeople;
    private string personPeople;

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
        mm.PlaySound(mm.music[5]);
        jobDatabase = GameObject.Find("Canvas").GetComponent<JobDatabase>();
        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
        registry = GameObject.Find("Registry").GetComponent<Registry>();

        numPeople = registry.GetNumberOfPeopleSacrificed();
        

        if (numPeople > 1)
            personPeople = "people";
        else
            personPeople = "person";
    }

    public void WinPageButton1()
    {
        sm.PlaySound(sm.sounds[1]);
        winpanel1.SetActive(false);
        winpanel2.SetActive(true);
    }

    public void WinPageButton2()
    {
        familyMember = jobDatabase.GetFamilyMember();
        sm.PlaySound(sm.sounds[1]);
        winpanel2.SetActive(false);
        winpanel3.SetActive(true);
        winpanel3Text.text = "You play the message and you're surprised to hear the voice of your " + familyMember + "! They say they are very sick and ask you for your help. There is a treatment that can cure them but it is very expensive. Infact it will take everything you have in your retirement fund to pay for this.";
    }

    public void WinPageButton3()
    {
        sm.PlaySound(sm.sounds[1]);
        winpanel3.SetActive(false);
        ending1.SetActive(true);
        ending1Text.text = "It didn't take you long to make your decision. You contact your " + familyMember + " and let them know you will give them the money. You plot a course for their homeplanet and set the autopilot. You contact dispatch and let them know you've changed your mind about retiring and explain the situation. Then you lay down for a well deserved nap, it's going to be a long trip. BTW you sacrificed " + numPeople + " " + personPeople + "!";
    }

    public void WinPageButton4()
    {
        sm.PlaySound(sm.sounds[1]);
        winpanel3.SetActive(false);
        ending2.SetActive(true);
        ending2Text.text = "With the push of a button you delete the message from your " + familyMember + ". You've made enough sacrifices for everyone, it's time you take care of yourself! You set the autopilot for the planet Otera. The shimmering pools it is! Then you lay down for a well deserved nap.\n\nBTW you sacrificed " + numPeople + " " + personPeople + "!";
    }

    public void GotoTitlescreen()
    {
        sm.PlaySound(sm.sounds[1]);
        registry.ranOutOfFuel = false;
        registry.spacePiratesDestroyedMe = false;
        registry.SetNumberOfPeopleSacrificed(0);
        fadeInAndOut.DoFade(0);
    }

}

