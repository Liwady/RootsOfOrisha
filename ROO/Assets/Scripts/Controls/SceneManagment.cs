using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagment : MonoBehaviour
{
    public GameObject soundon, soundoff, optionsScreen, pauseScreen, settingsButton, continueButton ,settingsChild, controlsChild, creditsChild, currentButton;
    private EventSystem eventSystem;
    public PlayerManager playerManager;
    public int currentScreen;//0=pause, 1=options, 2=child of options
    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        currentButton = eventSystem.firstSelectedGameObject;
        playerManager.playerControls.UI.Back.performed += ctx => GoBack();
        playerManager.playerControls.UI.Click.performed += ctx => ClickButton();
        playerManager.playerControls.UI.Navigate.performed += ctx => GetCurrentButton();
        currentScreen = 0;
    }
    public void PlayScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void SwitchSound()
    {
        if (soundon.activeInHierarchy)
        {
            soundon.SetActive(false);
            soundoff.SetActive(true);
        }
        else
        {
            soundon.SetActive(true);
            soundoff.SetActive(false);
        }
    }
    public void GoToOptionsScreen()
    {
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(settingsButton);
        currentScreen = 1;
    }
    private void GoToPauseScreen()
    {
        pauseScreen.SetActive(true);
        optionsScreen.SetActive(false);
        eventSystem.SetSelectedGameObject(continueButton);
        currentScreen = 0;
    }
    public void EnableButtonChildren(int button)
    {
        switch (button)
        {
            case 0: //settings
                controlsChild.SetActive(false);
                settingsChild.SetActive(true);
                creditsChild.SetActive(false);
                currentScreen = 2;
                break;
            case 1://controls
                controlsChild.SetActive(true);
                settingsChild.SetActive(false);
                creditsChild.SetActive(false);
                currentScreen = 1;
                break;
            case 2://credits
                controlsChild.SetActive(false);
                settingsChild.SetActive(false);
                creditsChild.SetActive(true);
                currentScreen = 1;
                break;
        }
        

    }
    public void Unpause()
    {
        playerManager.DoPause();
    }
    private void GoBack()
    {
        switch (currentScreen)
        {
            case 0://pausescreen
                Unpause();
                break;
            case 1://optionsscreen
                GoToPauseScreen();
                break;
            case 2://child of options screen
                GoToOptionsScreen();
                break;
        }
    }
    private void ClickButton()
    {
        currentButton.GetComponent<Button>().onClick.Invoke();
    }
    private void GetCurrentButton()
    {
        currentButton = eventSystem.currentSelectedGameObject;
    }

}
