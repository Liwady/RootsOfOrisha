using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// The AnimationManager script is responsible for controlling the animations of various UI elements in the game.
public class AnimationManager : MonoBehaviour
{
    // A reference to the animator component of the eye image UI element.
    [SerializeField]
    private Animator eyeImage;

    // A reference to the animator component of the fruit image UI element.
    [SerializeField]
    private Animator fruitImage;

    // A list of game objects that represent the lights in the game.
    [SerializeField]
    private List<GameObject> lights;

    // A list of sprites that represent the different connection states in the game.
    [SerializeField]
    private List<Sprite> connection;

    // A reference to the Image component of the connection UI element.
    [SerializeField]
    private Image connectionImage;

    // A reference to the animator component of the light UI element.
    [SerializeField]
    private Animator lightAnimator;

    // A reference to the animator component of the skill UI element.
    [SerializeField]
    private Animator skillAnimator;

    // A reference to the animator component of the Zoudoo character.
    [SerializeField]
    private Animator zoudooAnimator;

    // A reference to the animator component of the Kouboo character.
    [SerializeField]
    private Animator koubooAnimator;

    // A reference to the SpriteRenderer component of the eye UI element.
    [SerializeField]
    private SpriteRenderer eye;

    // A reference to the SpriteRenderer component of the statue hint UI element.
    [SerializeField]
    private SpriteRenderer statueHint;

    // A counter that keeps track of the number of lights that have been lit in the game.
    public int lightCount;

    // A flag that indicates whether the Zoudoo character is currently active or not.
    [SerializeField]
    private bool zoudoo;


    // The Start method is called when the script is first executed.
    private void Start()
    {
        // Set the lightCount to 0.
        lightCount = 0;
    }

    // The TutorialFeedbackTrigger method is called when a tutorial stage is completed.
    public void TutorialFeedbackTrigger(int stage)
    {
        // Depending on the stage that was completed, set the appropriate trigger in the skillAnimator.
        switch (stage)
        {
            case 2:
                skillAnimator.SetBool("CanResize", true);
                break;
            case 3:
                skillAnimator.SetBool("CanFloat", true);
                break;
            default:
                break;
        }
    }

    //change the mechanics ui
    public void ChangeMechanicsSprite(int switchOption, bool sizeAbility, bool reset)
    {
        switch (switchOption)
        {
            //switch between size and float abilities
            case 0:
                //check if we want to switch to size ability
                if (sizeAbility)
                {
                    //reset the float trigger
                    skillAnimator.ResetTrigger("SwitchToSize");
                    //set the size trigger
                    skillAnimator.SetTrigger("SwitchToFloat");
                }
                //switch to float ability
                else
                {
                    //reset the size trigger
                    skillAnimator.ResetTrigger("SwitchToFloat");
                    //set the float trigger
                    skillAnimator.SetTrigger("SwitchToSize");
                }
                break;

            //switch between zoudoo and kouboo characters
            case 1:
                //check if the current character is zoudoo
                if (zoudoo)
                {
                    //set the bool indicating the character is not zoudoo
                    skillAnimator.SetBool("isZoudoo", false);
                    //update the zoudoo status
                    zoudoo = false;
                }
                //current character is kouboo
                else
                {
                    //set the bool indicating the character is zoudoo
                    skillAnimator.SetBool("isZoudoo", true);
                    //update the zoudoo status
                    zoudoo = true;
                }
                break;

            //switch ability trigger
            case 2:
                //check if we want to switch to size ability
                if (sizeAbility)
                {
                    //set the size ability trigger to active
                    skillAnimator.SetBool("SizeActive", true);
                    //check if we need to reset the trigger
                    if (reset)
                        //reset the size ability trigger
                        skillAnimator.SetBool("SizeActive", false);
                }
                //switch to float ability
                else
                {
                    //set the float ability trigger to active
                    skillAnimator.SetBool("FloatActive", true);
                    //check if we need to reset the trigger
                    if (reset)
                        //reset the float ability trigger
                        skillAnimator.SetBool("FloatActive", false);
                }
                break;
        }

    }

    //change the fruit ui
    public void ChangeFruitSprite(int fruitCollected)
    {
        //set the number of fruit collected in the animator
        fruitImage.SetInteger("score", fruitCollected);
    }

    //change the eye ui
    public void ChangeEyeSprite(bool eyeCollected)
    {
        //check if the eye is collected
        if (eyeCollected)
            //set the eye collected trigger
            eyeImage.SetTrigger("EyeAni");
        //eye is not collected
        else
            //reset the eye collected trigger
            eyeImage.ResetTrigger("EyeAni");
    }


    //start the fire animation
    public void StartFireAnimation(Vector3 position, int distance)
    {
        // check if the lightCount is less than the number of lights in the list
        if (lightCount < lights.Count)
        {
            // check if the distance between the position and the light position is less than the distance parameter
            if (Vector3.Distance(position, lights[lightCount].transform.position) < distance)
            {
                // get the lightAnimator component from the child of the light object
                lightAnimator = lights[lightCount].GetComponentInChildren<Animator>();
                // trigger the "Start" animation
                lightAnimator.SetTrigger("Start");
                // increase the lightCount by 1
                lightCount++;
            }
        }
    }

    //change the connection ui
    public void SwapConnectionSprite(bool zoudoo, bool together)
    {
        // check if the parameters together is true
        if (together)
        {
            // enable the eye
            eye.enabled = true;
            // set the connectionImage sprite to the first sprite in the connection list
            connectionImage.sprite = connection[0];
        }
        else
        {
            // disable the eye
            eye.enabled = false;
            // check if the zoudoo parameter is true
            if (zoudoo)
                // set the connectionImage sprite to the second sprite in the connection list
                connectionImage.sprite = connection[1];
            else
                // set the connectionImage sprite to the third sprite in the connection list
                connectionImage.sprite = connection[2];
        }
    }

    // This method sets the walking state for two characters, Zoudoo and Kouboo.
    public void WalkingState(bool isWalking, bool together, bool zoudoo)
    {
        // If the characters are walking together.
        if (together)
        {
            // If the characters are walking.
            if (isWalking)
            {
                // Set the walking state of Zoudoo to true.
                zoudooAnimator.SetBool("isWalking", true);

                // Set the walking state of Kouboo to true.
                koubooAnimator.SetBool("isWalking", true);
            }
            else
            {
                // Set the walking state of Zoudoo to false.
                zoudooAnimator.SetBool("isWalking", false);

                // Set the walking state of Kouboo to false.
                koubooAnimator.SetBool("isWalking", false);
            }
        }
        // If the characters are not walking together.
        else
        {
            // If Zoudoo is walking.
            if (zoudoo)
            {
                // If the characters are walking.
                if (isWalking)
                {
                    // Set the walking state of Zoudoo to true.
                    zoudooAnimator.SetBool("isWalking", true);

                    // Set the walking state of Kouboo to false.
                    koubooAnimator.SetBool("isWalking", false);
                }
                else
                    // Set the walking state of Zoudoo to false.
                    zoudooAnimator.SetBool("isWalking", false);
            }
            // If Kouboo is walking.
            else
            {
                // If the characters are walking.
                if (isWalking)
                {
                    // Set the walking state of Kouboo to true.
                    koubooAnimator.SetBool("isWalking", true);

                    // Set the walking state of Zoudoo to false.
                    zoudooAnimator.SetBool("isWalking", false);
                }
                else
                    // Set the walking state of Kouboo to false.
                    koubooAnimator.SetBool("isWalking", false);
            }
        }
    }




}
