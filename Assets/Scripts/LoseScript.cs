using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour {

    public Text loseText;

    private Registry registry;

    private FadeInAndOut fadeInAndOut;

    MusicManager mm;
    SoundManager sm;

    void Awake()
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Start () 
	{
        mm.PlaySound(mm.music[6]);
        registry = GameObject.Find("Registry").GetComponent<Registry>();
        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();

        int numPeople = registry.GetNumberOfPeopleSacrificed();
        string personPeople;

        if (numPeople > 1)
            personPeople = "people";
        else
            personPeople = "person";

        if (registry.ranOutOfFuel)
        {
            loseText.text = "You should have fueled up before taking the job. Now you're a sitting duck for the Space Pirates! Was it really worth sacrificing "+numPeople+" "+personPeople+"? Better luck next time buddy.";
        }

        if (registry.spacePiratesDestroyedMe)
        {
            loseText.text = "You see a Space Pirate out the forward view. They fire and in a brilliant flash of light you and your ship are no more and you join the "+numPeople+" "+personPeople+" you sacrificed";
        }
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
