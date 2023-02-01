using UnityEngine;
using UnityEngine.UI;

public class InteractibleScript : MonoBehaviour
{
    private int previousScene;
    private GameManager gameManager;
    [SerializeField]
    private bool map, cutscenePlayed, clicked;
    [SerializeField]
    private GameObject eshuActive, shanActive, ossainActive;
    [SerializeField]
    private Animator[] animators;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        cutscenePlayed = false;
    }
    private void Update()
    {
        if (cutscenePlayed && AnimatorIsPlaying(animators[previousScene], "end"))
            gameManager.GoNextSceneEshu();
    }
    public void Interact()
    {
        if (map)
            MapScene();
        else
            EshuInteraction();
    }

    private void MapScene()
    {
        previousScene = gameManager.lastScene;
        gameManager.StartMap(true);
        switch (previousScene)
        {
            case 0: //tutorial show map going from nothing to eshu  
                eshuActive.SetActive(true);
                eshuActive.GetComponentInChildren<Image>().color = Color.Lerp(Color.clear, Color.white, 2);
                break;
            case 1://show map going from eshu to fire  
                shanActive.SetActive(true);
                eshuActive.SetActive(true);
                shanActive.GetComponentInChildren<Image>().color = Color.Lerp(Color.clear, Color.white, 2);
                break;
            case 2://fire to earth 
                eshuActive.SetActive(true);
                shanActive.SetActive(true);
                ossainActive.SetActive(true);
                ossainActive.GetComponentInChildren<Image>().color = Color.Lerp(Color.clear, Color.white, 2);
                break;
        }
        animators[previousScene].SetTrigger("start");
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
