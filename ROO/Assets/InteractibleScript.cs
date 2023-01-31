using UnityEngine;

public class InteractibleScript : MonoBehaviour
{
    private int previousScene;
    private GameManager gameManager;
    [SerializeField]
    private bool map, cutscenePlayed, clicked;
    [SerializeField]
    private Animator mapAnimator, buttonAnimator;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        previousScene = gameManager.currentScene;
        cutscenePlayed = false;
    }
    private void Update()
    {
        if (cutscenePlayed && AnimatorIsPlaying(mapAnimator, "next") && clicked)
        {
            clicked = false;
            gameManager.GoNextSceneEshu();
        }

    }
    public void Interact()
    {
        if (map)
            MapScene();
        else
            EshuInteraction();

    }
    public void Click()
    {
        clicked = true;
    }

    private void MapScene()
    {
        switch (previousScene)
        {
            case 0: //tutorial show map going from nothing to eshu  
                mapAnimator.SetInteger("level", 0);
                break;
            case 1://show map going from eshu to fire  
                mapAnimator.SetInteger("level", 1);
                break;
            case 2://fire to earth 
                mapAnimator.SetInteger("level", 2);
                break;
        }
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
