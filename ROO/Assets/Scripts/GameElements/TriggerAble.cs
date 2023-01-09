using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAble : MonoBehaviour
{
    enum Mode
    {
        up,
        down,
        weightdown,
        weightup,
    }

    GameManager gameManager;

    [SerializeField]
    private TriggerAble relatedWeightTriggerable;

    [SerializeField]
    private Vector3 movementVector;
    [SerializeField]
    private Vector3 maxPos;

    private Vector3 originalPos;
    private Vector3 originalPosLocal;




    private bool triggered;


    [SerializeField]
    private Mode mode;

    private void Start()
    {
        originalPos = transform.position;
        originalPosLocal = transform.localPosition;
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Toggle(bool _value)
    {
        triggered = _value;
    }



    private void Update()
    {
        switch (mode)
        {
            case Mode.up:
                if (triggered)
                {
                    if (transform.localPosition.y < maxPos.y)
                    {
                        transform.Translate(movementVector);
                    }
                }
                else if (transform.position.y > originalPos.y)
                {
                    transform.Translate(-movementVector);
                }
                break;
            case Mode.down:
                if (triggered)
                {
                    if (transform.localPosition.y > maxPos.y) //max pos downwards 
                    {
                        transform.Translate(-movementVector);
                    }
                }
                else if (transform.position.y < originalPos.y)
                {
                    transform.Translate(movementVector);
                }
                break;
            case Mode.weightdown:
                if (triggered)
                {
                    maxPos = new Vector3(0, gameManager.currentChar.GetComponent<CharacterScript>().weight, 1);
                    relatedWeightTriggerable.mode = Mode.weightup;
                    relatedWeightTriggerable.maxPos = this.maxPos;
                    relatedWeightTriggerable.movementVector = this.movementVector;
                    relatedWeightTriggerable.triggered = true;
                    if (transform.localPosition.y > originalPosLocal.y - maxPos.y)
                    {
                        transform.Translate(-movementVector);
                    }
                }
                else if(transform.position.y < originalPos.y)
                {
                    transform.Translate(movementVector);
                    relatedWeightTriggerable.triggered = false;

                }
                else if (transform.position.y == originalPos.y)
                {
                    relatedWeightTriggerable.mode = Mode.weightdown;
                }

                break;
            case Mode.weightup:
                if (triggered)
                {
                    maxPos = new Vector3(0, gameManager.currentChar.GetComponent<CharacterScript>().weight, 1);
                    if (transform.localPosition.y < originalPosLocal.y + maxPos.y)
                    {
                        transform.Translate(movementVector);
                    }
                }
                else if (transform.position.y > originalPos.y)
                {
                    transform.Translate(-movementVector);
                }
                relatedWeightTriggerable.mode = Mode.weightdown;
                relatedWeightTriggerable.maxPos = this.maxPos;
                relatedWeightTriggerable.movementVector = this.movementVector;
                break;
        }
    }
}
