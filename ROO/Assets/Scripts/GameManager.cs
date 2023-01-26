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
    public bool isPaused;
    private float previousTimeScale;

    private void Awake()
    {
        amountOfFruit = 0;
        SetCurrentScene();
    }
    private void Update()
    {
        if (currentScene == 0)
            animationManager.StartFireAnimation(playerManager.currentCharacter.transform.position, distance2Ani);
        //abilityText.text = playerManager.currentAbility.ToString();
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
            sceneManagment.GoToPauseScreen();
            sceneManagment.startGame = false;
            isPaused = true;

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;
            pauseMenu.SetActive(false);
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
    public void UpdateMechanics(int ability, int character, bool together)
    {
        //set
        //spriteManager.ChangeMechanicsSprite()
        //todo
    }
}
