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

    void Start()
    {
        titleText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        menuQuitButton.gameObject.SetActive(true);
        menuBackground.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        gameQuitButton.gameObject.SetActive(false);
        keyText.gameObject.SetActive(false);
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
    }
}
