using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class JobDatabase : MonoBehaviour {

    private List<string> multiplePeopleJobs;
    private List<string> singlePersonJobs;
    private List<string> multipleThings;
    private List<string> singleThings;
    private List<string> singlePeople;
    private List<string> singlePets;
    private List<string> yourFamily;
    private List<string> risk;
    private string jobPaysString;
    private string spacePirateRisk;
    private string currentSingleJobRisk;
    private int currentSingleJobCredits;
    private string currentMultipleJobRisk;
    private int currentMultipleJobCredits;
    private int currentSinglePeopleKilled;
    private int currentMultiplePeopleKilled;

    private void Start()
    {
        multiplePeopleJobs = new List<string>();
        singlePersonJobs = new List<string>();
        multipleThings = new List<string>();
        singleThings = new List<string>();
        singlePeople = new List<string>();
        singlePets = new List<string>();
        yourFamily = new List<string>();
        risk = new List<string>();
        jobPaysString = "Job pays number credits.";
        spacePirateRisk = "Space Pirate risk is risklevel.";
        currentSinglePeopleKilled = 1;
        AddItemsToLists();
    }

    private void AddItemsToLists()
    {
        multiplePeopleJobs.Add("Bring a shipment of ammunition to a group of number settlers to help them survive the wildlife.");
        multiplePeopleJobs.Add("Bring a multiThing to a group of number settlers to help them survive.");
        multiplePeopleJobs.Add("A planet is at the brink of destruction. Bring a shipment of wormhole generators to a group of number settlers to help them escape.");
        singlePersonJobs.Add("person is in desperate need of thing or they will not survive.");
        singlePersonJobs.Add("person is in desperate need of pet or they will die heartbroken.");
        multipleThings.Add("shipment of water");
        multipleThings.Add("shipment of food");
        multipleThings.Add("shipment of video games");
        multipleThings.Add("shipment of medicine");
        multipleThings.Add("shipment of sildenafil");
        singleThings.Add("a liver transplant");
        singleThings.Add("a heart transplant");
        singleThings.Add("a kidney transplant");
        singleThings.Add("a lung transplant");
        singleThings.Add("a shipment of sildenafil");
        singleThings.Add("a shipment of medicine");
        singlePeople.Add("A high ranking official");
        singlePeople.Add("A foreign dignitary");
        singlePeople.Add("A planetary leader");
        singlePeople.Add("Santa Claus");
        singlePeople.Add("Mrs Claus");
        singlePeople.Add("A woman");
        singlePeople.Add("A man");
        singlePeople.Add("A child");
        singlePeople.Add("A baby");
        singlePets.Add("a dog");
        singlePets.Add("a cat");
        singlePets.Add("a puppy");
        singlePets.Add("a kitten");
        yourFamily.Add("mother");
        yourFamily.Add("father");
        yourFamily.Add("brother");
        yourFamily.Add("sister");
        yourFamily.Add("grandmother");
        yourFamily.Add("grandfather");
        yourFamily.Add("aunt");
        yourFamily.Add("uncle");
        yourFamily.Add("niece");
        yourFamily.Add("nephew");
        yourFamily.Add("dog");
        yourFamily.Add("cat");
        yourFamily.Add("puppy");
        yourFamily.Add("kitten");
        yourFamily.Add("lover");
        yourFamily.Add("best friend");
        yourFamily.Add("childhood friend");
        risk.Add("low");
        risk.Add("medium");
        risk.Add("high");
    }

    private int GetRandomNumber(int min, int max)
    {
        System.Random rndValue = new System.Random();

        return rndValue.Next(min, max);
    }

    public string GetSingleJobText()
    {
        string singleJob;
        StringBuilder jobText = new StringBuilder(singlePersonJobs[GetRandomNumber(0, singlePersonJobs.Count)]);
        StringBuilder credits = new StringBuilder(jobPaysString);
        StringBuilder jobRisk = new StringBuilder(spacePirateRisk); 

        jobText.Replace("person", singlePeople[GetRandomNumber(0, singlePeople.Count)]);
        jobText.Replace("thing", singleThings[GetRandomNumber(0, singleThings.Count)]);
        jobText.Replace("pet", singlePets[GetRandomNumber(0, singlePets.Count)]);

        currentSingleJobCredits = GetRandomNumber(1000, 3000);
        credits.Replace("number", currentSingleJobCredits.ToString());

        currentSingleJobRisk = risk[GetRandomNumber(0, risk.Count)];
        jobRisk.Replace("risklevel", currentSingleJobRisk);

        singleJob = jobText.ToString() + " " + credits.ToString() + " " + jobRisk.ToString();
        
        return singleJob;
    }

    public string GetMultipleJobText()
    {
        string multipleJob;
        StringBuilder jobText = new StringBuilder(multiplePeopleJobs[GetRandomNumber(0, multiplePeopleJobs.Count)]);
        StringBuilder credits = new StringBuilder(jobPaysString);
        StringBuilder jobRisk = new StringBuilder(spacePirateRisk);

        jobText.Replace("multiThing", multipleThings[GetRandomNumber(0, multipleThings.Count)]);
        currentMultiplePeopleKilled = GetRandomNumber(10, 100);
        jobText.Replace("number", currentMultiplePeopleKilled.ToString());

        currentMultipleJobCredits = GetRandomNumber(1500, 3500);
        credits.Replace("number", currentMultipleJobCredits.ToString());

        currentMultipleJobRisk = risk[GetRandomNumber(0, risk.Count)];
        jobRisk.Replace("risklevel", currentMultipleJobRisk);

        multipleJob = jobText.ToString() + " " + credits.ToString() + " " + jobRisk.ToString();

        return multipleJob;
    }

    public int GetCurrentSingleJobCredits()
    {
        return currentSingleJobCredits;
    }

    public string GetCurrentSingleJobRisk()
    {
        return currentSingleJobRisk;
    }

    public int GetCurrentMultipleJobCredits()
    {
        return currentMultipleJobCredits;
    }

    public string GetCurrentMultipleJobRisk()
    {
        return currentMultipleJobRisk;
    }

    public int GetCurrentSinglePeopleKilled()
    {
        return currentSinglePeopleKilled;
    }

    public int GetCurrentMultiplePeopleKilled()
    {
        return currentMultiplePeopleKilled;
    }

    public string GetFamilyMember()
    {
        return yourFamily[GetRandomNumber(0, yourFamily.Count)].ToString();
    }
}
