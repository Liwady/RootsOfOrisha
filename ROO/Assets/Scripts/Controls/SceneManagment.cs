
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagment : MonoBehaviour
{
    [SerializeField]
    private GameObject soundon, soundoff, startObject, startScreen, optionsScreen, pauseScreen, settingsObject, controlsObject, creditsObject, continueObject, settingsChild, controlsChild, creditsChild, currentButtonObject, musicObject, sfxObject, brightnessObject, musicSource, SFXSource;
    [SerializeField]
    private List<Sprite> scroller, activeSlider;
    [SerializeField]
    private PostProcessProfile brightness;
    private ColorGrading exp;
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private int old, currentScreen, currentSlider;//0=pause, 1=options, 2=child of options, 3=child of settings

    private SpriteState ss;
    private EventSystem eventSystem;
    private Button sfxButton, musicButton, brightnessButton, settingsButton, currentButton, creditsButton, controlsButton;
    private Slider slider;
    private Vector2 valueS;
    private float time;
    private bool atSlider, start;
    public bool startGame;


    private void Awake()
    {
        startGame = true;
        brightness.TryGetSettings(out exp);
        start = true;
        atSlider = false;
        currentScreen = 0;
        currentSlider = 0;
        time = 0;
    }
    private void Start()
    {
        ss = new SpriteState();
        eventSystem = FindObjectOfType<EventSystem>();
        currentButtonObject = eventSystem.firstSelectedGameObject;
        SetButtons();
        PlayerControlsUI();
    }
    private void Update()
    {
        if (atSlider)
        {
            SliderValue();
        }

    }
    private void PlayerControlsUI()
    {
        playerManager.playerControls.UI.Back.performed += ctx => GoBack();
        playerManager.playerControls.UI.Click.performed += ctx => ClickButton();
        playerManager.playerControls.UI.Navigate.performed += ctx => GetCurrentButton();
        playerManager.playerControls.UI.Navigate.performed += ctx => valueS = ctx.ReadValue<Vector2>();
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

    //BUTTONS
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
    private void ClickButton()
    {
        currentButton.onClick.Invoke();
    }
    private void GetCurrentButton()
    {
        currentButtonObject = eventSystem.currentSelectedGameObject;
        currentButton = currentButtonObject.GetComponent<Button>();
    }

    //NAVIGATION
    public void Unpause()
    {
        if (startGame)
        {
            startGame = false;
            startScreen.SetActive(false); 
        }
        playerManager.DoPause();

    }
    public void GoToStartScreen(bool first)
    {
        startScreen.SetActive(true);
        optionsScreen.SetActive(false);
        pauseScreen.SetActive(false);
        if (!first)
        {
            eventSystem.SetSelectedGameObject(startObject);
            GetCurrentButton();
        }
        currentScreen = 0;
    }
    public void GoToPauseScreen()
    {
        pauseScreen.SetActive(true);
        optionsScreen.SetActive(false);
        eventSystem.SetSelectedGameObject(continueObject);
        GetCurrentButton();
        currentScreen = 0;
    }
    public void GoToOptionsScreen()
    {
        if (startGame)
            startScreen.SetActive(false);
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
        controlsChild.SetActive(false);
        creditsChild.SetActive(false);
        settingsChild.SetActive(true);
        controlsButton.enabled = true;
        creditsButton.enabled = true;
        settingsButton.enabled = true;
        eventSystem.SetSelectedGameObject(settingsObject);
        GetCurrentButton();
        currentScreen = 1;
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
    private void GoBack()
    {
        switch (currentScreen)
        {
            case 0://pausescreen
                Unpause();
                break;
            case 1://optionsscreen
                if (startGame)
                    GoToStartScreen(false);
                else
                    GoToPauseScreen();
                break;
            case 2://child of options screen
                GoToOptionsScreen();
                break;
            case 3:
                if (atSlider)
                    DeactivateSlider();
                GoToSettingsScreen();
                break;
            case 4:
                PlayScene(0);
                break;
        }
    }

    //SLIDERS
    private void SliderValue()
    {
        if (playerManager.playerControls.UI.Navigate.WasPerformedThisFrame() && time > Time.unscaledDeltaTime)
        {
            if (valueS.x > 0)
                old++;
            else if (valueS.x < 0)
                old--;

            if (old <= 0)
                old = 0;
            else if (old >= 10)
                old = 10;

            slider.value = old;
            time = 0;
        }
        else
            time += Time.unscaledDeltaTime;

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
                ss.selectedSprite = activeSlider[0];
                break;
            case 1://sfx
                musicButton.enabled = false;
                sfxButton.enabled = true;
                brightnessButton.enabled = false;
                eventSystem.SetSelectedGameObject(sfxObject);
                ss.selectedSprite = activeSlider[1];
                break;
            case 2://brightness
                musicButton.enabled = false;
                sfxButton.enabled = false;
                brightnessButton.enabled = true;
                eventSystem.SetSelectedGameObject(brightnessObject);
                ss.selectedSprite = activeSlider[2];
                break;
        }
        GetCurrentButton();
        currentButton.spriteState = ss;
        slider = currentButton.GetComponentInChildren<Slider>();
        slider.GetComponentInChildren<Image>().sprite = scroller[0];
        slider.transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = scroller[2];
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
        slider.GetComponentInChildren<Image>().sprite = scroller[1];
        slider.transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = scroller[3];
        slider = null;
        switch (currentSlider)
        {
            case 0: //music
                ss.selectedSprite = activeSlider[3];
                break;
            case 1://sfx
                ss.selectedSprite = activeSlider[4];
                break;
            case 2://brightness
                ss.selectedSprite = activeSlider[5];
                break;
        }
        currentButton.spriteState = ss;
        musicButton.enabled = true;
        sfxButton.enabled = true;
        brightnessButton.enabled = true;
        GoBack();
    }
    public void SetMusicVolume()
    {
        if (slider.value == 0)
            musicSource.GetComponent<AudioSource>().volume = 0;
        else
            musicSource.GetComponent<AudioSource>().volume = (slider.value / 10);
    }
    public void SetSFXVolume()
    {
        if (slider.value == 0)
            SFXSource.GetComponent<AudioSource>().volume = 0;
        else
            SFXSource.GetComponent<AudioSource>().volume = (slider.value / 100);
    }
    public void SetBrightness()//5=1.5 brighness 10=2 brightness 0=1
    {
        float value = slider.value;

        if (slider.value == 0)
            value = 0 / 8f;
        else if (slider.value <= 10 && slider.value > 0)
            value = 0.8f + (slider.value / 10);


        exp.postExposure.value = value;
    }
    public void GoNextScene(bool eshu)
    {
        if (!eshu)
            switch (gameManager.currentScene)
            {
                case 0: //tutorial
                    gameManager.currentScene = 1;
                    PlayScene(2);
                    break;
                case 1://FireLevel
                    gameManager.currentScene = 2;
                    PlayScene(3);
                    break;
                case 2://EarthLevel
                    gameManager.currentScene = 3;
                    PlayScene(4);
                    break;
                case 3://WaterLevel
                    gameManager.currentScene = 4;
                    PlayScene(5);
                    break;
            }
        else
            PlayScene(6);//go to Eshu

    }
}
