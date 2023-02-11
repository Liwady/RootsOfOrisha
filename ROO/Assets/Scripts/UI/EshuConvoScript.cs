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
        isActive = false;
        text = 2;
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (isActive) // if the cutscene is active 
        {
            if (AnimatorIsPlaying(boxAnimator, "staticBox")) //if when the animator is playing the staticbox state 
            {
                if (text == 2 && playSound) //second text and u still need to play the laugh sound => play the sound
                {
                    gameManager.PlaySound("eshulaugh");
                    playSound = false;
                }
                textAnimator.SetTrigger("Start"); //start the text animations
                if (buttonAnimator != null)
                    buttonAnimator.SetTrigger("start"); //start the button animations
            }

            if (AnimatorIsPlaying(textAnimator, "end")) //once the text ani ends start the box end animation
                EndBoxAnimation();

            if (AnimatorIsPlaying(boxAnimator, "end")) //if the box animator is in end state
            {
                fireani.SetTrigger("Start"); //sstart fires of the level
                gameManager.EndCutscene(); //call the game manager to end the cutscene
                isActive = false; //cutscene not active
                Destroy(gameObject);
                Destroy(this);
            }
            if (AnimatorIsPlaying(textAnimator, states[text])) // if in static state (waiting for the button) 
            {
                if (clicked) //if the button is clicked 
                {
                    clicked = false; //clicked false and next text 
                    text++;
                    if (text == 11) //laugh at text11
                        gameManager.PlaySound("eshulaugh");
                    NextText();
                }
                else
                    buttonAnimator.SetBool("Start", false); //not clicked == button animation starts blinking
            }
        }
    }
    bool AnimatorIsPlaying(Animator animator, string name) //checks if the animator is playing the state with given name
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
    public void Click()
    {
        buttonAnimator.SetBool("Start", true); //turn off blinking button animation
        clicked = true;
    }
    public void NextText()
    {
        textAnimator.SetInteger("CurrentText", text);//set text int in the animator
    }
    public void EndBoxAnimation() //set trigger and destroy the button 
    {
        boxAnimator.SetTrigger("end");
        Destroy(buttonAnimator);
    }
}
