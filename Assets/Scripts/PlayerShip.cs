using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour {

    public Sprite[] sprite;
    

    private GameManager gameManager;
    private Image image;

    private void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        image = GetComponent<Image>();
    }

    public void ChangeSprite(bool normal, bool moving)
    {
        if (normal)
            image.sprite = sprite[0];

        if (moving)
        {
            if (gameManager.GetHullPercent() <= 50 && gameManager.GetEnginePercent() <= 50)
                image.sprite = sprite[12];
            else if (gameManager.GetHullPercent() <= 75 && gameManager.GetEnginePercent() <= 75)
                image.sprite = sprite[11];
            else if (gameManager.GetHullPercent() <= 50)
                image.sprite = sprite[14];
            else if (gameManager.GetHullPercent() <= 75)
                image.sprite = sprite[13];
            else if (gameManager.GetEnginePercent() <= 50)
                image.sprite = sprite[16];
            else if (gameManager.GetEnginePercent() <= 75)
                image.sprite = sprite[15];
            else
                image.sprite = sprite[1];
        }
            


        if(!normal && !moving)
        {
            if (gameManager.GetHullPercent() <= 25 && gameManager.GetEnginePercent() <= 25)
                image.sprite = sprite[10];
            else if (gameManager.GetHullPercent() <= 50 && gameManager.GetEnginePercent() <= 50)
                image.sprite = sprite[9];
            else if (gameManager.GetHullPercent() <= 75 && gameManager.GetEnginePercent() <= 75)
                image.sprite = sprite[8];
            else if (gameManager.GetHullPercent() <= 25)
                image.sprite = sprite[7];
            else if (gameManager.GetHullPercent() <= 50)
                image.sprite = sprite[6];
            else if (gameManager.GetHullPercent() <= 75)
                image.sprite = sprite[5];
            else if (gameManager.GetEnginePercent() <= 25)
                image.sprite = sprite[4];
            else if (gameManager.GetEnginePercent() <= 50)
                image.sprite = sprite[3];
            else if (gameManager.GetEnginePercent() <= 75)
                image.sprite = sprite[2];
        }
    }
}
