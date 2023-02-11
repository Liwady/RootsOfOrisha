// Import necessary libraries
using UnityEngine;
using UnityEngine.SceneManagement;

// Define GameManager class
public class GameManager : MonoBehaviour
{
    // Define private fields that can be adjusted in the Unity Inspector
    [SerializeField]
    private int distance2Ani;
    [SerializeField]
    private int ability, character, together;
    // Define public fields accessible from other classes
    public bool eyeColl;
    public int currentScene, amountOfFruit, lastScene;

    // Define variables to reference instances of other classes
    public PlayerManager playerManager;
    public SceneManagment sceneManagment;
    public AnimationManager animationManager;
    public AudioManager audioManager;
    public EshuConvoScript eshuConvo;

    // Define game objects to reference in the Unity Inspector
    public GameObject pauseMenu, overlay, cutscene, map;
    public CanvasGroup cg;

    // Define additional variables to manage game state
    public bool isPaused, tutorial;
    private float previousTimeScale;

    // Called once when the script is loaded
    private void Start()
    {
        // Initialize currentScene variable
        SetCurrentScene();
    }

    // Called once per frame
    private void Update()
    {
        // If the current scene is 1 or the build index of the active scene is 5 and the game is not paused
        if (currentScene == 1 || SceneManager.GetActiveScene().buildIndex == 5 && !isPaused)
        {
            // If character 1 is moving left
            if (playerManager.character1script.left)
                // Start the fire animation at character 2's position
                animationManager.StartFireAnimation(playerManager.character2script.transform.position, distance2Ani);
            else
                // Otherwise, start the fire animation at character 1's position
                animationManager.StartFireAnimation(playerManager.character1script.transform.position, distance2Ani);
        }
    }
    //set current scene
    private void SetCurrentScene()
    {
        //Get the current and last scene indexes from the scene manager
        currentScene = sceneManagment.currentScene;
        lastScene = sceneManagment.lastScene;

        //If the current scene is the tutorial
        if (currentScene == 1)
        {
            //Disable all controls besides walking
            Time.timeScale = 0;
            cg.alpha = 0;
            sceneManagment.startGame = true;
            sceneManagment.GoToStartScreen(true);
            isPaused = true;
            playerManager.EnableEshuControls(true); //enable UI controls 
            //Trigger tutorial sequence
            TutorialTriggers(0);
        }
        //If the current scene is the level selection screen
        else if (currentScene == 5)
        {
            //If the last scene was the tutorial screen, start the cutscene
            if (sceneManagment.lastScene == 1)
                StartCutscene();
            else
                playerManager.EnableEshuControls(false);
        }
        //If the current scene is a level
        else if (currentScene == 2 || currentScene == 3)
            playerManager.EnablePlayerControls(false);
    }


    //Display the world map and enable Eshu's controls
    public void StartMap(bool _start)
    {
        if (_start)
        {
            if (currentScene != 0)
                playerManager.EnableEshuControls(true);
            map.SetActive(true);
        }
        else
        {
            playerManager.EnableEshuControls(false);
            map.SetActive(false);
        }
    }

    //End the game by playing the final scene
    public void EndGame()
    {
        sceneManagment.PlayScene(4);
    }

    // Start the cutscene by enabling player controls and activating the cutscene object and Eshu's conversation script
    public void StartCutscene()
    {
        playerManager.EnableEshuControls(true);
        cutscene.SetActive(true);
        eshuConvo.isActive = true;
    }

    // End the cutscene by disabling player controls and deactivating the cutscene object
    public void EndCutscene()
    {
        playerManager.EnableEshuControls(false);
        cutscene.SetActive(false);
        sceneManagment.cutscene = false;
    }

    // Switch to the Eshu scene
    public void GoToEshu()
    {
        sceneManagment.PlayScene(5);
    }

    // Pause the game by setting timescale to 0 and displaying the pause menu
    public void Pause()
    {
        if (currentScene == 1)
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = previousTimeScale;
                pauseMenu.SetActive(false);
                cg.alpha = 1;
                isPaused = false;
            }
            else if (Time.timeScale > 0)
            {
                previousTimeScale = Time.timeScale;
                Time.timeScale = 0;
                if (cg != null)
                    cg.alpha = 0;
                sceneManagment.GoToPauseScreen();
                isPaused = true;
            }
            playerManager.EnablePlayerControls(isPaused);
        }
    }

    // Start the game by setting timescale to 1 and hiding the pause menu
    public void StartGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        overlay.SetActive(true);
        cg.alpha = 1;
        isPaused = false;
        playerManager.EnablePlayerControls(isPaused);
    }

    // Set the walking animation and sprite based on movement
    public void SetWalking(bool isWalking)
    {
        animationManager.WalkingState(isWalking, playerManager.moveBoth, playerManager.currentCharacter == playerManager.character1script);
    }

    // Update the eye sprite based on whether the player is colliding with a trigger
    public void UpdateEye(bool col)
    {
        eyeColl = col;
        animationManager.ChangeEyeSprite(eyeColl);
    }

    // Update the fruit sprite based on the number of fruit collected
    public void UpdateFruit(int fruit)
    {
        amountOfFruit = fruit;
        animationManager.ChangeFruitSprite(amountOfFruit);
    }

    // Update the ability or character mechanics sprite based on the player's selection
    public void UpdateMechanics(int option, bool reset)
    {
        if (overlay != null)
        {
            if (overlay.activeInHierarchy)
                animationManager.ChangeMechanicsSprite(option, playerManager.currentAbility == 0, reset);
        }
    }

    // Update the connection sprite based on the character and movement status
    public void UpdateConnection()
    {
        animationManager.SwapConnectionSprite(playerManager.currentCharacter == playerManager.character1script, playerManager.moveBoth);
    }

    // Set tutorial triggers and provide feedback based on the current stage
    public void TutorialTriggers(int stage)
    {
        playerManager.SetTutorialControls(stage, false);
        animationManager.TutorialFeedbackTrigger(stage);
    }

    // Play a sound effect
    public void PlaySound(string _name)
    {
        audioManager.PlaySound(_name);
    }

    // Play a sound effect with a specified ID
    public void PlaySound(string _name, int _num)
    {
        audioManager.PlaySound(_name, _num);
    }

    // Stop a sound effect
    public void StopSound(string _name)
    {
        audioManager.StopSound(_name);
    }

    // Stop a sound effect with a specified ID
    public void StopSound(string _name, int _num)
    {
        audioManager.StopSound(_name, _num);
    }

    // Go back to the previous scene
    public void GoBack()
    {
        sceneManagment.GoBack();
    }

    // Simulate clicking a button
    public void ClickButton()
    {
        sceneManagment.ClickButton();
    }

    // Get current button
    public void GetCurrentButton()
    {
        sceneManagment.GetCurrentButton();
    }

    // Set the value of the slider by updating the movedSlider and valueS variables in the scene management
    public void SetValue(Vector2 value)
    {
        sceneManagment.movedSlider = true;
        sceneManagment.valueS = value;
    }
}
