using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractibleScript : MonoBehaviour
{

    private GameManager gameManager;
    private SceneManagment sceneManager;
    [SerializeField]
    private bool map, cutscenePlayed, clicked, introNext;
    [SerializeField]
    private GameObject[] activeElements;
    [SerializeField]
    private Animator[] animators;
    private int button;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        sceneManager = gameManager.sceneManagment;
        cutscenePlayed = false;
    }
    private void Update()
    {
        if (cutscenePlayed) //if the animation started playing after clicking a level (or automatic when tutorial)
        {
            if (AnimatorIsPlaying(animators[0], "end")) //load scene1 after the tutorial ani is done
                SceneManager.LoadScene(1);

            if (AnimatorIsPlaying(animators[button], "end"))//can click button after flashing animation, activates button to go to next scene 
                MapButtonScript.canClick = true;
        }
    }
    public void Interact() // two types of interaction one for the map one for eshu
    {
        if (map)
            MapScene();
        else
            EshuInteraction();
    }
    public void MapScene()
    {
        gameManager.StartMap(true); // set map active + controls if tutorial for the cutscene
        if (LevelTracker.level != 0)// if not tutorial
        {
            activeElements[0].SetActive(true);
            activeElements[0].GetComponentInParent<Button>().interactable = false;
            for (int i = 1; i < 3; i++) // level 0=intro 1=tutorial 2=shan 3=osh =>img/ani 0=tut 1=shan 2=osu
            {
                if (LevelTracker.completedLevel[i+1]) // If level completed set the list of disabled elements to active and get rid of the button
                {
                    activeElements[i].SetActive(true);
                    activeElements[i].GetComponentInParent<Button>().interactable = false;
                }
                else
                    sceneManager.SetCurrentButton(activeElements[i].GetComponentInParent<Button>().gameObject); // else set current button to be the not played level
            }
        }
        else //if tutorial
        {
            activeElements[0].SetActive(true);
            TriggerAnimation(0);
        }
        if (LevelTracker.completedLevel[1] && LevelTracker.completedLevel[2] && LevelTracker.completedLevel[3])
            gameManager.EndGame();
    }

    public void TriggerAnimation(int _button) // play the animations for the map and keep button for end condition
    {
        activeElements[_button].SetActive(true);
        animators[_button].SetTrigger("start");
        button = _button;
        cutscenePlayed = true;
    }

    private void EshuInteraction()
    {
        gameManager.PlaySound("eshulaugh"); // he laughs 
    }

    bool AnimatorIsPlaying(Animator animator, string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name); // check if the animator is playing the state with the given name
    }

}
