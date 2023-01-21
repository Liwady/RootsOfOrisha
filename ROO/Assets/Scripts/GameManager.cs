using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfFruit, amountOfEyes;
    public TMP_Text fruitText, eyesText, abilityText;
    public Camera mainCamera;
    public PlayerManager playerManager;
    public Animator animator;
    public List<GameObject> lights;
    public int lightCount,distance2Ani;
    public int currentScene;


    private void Awake()
    {
        currentScene = 0;
        amountOfFruit = 0;
        lightCount = 0;
        playerManager = FindObjectOfType<PlayerManager>();
        //GetComponent<Camera>().GetComponent<CameraScript>().currentLevel=currentScene;
        //abilityText.text = playerManager.currentAbility.ToString();
    }
    private void Update()
    {
        if(currentScene==0)
            StartFireAnimation();
        //abilityText.text = playerManager.currentAbility.ToString();
    }

    private void StartFireAnimation()
    {if (lightCount < lights.Count)
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
