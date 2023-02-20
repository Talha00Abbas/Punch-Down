using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{

    public GameObject[] players;
    public int currentPlayers;
    public bool inGameplayScene = false;
    public GameObject selectUI;

    void Start()
    {
        Time.timeScale = 0;
        int selectedCar = 0;

        if (inGameplayScene == true)
        {
            players[selectedCar].SetActive(true);
            currentPlayers = selectedCar;
        }
        selectUI.SetActive(true);

    }


    public void Right()
    {
        
        if (currentPlayers < players.Length - 1)
        {
            currentPlayers += 1;
            for (int i = 0; i < players.Length; i++)
            {
                if (currentPlayers < players.Length)
                {
                    players[i].gameObject.SetActive(false);
                    players[currentPlayers].SetActive(true);
                    
                }
            }
        }
    }


    public void Left()
    {
        if (currentPlayers > 0)
        {
            currentPlayers -= 1;
            for (int i = 0; i < players.Length; i++)
            {
                if (currentPlayers < players.Length)
                {
                    players[i].gameObject.SetActive(false);
                    players[currentPlayers].SetActive(true);
                }
            }
        }
    }

    public void Select()
    {
        Time.timeScale = 1;
        selectUI.SetActive(false);
    }
}
