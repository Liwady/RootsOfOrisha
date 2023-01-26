using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private Animator mechanicsImage, eyeImage, fruitImage;
    [SerializeField]
    private List<GameObject> lights;

    private Animator animator;
    private int lightCount;
    //TODO wait for paya 
    public void ChangeMechanicsSprite(int ability,int character,bool together)
    {
    }

    public void ChangeFruitSprite(int fruitCollected)
    {
        fruitImage.SetInteger("score",fruitCollected);
    }
    public void ChangeEyeSprite(bool eyeCollected)
    {
        if(eyeCollected)
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
                animator = lights[lightCount].GetComponentInChildren<Animator>();
                animator.SetTrigger("Start");
                lightCount++;
            }
        }
    }

}
