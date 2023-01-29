using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int distance2Ani;
    public int amountOfFruit, ability, character, together;
    public bool eyeColl;
    public int currentScene;

    public PlayerManager playerManager;
    public SceneManagment sceneManagment;
    public AnimationManager animationManager;

    public GameObject pauseMenu;
    public CanvasGroup cg;
    public bool isPaused,tutorial;
    private float previousTimeScale;

    private void Awake()
    {
        amountOfFruit = 0;
        SetCurrentScene();
        if(currentScene == 0)
            tutorial = true;
        
    }
    private void Update()
    {
        if (currentScene == 0 && !isPaused)
            animationManager.StartFireAnimation(playerManager.currentCharacter.transform.position, distance2Ani);
    }
    private void SetCurrentScene()
    {
        //currentScene= sceneManagment.currentScene;
        //GetComponent<Camera>().GetComponent<CameraScript>().currentLevel = currentScene;
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
    }
    public void StartGame()
    {
        if (Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            cg.alpha = 0;
            sceneManagment.GoToStartScreen(true);
            isPaused = true;

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;
            pauseMenu.SetActive(false);
            cg.alpha = 1;
            isPaused = false;
        }
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
    public void UpdateMechanicsTutorial(int n)
    {
        animationManager.TutorialMechanicsTrigger(n);
    }
    public void UpdateConnection(int num)
    {
        animationManager.SwapConnectionSprite(num);
    }

}
