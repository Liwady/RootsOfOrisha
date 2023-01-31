using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int distance2Ani;
    [SerializeField]
    private int ability, character, together;
    public bool eyeColl;
    public int currentScene, amountOfFruit;

    public PlayerManager playerManager;
    public SceneManagment sceneManagment;
    public AnimationManager animationManager;
    public AudioManager audioManager;


    public GameObject pauseMenu,overlay,cutscene;
    public CanvasGroup cg;
    public bool isPaused, tutorial;
    private float previousTimeScale;


    private void Start()
    {
        SetCurrentScene();
    }
    private void Update()
    {
        if (currentScene == 0 && !isPaused)
            animationManager.StartFireAnimation(playerManager.currentCharacter.transform.position, distance2Ani);
    }
    public void StartCutscene()
    {
        playerManager.EnablePlayerControls(true);
        cutscene.SetActive(true);
    }
    private void SetCurrentScene()
    {
        if (currentScene == 0)
            tutorial = true;
        if (tutorial)
        {
            //disable all controls besides walking
            Time.timeScale = 0;
            cg.alpha = 0;
            sceneManagment.startGame = true;
            sceneManagment.GoToStartScreen(true);
            isPaused = true;
            playerManager.EnablePlayerControls(isPaused);
            TutorialTriggers(0);
        }
        else if (currentScene == 6)
            StartCutscene();
        sceneManagment.currentScene = currentScene;

    }
    public void EndTutorial()
    {
        playerManager.EnablePlayerControls(isPaused);
    }
    public void Pause()
    {
        if (Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            cg.alpha = 0;
            sceneManagment.GoToPauseScreen();
            isPaused = true;

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;
            pauseMenu.SetActive(false);
            cg.alpha = 1;
            isPaused = false;
        }
        playerManager.EnablePlayerControls(isPaused);
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        overlay.SetActive(true);
        cg.alpha = 1;
        isPaused = false;
        playerManager.EnablePlayerControls(isPaused);
        playerManager.SetTutorialControls(0);
    }
    //set values so the sprite manager can use them and we can keep the info in the game manager. 
    public void UpdateEye(bool col)
    {
        eyeColl = col;
        animationManager.ChangeEyeSprite(eyeColl);
    }
    public void UpdateFruit(int fruit)
    {
        amountOfFruit = fruit;
        animationManager.ChangeFruitSprite(amountOfFruit);
    }
    // option 0 = switch ability, option 1 = switch character, option 2 = trigger ability
    public void UpdateMechanics(int option, bool reset)
    {
        animationManager.ChangeMechanicsSprite(option, playerManager.currentAbility == 0, reset);
    }
    public void UpdateConnection(int num)
    {
        animationManager.SwapConnectionSprite(num);
    }
    public void TutorialTriggers(int stage)
    {
        playerManager.SetTutorialControls(stage);
        animationManager.TutorialFeedbackTrigger(stage);
    }
    public void PlaySound(string _name)
    {
        audioManager.PlaySound(_name);
    }
    public void PlaySound(string _name, int _num)
    {
        audioManager.PlaySound(_name, _num);
    }

    public void StopSound(string _name, int _num)
    {
        audioManager.StopSound(_name, _num);
    }
    public void GoBack()
    {
        sceneManagment.GoBack();
    }
    public void ClickButton()
    {
        sceneManagment.ClickButton();
    }
    public void GetCurrentButton()
    {
        sceneManagment.GetCurrentButton();
    }
    public void SetValue(Vector2 value)
    {
        sceneManagment.movedSlider = true;
        sceneManagment.valueS = value;
    }
}
