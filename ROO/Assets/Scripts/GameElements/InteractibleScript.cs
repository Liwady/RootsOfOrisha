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
        if (cutscenePlayed)
        {
            if (AnimatorIsPlaying(animators[0], "end"))
                SceneManager.LoadScene(1);

            if (AnimatorIsPlaying(animators[button], "end"))
                MapButtonScript.canClick = true;
        }
    }
    public void Interact()
    {
        if (map)
            MapScene();
        else
            EshuInteraction();
    }
    public void MapScene()
    {
        gameManager.StartMap(true);
        if (LevelTracker.level != 0)
        {
            activeElements[0].SetActive(true);
            activeElements[0].GetComponentInParent<Button>().interactable = false;
            for (int i = 1; i < 3; i++) // level 0=intro 1=tutorial 2=shan 3=osh =>img/ani 0=tut 1=shan 2=osu
            {
                if (LevelTracker.completedLevel[i+1])
                {
                    activeElements[i].SetActive(true);
                    activeElements[i].GetComponentInParent<Button>().interactable = false;
                }
                else
                    sceneManager.SetCurrentButton(activeElements[i].GetComponentInParent<Button>().gameObject);
            }
        }
        else
        {
            activeElements[0].SetActive(true);
            TriggerAnimation(0);
        }
    }

    public void TriggerAnimation(int _button)
    {
        activeElements[_button].SetActive(true);
        animators[_button].SetTrigger("start");
        button = _button;
        cutscenePlayed = true;
    }

    private void EshuInteraction()
    {
        gameManager.PlaySound("eshulaugh");
    }

    bool AnimatorIsPlaying(Animator animator, string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

}
