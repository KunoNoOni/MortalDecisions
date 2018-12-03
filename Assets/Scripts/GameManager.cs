using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private int credits;

    public int GetCredits()
    {
        return credits;
    }

    public void SetCredits(int value)
    {
        credits = value;
        if (credits < 0)
            credits = 0;
        creditText.text = credits.ToString();
    }

    private int fuel;

    public int GetFuel()
    {
        return fuel;
    }

    public void SetFuel(int value)
    {
        fuel = value;
        if (fuel < 0)
            fuel = 0;
        fuelText.text = fuel.ToString();
    }

    private int hullPercent;

    public int GetHullPercent()
    {
        return hullPercent;
    }

    public void SetHullPercent(int value)
    {
        hullPercent = value;
        if (hullPercent > 100)
            hullPercent = 100;
        hullText.text = hullPercent.ToString() + "%";
    }

    private int enginePercent;

    public int GetEnginePercent()
    {
        return enginePercent;
    }

    public void SetEnginePercent(int value)
    {
        enginePercent = value;
        if (enginePercent > 100)
            enginePercent = 100;
        engineText.text = enginePercent.ToString() + "%";
    }

    public Button contactDispatchButton;
    public Text creditText;
    public Text fuelText;
    public Text hullText;
    public Text engineText;
    
    private LogScreen logScreen;
    private CameraShaker cameraShaker;
    private PopupManager popupManager;
    private Registry registry;
    private PlayerShip playerShip;
    private FadeInAndOut fadeInAndOut;
    private float fuelAndRepairCooldown;
    private float fuelAndRepairCooldownReset;
    private float jobCooldown;
    private float jobCooldownReset;
    private bool doFuel;
    private bool doRepair;
    private bool doJob;
    private int stage;
    private int jobCredits;
    private string jobRisk;
    private bool jobOneSelected;
    private bool jobTwoSelected;
    private readonly int creditGoal = 10000;

    MusicManager mm;
    SoundManager sm;

    void Awake()
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void Start () 
	{
        SetCredits(0);
        SetFuel(100);
        SetHullPercent(100);
        SetEnginePercent(100);
        logScreen = GameObject.Find("LogScreen").GetComponent<LogScreen>();
        popupManager = GameObject.Find("GameScreen").GetComponent<PopupManager>();
        cameraShaker = Camera.main.GetComponent<CameraShaker>();
        registry = GameObject.Find("Registry").GetComponent<Registry>();
        playerShip = GameObject.Find("PlayerShip").GetComponent<PlayerShip>();
        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
        registry.SetNumberOfPeopleSacrificed(0);
        fuelAndRepairCooldownReset = 5f;
        fuelAndRepairCooldown = fuelAndRepairCooldownReset;
        jobCooldownReset = 10f;
        jobCooldown = 0;
        doFuel = false;
        doRepair = false;
        doJob = false;
        jobOneSelected = false;
        stage = 0;
        mm.PlaySound(mm.music[GetRandomNumber(1,4)]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


        if(doFuel)
        {
            fuelAndRepairCooldown -= Time.deltaTime;
            if(fuelAndRepairCooldown <= 0)
            {
                Refueling();
            }
        }

        if (doRepair)
        {
            fuelAndRepairCooldown -= Time.deltaTime;
            if (fuelAndRepairCooldown <= 0)
            {
                Repairing();
            }
        }

        if(doJob)
        {
            jobCooldown -= Time.deltaTime;
            if (jobCooldown <= 0)
            {
                Job();
            }
        }
    }

    private int GetRandomNumber(int min, int max)
    {
        System.Random rndValue = new System.Random();

        return rndValue.Next(min, max);
    }

    public void DoRefueling()
    {
        doFuel = true;
    }

    private void Refueling()
    {
        doFuel = false;
        fuelAndRepairCooldown = fuelAndRepairCooldownReset;
        stage++;
        switch (stage)
        {
            case 1:
                logScreen.AddMessage("Sensors indicate the fuel ship is approaching.");
                doFuel = true;
                break;
            case 2:
                cameraShaker.RumpShaker(.3f);
                logScreen.AddMessage("Fuel ship has connected and is commencing with the refuling.");
                doFuel = true;
                break;
            case 3:
                logScreen.AddMessage("Fuel ship has completed the refueling and has disconnected.");
                SetFuel(100);
                stage = 0;
                contactDispatchButton.enabled = true;
                break;
        }
    }

    public void DoRepairing()
    {
        doRepair = true;
    }

    private void Repairing()
    {
        doRepair = false;
        fuelAndRepairCooldown = fuelAndRepairCooldownReset;
        stage++;
        switch (stage)
        {
            case 1:
                logScreen.AddMessage("Sensors indicate the repair ship is approaching.");
                doRepair = true;
                break;
            case 2:
                cameraShaker.RumpShaker(.3f);
                logScreen.AddMessage("Repair ship has connected and is commencing with the repair.");
                doRepair = true;
                break;
            case 3:
                logScreen.AddMessage("Repair ship has completed the repairng and has disconnected.");
                SetHullPercent(100);
                SetEnginePercent(100);
                stage = 0;
                contactDispatchButton.enabled = true;
                break;
        }
    }

    public void DoJob(int credits, string risk, bool isJobOne)
    {
        doJob = true;
        jobCredits = credits;
        jobRisk = risk;
        if (isJobOne)
            jobOneSelected = true;
    }

    private void Job()
    {
        doJob = false;
        jobCooldown = jobCooldownReset;
        stage++;
        switch (stage)
        {
            case 1:
                logScreen.AddMessage("Coordinates received. Course plotted. Engines engaged!");
                playerShip.ChangeSprite(false, true);
                sm.PlaySound(sm.sounds[4]); //<<-----------------------------------
                SetFuel(GetFuel() - GetRandomNumber(10, 20));
                CheckForFuel();
                doJob = true;
                break;
            case 2:
                cameraShaker.RumpShaker(.3f);
                logScreen.AddMessage("Ship has docked and the containers are being loaded.");
                SetFuel(GetFuel() - GetRandomNumber(10, 20));
                CheckForFuel();
                doJob = true;
                break;
            case 3:
                logScreen.AddMessage("Delivery coordinates entered. New course plotted. Engines engaged!");
                SetFuel(GetFuel() - GetRandomNumber(10, 20));
                CheckForFuel();
                doJob = true;
                break;
            case 4:
                logScreen.AddMessage("Scanning for Space Pirates...");
                int rndNumber = GetRandomNumber(1, 100);
                if (jobRisk == "high" && rndNumber <= 70)
                    ActivateSpacePirates();
                else if (jobRisk == "medium" && rndNumber <= 50)
                    ActivateSpacePirates();
                else if (jobRisk == "low" && rndNumber <= 30)
                    ActivateSpacePirates();
                else
                    doJob = true;
                break;
            case 5:
                logScreen.AddMessage("Destination in sight. Ship has landed and containers have been unloaded.");
                logScreen.AddMessage("Another satisfied customer! Credits have been transferred.");
                SetFuel(GetFuel() - GetRandomNumber(10, 20));
                SetCredits(GetCredits() + jobCredits);
                stage = 0;
                contactDispatchButton.enabled = true;
                jobOneSelected = false;
                jobCooldown = 0;
                playerShip.ChangeSprite(true, false);
                CheckForWinCondition();
                break;
        }
    }

    public void CheckForFuel()
    {
        if (GetFuel() == 0)
        {
            registry.ranOutOfFuel = true;
            fadeInAndOut.DoFade(4);
        }
    }

    private void ActivateSpacePirates()
    {
        sm.PlaySound(sm.sounds[5]); //<--------------------------------------------------------------
        logScreen.AddMessage("Sensors indicate multiple hostiles inbound. Space Pirates!");
        playerShip.ChangeSprite(true, false);
        popupManager.ShowSpacePirateAttackPanel();
    }

    private void CheckForWinCondition()
    {
        if (GetCredits() >= creditGoal)
            fadeInAndOut.DoFade(3);
    }

    public void CheckForLoseCondition()
    {
        if (GetHullPercent() == 0 || GetEnginePercent() == 0)
        {
            registry.spacePiratesDestroyedMe = true;
            fadeInAndOut.DoFade(4);
        }
        else
            popupManager.ShowSpacePirateAttackPanel();
    }

    public string GetJobRisk()
    {
        return jobRisk;
    }

    public bool GetJobOneSelected()
    {
        return jobOneSelected;
    }

    public void SetStageToZero()
    {
        stage = 0;
    }
}
