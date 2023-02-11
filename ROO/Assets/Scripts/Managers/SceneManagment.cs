
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagment : MonoBehaviour
{
    [SerializeField]
    private GameObject soundon, soundoff, startObject, startScreen, optionsScreen, pauseScreen,
        settingsObject, controlsObject, creditsObject, continueObject,
        settingsChild, controlsChild, creditsChild,
        currentButtonObject, musicObject, sfxObject, brightnessObject,
        musicSource, SFXSource;
    [SerializeField]
    private List<Sprite> scroller, activeSlider;
    [SerializeField]
    private PostProcessProfile brightness;
    private ColorGrading exp;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private int old, currentScreen, currentSlider;//0=pause, 1=options, 2=child of options, 3=child of settings

    [SerializeField]
    private AudioMixer SFXMasterMixer;

    public int lastScene, currentScene;
    private SpriteState ss;
    private EventSystem eventSystem;
    private Button sfxButton, musicButton, brightnessButton, settingsButton, currentButton, creditsButton, controlsButton;
    private Slider slider;
    public Vector2 valueS;
    private float time;
    private bool atSlider, start;
    public bool startGame, movedSlider, cutscene;

    // Awake function is called before the Start function
    private void Awake()
    {
        brightness.TryGetSettings(out exp);
        start = true;
        atSlider = false;
        currentScreen = 0;
        currentSlider = 0;
        movedSlider = false;
        time = 0;
        lastScene = LevelTracker.level; //last scene that is not eshu
        if (SceneManager.GetActiveScene().buildIndex != 5) // 
            LevelTracker.level = SceneManager.GetActiveScene().buildIndex;
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Start function is called after the Awake function
    private void Start()
    {
        ss = new SpriteState();
        eventSystem = FindObjectOfType<EventSystem>();
        currentButtonObject = eventSystem.firstSelectedGameObject;
        SetButtons();
    }

    // Update function is called every frame
    private void Update()
    {
        if (atSlider)
            SliderValue();
    }

    // PlayScene function loads the scene with the given scene number
    public void PlayScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    // Quit function closes the application
    public void Quit()
    {
        Application.Quit();
    }
    // SwitchSound function switches the sound on/off
    public void SwitchSound()
    {
        //Toggles the sound on or off based on the current state of the sound on/off buttons
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
        //Gets the component of type "Button" for each button object
        sfxButton = sfxObject.GetComponent<Button>();
        musicButton = musicObject.GetComponent<Button>();
        brightnessButton = brightnessObject.GetComponent<Button>();
        settingsButton = settingsObject.GetComponent<Button>();
        currentButton = currentButtonObject.GetComponent<Button>();
        creditsButton = creditsObject.GetComponent<Button>();
        controlsButton = controlsObject.GetComponent<Button>();
    }
    public void ClickButton()
    {
        //Invokes the onClick event of the current button
        currentButton.onClick.Invoke();
    }
    public void GetCurrentButton()
    {
        //Sets the current button based on the currently selected game object in the event system
        if (!cutscene)
        {
            currentButtonObject = eventSystem.currentSelectedGameObject;
            currentButton = currentButtonObject.GetComponent<Button>();
        }
    }

    // Sets the currently selected button in the event system, which is then stored for future reference
    public void SetCurrentButton(GameObject button)
    {
        eventSystem.SetSelectedGameObject(button);
        currentButtonObject = eventSystem.currentSelectedGameObject;
        currentButton = currentButtonObject.GetComponent<Button>();
    }

    // Unpauses the game if it's already started, otherwise pauses it
    public void Unpause()
    {
        if (startGame)
        {
            startGame = false;
            startScreen.SetActive(false);
            gameManager.StartGame();
        }
        else
            gameManager.Pause();
    }

    // Goes to the start screen, optionally resetting the current selected button to the default
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

    // Goes to the pause screen, setting the continue button as the current selected button
    public void GoToPauseScreen()
    {
        pauseScreen.SetActive(true);
        optionsScreen.SetActive(false);
        eventSystem.SetSelectedGameObject(continueObject);
        GetCurrentButton();
        currentScreen = 0;
    }

    //Function to go to the options screen
    public void GoToOptionsScreen()
    {
        //Turn off start screen if game has started
        if (startGame)
            startScreen.SetActive(false);
        //Turn off pause screen
        pauseScreen.SetActive(false);

        //Turn on options screen
        optionsScreen.SetActive(true);

        //Turn off controls and credits child screens
        controlsChild.SetActive(false);
        creditsChild.SetActive(false);

        //Turn on settings child screen
        settingsChild.SetActive(true);

        //Set controls, credits, and settings buttons to be enabled
        controlsButton.enabled = true;
        creditsButton.enabled = true;
        settingsButton.enabled = true;

        //Set the current selected game object to the settings button
        eventSystem.SetSelectedGameObject(settingsObject);

        //Get the current selected button
        GetCurrentButton();

        //Set the current screen to 1
        currentScreen = 1;
    }

    //Function to go to the settings screen
    private void GoToSettingsScreen()
    {
        //Turn off controls and credits child screens
        controlsChild.SetActive(false);
        creditsChild.SetActive(false);
        //Turn on settings child screen
        settingsChild.SetActive(true);

        //Set controls, credits, and settings buttons to be disabled
        controlsButton.enabled = false;
        creditsButton.enabled = false;
        settingsButton.enabled = false;

        //Set the current selected game object based on the current slider value
        if (currentSlider == 1)
            eventSystem.SetSelectedGameObject(sfxObject);
        else if (currentSlider == 2)
            eventSystem.SetSelectedGameObject(brightnessObject);
        else
            eventSystem.SetSelectedGameObject(musicObject);

        //Get the current selected button
        GetCurrentButton();

        //Set the current screen to 2
        currentScreen = 2;
    }

    //Function to enable the button children
    public void EnableButtonChildren(int button)
    {
        // Switch statement to determine which button was pressed
        switch (button)
        {
            case 0: // Settings button was pressed
                GoToSettingsScreen(); // Call function to go to settings screen
                break;
            case 1: // Controls button was pressed
                    // Set relevant children active and other children inactive
                controlsChild.SetActive(true);
                settingsChild.SetActive(false);
                creditsChild.SetActive(false);
                currentScreen = 1; // Set current screen to controls screen
                break;
            case 2: // Credits button was pressed
                    // Set relevant children active and other children inactive
                controlsChild.SetActive(false);
                settingsChild.SetActive(false);
                creditsChild.SetActive(true);
                currentScreen = 1; // Set current screen to credits screen
                break;
        }
    }


    //Function to go back to the previous screen
    public void GoBack()
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
    //Function to update the value of the slider
    private void SliderValue()
    {
        //Check if the slider has been moved and enough time has passed
        if (movedSlider && time > Time.unscaledDeltaTime)
        {
            //Increase or decrease the old value depending on the direction of the movement
            if (valueS.x > 0)
                old++;
            else if (valueS.x < 0)
                old--;

            //Ensure that the value remains within the acceptable range
            if (old <= 0)
                old = 0;
            else if (old >= 10)
                old = 10;

            //Update the value of the slider
            slider.value = old;

            //Reset the time and set the movedSlider flag to false
            time = 0;
            movedSlider = false;
        }
        else
            //Increment the time
            time += Time.unscaledDeltaTime;

    }

    // method to handle the button interaction with the slider
    public void SliderButton(int button)
    {
        // storing the current button number that was interacted with
        currentSlider = button;

        // if the game has not started
        if (!start)
        {
            // deactivate the slider
            DeactivateSlider();
        }
        // if the game has started
        else
        {
            // activate the slider with the current button number
            ActivateSlider(currentSlider);
        }
    }


    private void ActivateSlider(int button)
    {
        // Switch based on the button passed as argument
        switch (button)
        {
            case 0: // case for music button
                    // enable music button and disable sfx and brightness buttons
                musicButton.enabled = true;
                sfxButton.enabled = false;
                brightnessButton.enabled = false;
                // set the selected game object to the music object
                eventSystem.SetSelectedGameObject(musicObject);
                // set the sprite state of the selected slider to the active sprite
                ss.selectedSprite = activeSlider[0];
                break;
            case 1: // case for sfx button
                    // enable sfx button and disable music and brightness buttons
                musicButton.enabled = false;
                sfxButton.enabled = true;
                brightnessButton.enabled = false;
                // set the selected game object to the sfx object
                eventSystem.SetSelectedGameObject(sfxObject);
                // set the sprite state of the selected slider to the active sprite
                ss.selectedSprite = activeSlider[1];
                break;
            case 2: // case for brightness button
                    // enable brightness button and disable music and sfx buttons
                musicButton.enabled = false;
                sfxButton.enabled = false;
                brightnessButton.enabled = true;
                // set the selected game object to the brightness object
                eventSystem.SetSelectedGameObject(brightnessObject);
                // set the sprite state of the selected slider to the active sprite
                ss.selectedSprite = activeSlider[2];
                break;
        }

        // Get the current button based on the selected slider
        GetCurrentButton();
        // set the sprite state of the current button
        currentButton.spriteState = ss;
        // Get the slider component from the current button
        slider = currentButton.GetComponentInChildren<Slider>();
        // set the sprite of the slider handle
        slider.GetComponentInChildren<Image>().sprite = scroller[0];
        // set the sprite of the slider fill
        slider.transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = scroller[2];
        // enable the slider
        slider.enabled = true;
        // store the old value of the slider
        old = (int)slider.value;
        // set the atSlider flag to true
        atSlider = true;
        // set the start flag to false
        start = false;
        // set the current screen to 3
        currentScreen = 3;
    }

    private void DeactivateSlider()
    {
        // Set the "atSlider" flag to false
        atSlider = false;

        // Set the "start" flag to true
        start = true;

        // Set the "valueS" vector to (0, 0)
        valueS = Vector2.zero;

        // Disable the slider
        slider.enabled = false;

        // Change the sprite of the slider's track and handle
        slider.GetComponentInChildren<Image>().sprite = scroller[1];
        slider.transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = scroller[3];

        // Reset the "slider" variable to null
        slider = null;

        // Change the sprite of the current button based on its type
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

        // Apply the new sprite state to the current button
        currentButton.spriteState = ss;

        // Enable the music, sfx, and brightness buttons
        musicButton.enabled = true;
        sfxButton.enabled = true;
        brightnessButton.enabled = true;

        // Call the "GoBack" function to return to the previous screen
        GoBack();
    }


    // This function is used to set the volume of the music based on the value of the slider.
    public void SetMusicVolume()
    {
        // If the value of the slider is 0, set the volume of the music to 0
        if (slider.value == 0)
        {
            musicSource.GetComponent<AudioSource>().volume = 0;
        }
        // Else, set the volume of the music to (slider value / 10)
        else
        {
            musicSource.GetComponent<AudioSource>().volume = (slider.value / 10);
        }
    }


    //The function "SetSFXVolume" sets the volume of the sound effects in the game.
    public void SetSFXVolume()
    {
        //This comment states that the code within this function is a temporary fix and needs to be overhauled.

        //The switch statement checks the value of the slider.
        switch (slider.value)
        {
            //In each case, the volume of the sound effects is set using the "SFXMasterMixer" object and the "SetFloat" method.
            //The first argument is the name of the float parameter in the audio mixer.
            //The second argument is the value to set the parameter to.
            case 0:
                SFXMasterMixer.SetFloat("MasterVolSFX", -80);
                break;
            case 1:
                SFXMasterMixer.SetFloat("MasterVolSFX", -20);
                break;
            case 2:
                SFXMasterMixer.SetFloat("MasterVolSFX", -15);
                break;
            case 3:
                SFXMasterMixer.SetFloat("MasterVolSFX", -10);
                break;
            case 4:
                SFXMasterMixer.SetFloat("MasterVolSFX", -5);
                break;
            case 5:
                SFXMasterMixer.SetFloat("MasterVolSFX", 0);
                break;
            case 6:
                SFXMasterMixer.SetFloat("MasterVolSFX", 5);
                break;
            case 7:
                SFXMasterMixer.SetFloat("MasterVolSFX", 10);
                break;
            case 8:
                SFXMasterMixer.SetFloat("MasterVolSFX", 15);
                break;
            case 9:
                SFXMasterMixer.SetFloat("MasterVolSFX", 20);
                break;
            case 10:
                SFXMasterMixer.SetFloat("MasterVolSFX", 25);
                break;
            default:
                break;
        }
    }

    //The function "SetBrightness" changes the brightness of the scene by adjusting the post-exposure value of the camera's exposure component.
    public void SetBrightness()
    {
        float value = slider.value;

        //If the value of the slider is 0, the brightness is set to 0/8
        if (slider.value == 0)
            value = 0 / 8f;
        //If the value of the slider is between 0 and 10, the brightness is set to 0.8 + (slider.value/10)
        else if (slider.value <= 10 && slider.value > 0)
            value = 0.8f + (slider.value / 10);

        //Set the post-exposure value to the calculated value
        exp.postExposure.value = value;
    }

}
