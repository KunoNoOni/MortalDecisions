using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public Button contactDispatchButton;
    public GameObject dispatchPanel;
    public GameObject contactDispatchPanel;
    public GameObject jobPanel;
    public GameObject fuelPanel;
    public GameObject repairPanel;
    public GameObject spacePirateAttackPanel;
    public Text jobOneText;
    public Text jobTwoText;

    private LogScreen logScreen;
    private GameManager gameManager;
    private JobDatabase jobDatabase;
    private CameraShaker cameraShaker;
    private PlayerShip playerShip;
    private Registry registry;

    SoundManager sm;

    private void Start()
    {
        logScreen = GameObject.Find("LogScreen").GetComponent<LogScreen>();
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        jobDatabase = GameObject.Find("Canvas").GetComponent<JobDatabase>();
        cameraShaker = Camera.main.GetComponent<CameraShaker>();
        playerShip = GameObject.Find("PlayerShip").GetComponent<PlayerShip>();
        registry = GameObject.Find("Registry").GetComponent<Registry>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private int GetRandomNumber(int min, int max)
    {
        System.Random rndValue = new System.Random();

        return rndValue.Next(min, max);
    }

    public void ContactDispatchButton()
    {
        sm.PlaySound(sm.sounds[1]);
        logScreen.AddMessage("Contacting Dispatch.");
        contactDispatchButton.enabled = false;
        dispatchPanel.SetActive(true);
        repairPanel.SetActive(false);
        fuelPanel.SetActive(false);
        jobPanel.SetActive(false);
        contactDispatchPanel.SetActive(true);
    }

    public void GetJobButton()
    {
        sm.PlaySound(sm.sounds[1]);
        if ((gameManager.GetHullPercent() <= 25 && gameManager.GetEnginePercent() <= 25) || gameManager.GetHullPercent() <= 25 || gameManager.GetEnginePercent() <= 25)
            logScreen.AddMessage("Hmm, looks like you need to get repaired first.");
        else
        {
            contactDispatchPanel.SetActive(false);
            jobPanel.SetActive(true);
            jobOneText.text = jobDatabase.GetMultipleJobText();
            jobTwoText.text = jobDatabase.GetSingleJobText();
        }
    }

    public void GetFuelButton()
    {
        sm.PlaySound(sm.sounds[1]);
        dispatchPanel.SetActive(false);

        if (gameManager.GetFuel() == 100)
        {
            logScreen.AddMessage("Sorry, you don't need fuel right now.");
            contactDispatchButton.enabled = true;
        }
        else if (gameManager.GetCredits() < 250)
            logScreen.AddMessage("Sorry, you don't have the 250 credit it costs to refuel.");
        else
        {
            logScreen.AddMessage("Acknowledged, sending fuel ship to your location.");
            gameManager.SetCredits(gameManager.GetCredits() - 250);
            gameManager.DoRefueling();
        }
    }

    public void RepairShipButton()
    {
        sm.PlaySound(sm.sounds[1]);
        if (gameManager.GetHullPercent() == 100 && gameManager.GetEnginePercent() == 100)
            logScreen.AddMessage("Sorry, you don't need your ship repaired.");
        else if (gameManager.GetCredits() < 500)
            logScreen.AddMessage("Sorry, you don't have the 500 credit it costs to repair.");
        else
        {
            logScreen.AddMessage("Roger, sending repair crew to your location.");
            gameManager.SetCredits(gameManager.GetCredits() - 500);
            gameManager.DoRepairing();
        }
        dispatchPanel.SetActive(false);
    }

    public void AcceptJobOneButton()
    {
        sm.PlaySound(sm.sounds[1]);
        dispatchPanel.SetActive(false);
        registry.SetNumberOfPeopleSacrificed(registry.GetNumberOfPeopleSacrificed() + jobDatabase.GetCurrentSinglePeopleKilled());
        gameManager.DoJob(jobDatabase.GetCurrentMultipleJobCredits(), jobDatabase.GetCurrentMultipleJobRisk(), true);
    }

    public void AcceptJobTwoButton()
    {
        sm.PlaySound(sm.sounds[1]);
        dispatchPanel.SetActive(false);
        registry.SetNumberOfPeopleSacrificed(registry.GetNumberOfPeopleSacrificed() + jobDatabase.GetCurrentMultiplePeopleKilled());
        gameManager.DoJob(jobDatabase.GetCurrentSingleJobCredits(), jobDatabase.GetCurrentSingleJobRisk(), false);
    }

    public void ShowSpacePirateAttackPanel()
    {
        spacePirateAttackPanel.SetActive(true);
    }

    public void TryToOutrunButton()
    {
        sm.PlaySound(sm.sounds[1]);
        int rndNumber = GetRandomNumber(1, 100);
        if (gameManager.GetJobRisk() == "low" && rndNumber <= 70)
        {
            OutrunSuccess();
        }
        else if (gameManager.GetJobRisk() == "low")
        {
            OutrunFailure();
            gameManager.CheckForLoseCondition();
        }

        if (gameManager.GetJobRisk() == "medium" && rndNumber <= 50)
        {
            OutrunSuccess();
        }
        else if (gameManager.GetJobRisk() == "medium")
        {
            OutrunFailure();
            gameManager.CheckForLoseCondition();
        }
        
        if (gameManager.GetJobRisk() == "high" && rndNumber <= 30)
        {
            OutrunSuccess();
        }
        else if (gameManager.GetJobRisk() == "high")
        {
            OutrunFailure();
            gameManager.CheckForLoseCondition();
        }
    }

    public void DropCargoAndFleeButton()
    {
        sm.PlaySound(sm.sounds[1]);
        playerShip.ChangeSprite(false, true);
        logScreen.AddMessage("Dropping your cargo and throwing the engines into overdrive saves the -");
        logScreen.AddMessage("ship but hurts the bank account.");
        if(gameManager.GetJobOneSelected())
            registry.SetNumberOfPeopleSacrificed(registry.GetNumberOfPeopleSacrificed() + jobDatabase.GetCurrentSinglePeopleKilled());
        else
            registry.SetNumberOfPeopleSacrificed(registry.GetNumberOfPeopleSacrificed() + jobDatabase.GetCurrentMultiplePeopleKilled());
        gameManager.SetStageToZero();
        spacePirateAttackPanel.SetActive(false);
        contactDispatchButton.enabled = true;
        gameManager.SetFuel(gameManager.GetFuel() - 5);
        gameManager.CheckForFuel();
    }

    private void OutrunSuccess()
    {
        playerShip.ChangeSprite(false, true);
        gameManager.SetFuel(gameManager.GetFuel() - 5);
        gameManager.CheckForFuel();
        logScreen.AddMessage("With the engines in overdrive and some fancy flying the Space Pirates -");
        logScreen.AddMessage("are a distant memory.");
        logScreen.AddMessage("Now back to delivering your cargo.");
        spacePirateAttackPanel.SetActive(false);
        if (gameManager.GetJobOneSelected())
            gameManager.DoJob(jobDatabase.GetCurrentSingleJobCredits(), jobDatabase.GetCurrentSingleJobRisk(), true);
        else
            gameManager.DoJob(jobDatabase.GetCurrentMultipleJobCredits(), jobDatabase.GetCurrentMultipleJobRisk(), false);
    }

    private void OutrunFailure()
    {
        logScreen.AddMessage("Attempting to run only angered the Space Pirates!");
        int rndNumber = GetRandomNumber(1, 100);
        if(rndNumber >= 70)
        {
            logScreen.AddMessage("The Space Pirates attack and damaged the hull!");
            cameraShaker.RumpShaker(.3f);
            gameManager.SetHullPercent(gameManager.GetHullPercent() - GetRandomNumber(5, 15));
        }
        else if(rndNumber >= 50)
        {
            logScreen.AddMessage("The Space Pirates attack and damaged the engines!");
            cameraShaker.RumpShaker(.3f);
            gameManager.SetEnginePercent(gameManager.GetEnginePercent() - GetRandomNumber(5, 15));
        }
        else if (rndNumber >= 30)
        {
            logScreen.AddMessage("The Space Pirates attack and damaged both the hull and the engines!");
            cameraShaker.RumpShaker(.5f);
            gameManager.SetHullPercent(gameManager.GetHullPercent() - GetRandomNumber(5, 15));
            gameManager.SetEnginePercent(gameManager.GetEnginePercent() - GetRandomNumber(5, 15));
        }
        playerShip.ChangeSprite(false, false);
        gameManager.SetFuel(gameManager.GetFuel() - 5);
        gameManager.CheckForFuel();

    }
}
