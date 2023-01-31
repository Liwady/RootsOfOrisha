using UnityEngine;


public class eshuConvo : MonoBehaviour
{
    private GameManager gameManager;
    bool clicked;
    [SerializeField]
    private int text;
    [SerializeField]
    private string[] states;
    [SerializeField]
    private Animator boxAnimator, textAnimator, buttonAnimator;
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        text = 2;
        gameManager = FindObjectOfType<GameManager>();
        gameManager.StartCutscene();
    }
    private void Update()
    {
        if (AnimatorIsPlaying(boxAnimator, "staticBox"))
            textAnimator.SetTrigger("Start");

        if (AnimatorIsPlaying(textAnimator, "end"))
            EndBoxAnimation();

        if ( AnimatorIsPlaying(boxAnimator, "end"))
        {
            gameManager.EndTutorial();
            Destroy(gameObject);
            Destroy(this);
        }
        if (AnimatorIsPlaying(textAnimator, states[text]))
        {
            if (clicked)
            {
                clicked = false;
                text++;
                NextText();
            }
            else
                buttonAnimator.SetBool("Start", false);
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
