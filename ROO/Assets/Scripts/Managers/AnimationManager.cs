using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private Animator eyeImage, fruitImage;
    [SerializeField]
    private List<GameObject> lights;
    [SerializeField]
    private List<Sprite> connection;
    [SerializeField]
    private Image connectionImage;
    [SerializeField]
    private Animator lightAnimator, skillAnimator, zoudooAnimator, koubooAnimator;
    [SerializeField]
    private SpriteRenderer eye,statueHint;
    public int lightCount;
    [SerializeField]
    private bool zoudoo;

    private void Start()
    {
        lightCount = 0;
    }

    //animate the ui in the tutorial
    public void TutorialFeedbackTrigger(int stage)
    {
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
            //switch ability
            case 0:
                //size
                if (sizeAbility)
                {
                    skillAnimator.ResetTrigger("SwitchToSize");
                    skillAnimator.SetTrigger("SwitchToFloat");
                }
                //float
                else
                {
                    skillAnimator.ResetTrigger("SwitchToFloat");
                    skillAnimator.SetTrigger("SwitchToSize");
                }
                break;

            //switch character
            case 1:
                if (zoudoo)
                {
                    skillAnimator.SetBool("isZoudoo", false);
                    zoudoo = false;
                }
                else
                {
                    skillAnimator.SetBool("isZoudoo", true);
                    zoudoo = true;
                }
                break;

            //trigger ability
            case 2:
                if (sizeAbility)
                {
                    skillAnimator.SetBool("SizeActive", true);
                    if (reset)
                        skillAnimator.SetBool("SizeActive", false);
                }
                else
                {
                    skillAnimator.SetBool("FloatActive", true);
                    if (reset)
                        skillAnimator.SetBool("FloatActive", false);
                }
                break;
        }

    }

    //change the fruit ui
    public void ChangeFruitSprite(int fruitCollected)
    {
        fruitImage.SetInteger("score", fruitCollected);
    }

    //change the eye ui
    public void ChangeEyeSprite(bool eyeCollected)
    {
        if (eyeCollected)
            eyeImage.SetTrigger("EyeAni");
        else
            eyeImage.ResetTrigger("EyeAni");
    }

    //start the fire animation 
    public void StartFireAnimation(Vector3 position, int distance)
    {
        if (lightCount < lights.Count)
        {
            if (Vector3.Distance(position, lights[lightCount].transform.position) < distance)
            {
                lightAnimator = lights[lightCount].GetComponentInChildren<Animator>();
                lightAnimator.SetTrigger("Start");
                lightCount++;
            }
        }
    }

    //change the connection ui
    public void SwapConnectionSprite(bool zoudoo, bool together)
    {
        if (together)
        {
            eye.enabled = true;
            connectionImage.sprite = connection[0];
        }
        else
        {
            eye.enabled = false;
            if (zoudoo)
                connectionImage.sprite = connection[1];
            else
                connectionImage.sprite = connection[2];
        }
    }

 
    public void WalkingState(bool isWalking, bool together, bool zoudoo)
    {
        if (together)
        {
            if (isWalking)
            {
                zoudooAnimator.SetBool("isWalking", true);
                koubooAnimator.SetBool("isWalking", true);
            }
            else
            {
                zoudooAnimator.SetBool("isWalking", false);
                koubooAnimator.SetBool("isWalking", false);
            }
        }
        else
        {
            if (zoudoo)
            {
                if (isWalking)
                {
                    zoudooAnimator.SetBool("isWalking", true);
                    koubooAnimator.SetBool("isWalking", false);
                }
                else
                    zoudooAnimator.SetBool("isWalking", false);
            }
            else
            {
                if (isWalking)
                {
                    koubooAnimator.SetBool("isWalking", true);
                    zoudooAnimator.SetBool("isWalking", false);
                }
                else
                    koubooAnimator.SetBool("isWalking", false);
            }
        }
    }



}
