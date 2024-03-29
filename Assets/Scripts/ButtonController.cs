﻿//Leah Zahn
//ID: 2341427
//zahn @chapman.edu
//CPSC236-03
//Avoider
//This is my own work, and I did not cheat on this assignment.

/*
 * ButtonController.cs
 * This class controls the buttons and text elements in the menu and during 
 * the game so that the appropriate UI elements are shown at the correct times.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject player;
    public GameObject gameQuitButton;
    public GameObject keyText;
    public GameObject playButton;
    public GameObject menuQuitButton;
    public GameObject menuBackground;
    public GameObject titleText;
    public GameObject winText;

    void Start()
    {
        titleText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        menuQuitButton.gameObject.SetActive(true);
        menuBackground.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        gameQuitButton.gameObject.SetActive(false);
        keyText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
    }

    public void OnPlayButtonClick()
    {
        titleText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        menuQuitButton.gameObject.SetActive(false);
        menuBackground.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        gameQuitButton.gameObject.SetActive(true);
        keyText.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);
    }

    public void OnMenuQuitButtonClick()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void OnGameQuitButtonClick()
    {
        titleText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        menuQuitButton.gameObject.SetActive(true);
        menuBackground.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        gameQuitButton.gameObject.SetActive(false);
        keyText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
    }

    public void OnGameWin()
    {
        titleText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        menuQuitButton.gameObject.SetActive(true);
        menuBackground.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        gameQuitButton.gameObject.SetActive(false);
        keyText.gameObject.SetActive(false);
        winText.gameObject.SetActive(true);
    }
}
