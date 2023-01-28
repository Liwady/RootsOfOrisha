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
    private Animator lightAnimator, skillAnimator;
    [SerializeField]
    private int skillCount, lightCount;
    [SerializeField]
    private bool zoudoo;

    public void ChangeMechanicsSprite(bool tutorial, int switchOption, bool sizeAbility, bool reset)
    {
        //tutorial
        if (tutorial)
        {
            if (skillCount == 0)
            {
                skillAnimator.SetTrigger("CanSize");
                skillCount++;
            }
            else
                skillAnimator.SetTrigger("CanFloat");
        }
        //all
        else
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
    public void SwapConnectionSprite(int num)
    {
        connectionImage.sprite = connection[num];
    }

}
