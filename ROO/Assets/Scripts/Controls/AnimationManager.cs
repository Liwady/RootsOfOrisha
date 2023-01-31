using System.Collections.Generic;
using TMPro;
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
    private Animator lightAnimator, skillAnimator,zoudooAnimator,koubooAnimator;
    private int lightCount;
    [SerializeField]
    private bool zoudoo;

    public void TutorialFeedbackTrigger(int stage)
    {
        switch (stage)
        {
            case 1://enable move together // show grab button 
                break;
            case 2://enable trigger ability // show trigger button 
                skillAnimator.SetBool("CanResize", true);
                break;
            case 3://enable switch ability => show switch button 
                skillAnimator.SetBool("CanFloat", true);
                break;
            case 4://enable grab => show grab button
                break;
            default:
                break;
        }
    }
    public void ChangeMechanicsSprite(int switchOption, bool sizeAbility, bool reset)
    {
        switch (switchOption)
        {
            case 0://switch ability
                if (sizeAbility)//size
                {
                    skillAnimator.ResetTrigger("SwitchToSize");
                    skillAnimator.SetTrigger("SwitchToFloat");
                }
                else //float
                {
                    skillAnimator.ResetTrigger("SwitchToFloat");
                    skillAnimator.SetTrigger("SwitchToSize");
                }
                break;
            case 1://switch character
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
            case 2://trigger ability
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
    public void ChangeFruitSprite(int fruitCollected)
    {
        fruitImage.SetInteger("score", fruitCollected);
    }
    public void ChangeEyeSprite(bool eyeCollected)
    {
        if (eyeCollected)
            eyeImage.SetTrigger("EyeAni");
        else
            eyeImage.ResetTrigger("EyeAni");

    }
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
    public void SwapConnectionSprite(int num,bool together)
    {
        if(together)
            connectionImage.sprite = connection[0];
        else
            connectionImage.sprite = connection[num];
    }
    public void ShowHelpStatue(bool _show)
    {
        Debug.Log("show help statue");
    }
    public void WalkingState(bool isWalking)
    {
        if (isWalking)
            zoudooAnimator.SetBool("isWalking", true);
        else
            zoudooAnimator.SetBool("isWalking", false);
    }
}
