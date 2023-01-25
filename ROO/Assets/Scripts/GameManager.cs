using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfFruit, amountOfEyes, lightCount, distance2Ani, currentScene;
    public TMP_Text fruitText, eyesText, abilityText;
    public Camera mainCamera;
    public PlayerManager playerManager;
    public SceneManagment sceneManagment;
    public Animator animator;
    public List<GameObject> lights;
    private float previousTimeScale;
    public GameObject pauseMenu;
    public bool isPaused;



    private void Awake()
    {
        currentScene = 0;
        amountOfFruit = 0;
        lightCount = 0;
        playerManager = FindObjectOfType<PlayerManager>();
        setCurrentScene();
        //abilityText.text = playerManager.currentAbility.ToString();
    }
    private void Update()
    {
        if (currentScene == 0)
            StartFireAnimation();
        //abilityText.text = playerManager.currentAbility.ToString();
    }
    private void setCurrentScene()
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
    private void StartFireAnimation()
    {
        if (lightCount < lights.Count)
        {
            //Debug.Log(Vector3.Distance(playerManager.currentCharacter.transform.position, lights[lightCount].transform.position));
            if (Vector3.Distance(playerManager.currentCharacter.transform.position, lights[lightCount].transform.position) < distance2Ani)
            {
                animator = lights[lightCount].GetComponentInChildren<Animator>();
                animator.SetTrigger("Start");
                lightCount++;
            }
        }
    }
}
