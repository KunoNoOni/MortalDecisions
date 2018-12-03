using UnityEngine;

public class Registry : MonoBehaviour {

    public bool ranOutOfFuel = false;
    public bool spacePiratesDestroyedMe = false;

    private int numberOfPeopleSacrificed;

    public int GetNumberOfPeopleSacrificed()
    {
        return numberOfPeopleSacrificed;
    }

    public void SetNumberOfPeopleSacrificed(int value)
    {
        numberOfPeopleSacrificed = value;
    }

    private void Awake () 
	{
        DontDestroyOnLoad(this);
    }

}
