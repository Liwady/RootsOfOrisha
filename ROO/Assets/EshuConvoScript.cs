using UnityEngine;


public class EshuConvoScript : MonoBehaviour
{
    private GameManager gameManager;
    public bool clicked, isActive, playSound;
    [SerializeField]
    private int text;
    [SerializeField]
    private string[] states;
    [SerializeField]
    private Animator boxAnimator, textAnimator, buttonAnimator, fireani;
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        playSound = true;
        text = 2;
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (isActive)
        {
            if (AnimatorIsPlaying(boxAnimator, "staticBox"))
            {
                if (text == 2 && playSound)
                {
                    gameManager.PlaySound("eshulaugh");
                    playSound = false;
                }
                textAnimator.SetTrigger("Start");
                buttonAnimator.SetTrigger("start");
            }

            if (AnimatorIsPlaying(textAnimator, "end"))
                EndBoxAnimation();

            if (AnimatorIsPlaying(boxAnimator, "end"))
            {
                fireani.SetTrigger("start");
                gameManager.EndTutorial();
                isActive = false;
                Destroy(gameObject);
                Destroy(this);
            }
            if (AnimatorIsPlaying(textAnimator, states[text]))
            {
                if (clicked)
                {
                    clicked = false;
                    text++;
                    if (text == 11)
                        gameManager.PlaySound("eshulaugh");
                    NextText();
                }
                else
                    buttonAnimator.SetBool("Start", false);
            }
        }
    }
    bool AnimatorIsPlaying(Animator animator, string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
    public void Click()
    {
        buttonAnimator.SetBool("Start", true);
        clicked = true;
    }
    public void NextText()
    {
        textAnimator.SetInteger("CurrentText", text);
    }
    public void EndBoxAnimation()
    {
        boxAnimator.SetTrigger("end");
    }
}
