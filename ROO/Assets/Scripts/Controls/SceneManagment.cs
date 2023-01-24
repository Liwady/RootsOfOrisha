
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagment : MonoBehaviour
{
    public GameObject soundon, soundoff, optionsScreen, pauseScreen, settingsObject, controlsObject, creditsObject, continueObject, settingsChild, controlsChild, creditsChild, currentButtonObject,
        musicObject, sfxObject, brightnessObject, musicSource, SFXSource;
    private EventSystem eventSystem;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    private ColorGrading exp;
    public PlayerManager playerManager;
    private Button sfxButton, musicButton, brightnessButton, settingsButton, currentButton, creditsButton, controlsButton;
    private Slider slider;
    public Vector2 valueS;
    public int old;
    private float time;
    private bool atSlider, start;
    public int currentScreen, currentSlider;//0=pause, 1=options, 2=child of options, 3=child of settings
    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        brightness.TryGetSettings(out exp);
        currentButtonObject = eventSystem.firstSelectedGameObject;
        start = true;
        atSlider = false;
        currentScreen = 0;
        currentSlider = 0;
        SetButtons();
        PlayerControlsUI();
        time = 0;
    }
    private void PlayerControlsUI()
    {
        playerManager.playerControls.UI.Back.performed += ctx => GoBack();
        playerManager.playerControls.UI.Click.performed += ctx => ClickButton();
        playerManager.playerControls.UI.Navigate.performed += ctx => GetCurrentButton();
        playerManager.playerControls.UI.Navigate.performed += ctx => valueS = ctx.ReadValue<Vector2>();
        playerManager.playerControls.UI.Enable();
    }
    private void Update()
    {
        if (atSlider)
        {
            SliderValue();
        }

    }
    private void SliderValue()
    {
        if (playerManager.playerControls.UI.Navigate.WasPerformedThisFrame() && time > Time.unscaledDeltaTime)
        {
            if (valueS.x > 0)
                old++;
            else if (valueS.x < 0)
                old--;
            slider.value = old;
            time = 0;
        }
        else
            time += Time.unscaledDeltaTime;

    }
    private void SetButtons()
    {
        sfxButton = sfxObject.GetComponent<Button>();
        musicButton = musicObject.GetComponent<Button>();
        brightnessButton = brightnessObject.GetComponent<Button>();
        settingsButton = settingsObject.GetComponent<Button>();
        currentButton = currentButtonObject.GetComponent<Button>();
        creditsButton = creditsObject.GetComponent<Button>();
        controlsButton = controlsObject.GetComponent<Button>();
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
        controlsChild.SetActive(false);
        creditsChild.SetActive(false);
        settingsChild.SetActive(false);
        controlsButton.enabled = true;
        creditsButton.enabled = true;
        settingsButton.enabled = true;
        eventSystem.SetSelectedGameObject(settingsObject);
        GetCurrentButton();
        currentScreen = 1;
    }
    private void GoToPauseScreen()
    {
        pauseScreen.SetActive(true);
        optionsScreen.SetActive(false);
        eventSystem.SetSelectedGameObject(continueObject);
        GetCurrentButton();
        currentScreen = 0;
    }
    public void EnableButtonChildren(int button)
    {
        switch (button)
        {
            case 0: //settings
                GoToSettingsScreen();
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
    private void GoToSettingsScreen()
    {
        //change sprite with red line under it for settings 
        controlsChild.SetActive(false);
        creditsChild.SetActive(false);
        settingsChild.SetActive(true);
        controlsButton.enabled = false;
        creditsButton.enabled = false;
        settingsButton.enabled = false;
        if (currentSlider == 1)
            eventSystem.SetSelectedGameObject(sfxObject);
        else if (currentSlider == 2)
            eventSystem.SetSelectedGameObject(brightnessObject);
        else
            eventSystem.SetSelectedGameObject(musicObject);
        GetCurrentButton();
        currentScreen = 2;
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
            case 3:
                GoToSettingsScreen();
                break;

        }
    }
    private void ClickButton()
    {
        currentButton.onClick.Invoke();
    }
    private void GetCurrentButton()
    {
        currentButtonObject = eventSystem.currentSelectedGameObject;
        currentButton = currentButtonObject.GetComponent<Button>();
    }
    public void SliderButton(int button)
    {
        currentSlider = button;
        if (!start)
            DeactivateSlider();
        else
            ActivateSlider(currentSlider);
    }
    private void ActivateSlider(int button)
    {
        switch (button)
        {
            case 0: //music
                musicButton.enabled = true;
                sfxButton.enabled = false;
                brightnessButton.enabled = false;
                eventSystem.SetSelectedGameObject(musicObject);
                break;
            case 1://sfx
                musicButton.enabled = false;
                sfxButton.enabled = true;
                brightnessButton.enabled = false;
                eventSystem.SetSelectedGameObject(sfxObject);
                break;
            case 2://brightness
                musicButton.enabled = false;
                sfxButton.enabled = false;
                brightnessButton.enabled = true;
                eventSystem.SetSelectedGameObject(brightnessObject);
                break;
        }
        GetCurrentButton();
        slider = currentButton.GetComponentInChildren<Slider>();
        slider.enabled = true;
        old = (int)slider.value;
        atSlider = true;
        start = false;
        currentScreen = 3;
    }
    private void DeactivateSlider()
    {
        atSlider = false;
        start = true;
        valueS = Vector2.zero;
        slider.enabled = false;
        slider = null;
        musicButton.enabled = true;
        sfxButton.enabled = true;
        brightnessButton.enabled = true;
        GoBack();
    }
    public void SetMusicVolume()
    {
        musicSource.GetComponent<AudioSource>().volume = (slider.value / 100);
    }
    public void SetSFXVolume()
    {
        SFXSource.GetComponent<AudioSource>().volume = (slider.value / 100);
    }
    public void SetBrightness()//5=1.5 brighness 10=2 brightness 0=1
    {
        
        if (valueS != null)
            exp.postExposure.value = 0.8f+(slider.value/10);
        else
            exp.postExposure.value =  0.8f;
    }
}
