using System.Collections.Generic;
using UnityEngine;

public class TriggerAble2 : MonoBehaviour
{
    enum Mode
    {
        up,
        down,
        weight,
    }
    [Header("General Must Set")]
    [SerializeField]
    private Mode mode;
    [SerializeField]
    private float maxXchangeValue, movementSpeed;
    [Header("Up Mode Specifics ")]
    [SerializeField]
    private GameObject deathZone;
    [Header("Weight Mode Specifics")]
    [SerializeField]
    private GameObject wheelUp;
    [SerializeField]
    private GameObject wheelDown;
    [SerializeField]
    private bool firstPackage;
    [SerializeField]
    private WeightManager weightManager;

    private Vector3 originalPosLocal;
    private bool triggered;
    private Wheel wheelUpScript, wheelDownScript;
    private PressurePlate2 pressurePlate;

    [HideInInspector]
    public List<CharacterScript> characterScriptsOnMe = new();

    private void Start()
    {

        originalPosLocal = transform.localPosition;
        if (mode == Mode.weight)
        {
            if (wheelUp != null)
            {
                wheelUpScript = wheelUp.GetComponent<Wheel>();
                wheelDownScript = wheelDown.GetComponent<Wheel>();
            }
            pressurePlate = GetComponent<PressurePlate2>();
        }
    }
    public void Toggle(bool _value) //sets the triggered value
    {
        triggered = _value;
    }

    private void ModeSwitch() //depending on which mode the TriggerAble is executes the corresponding code
    {
        switch (mode)
        {
            case Mode.up:
                if (triggered)
                {
                    if (deathZone != null)
                        deathZone.SetActive(false);
                    if (transform.localPosition.y < originalPosLocal.y + maxXchangeValue)
                        transform.Translate(new Vector3(0, movementSpeed, 0));
                }
                else if (transform.localPosition.y > originalPosLocal.y)
                {
                    if (deathZone != null)
                        deathZone.SetActive(true);
                    transform.Translate(new Vector3(0, -movementSpeed, 0));
                }
                break;
            case Mode.down:
                if (triggered)
                {
                    if (deathZone != null)
                        deathZone.SetActive(false);
                    if (transform.localPosition.y > originalPosLocal.y - maxXchangeValue)
                        transform.Translate(new Vector3(0, -movementSpeed, 0));
                }
                else if (transform.localPosition.y < originalPosLocal.y)
                {
                    if (deathZone != null)
                        deathZone.SetActive(true);
                    transform.Translate(new Vector3(0, movementSpeed, 0));
                }
                break;
            case Mode.weight:
                if (firstPackage)
                {
                    weightManager.weightOn1 = pressurePlate.weightOnMe;
                    weightManager.balanceWeights();
                    maxXchangeValue = weightManager.weightOn1New;
                }
                else
                {
                    weightManager.weightOn2 = pressurePlate.weightOnMe;
                    maxXchangeValue = weightManager.weightOn2New;
                }
                RotateWheels();
                if (maxXchangeValue > 0)
                {
                    if (transform.localPosition.y > originalPosLocal.y - maxXchangeValue)
                    {
                        transform.Translate(new Vector3(0, -movementSpeed, 0));
                        if (transform.localPosition.y < originalPosLocal.y - maxXchangeValue) // catch going over the limit
                            transform.localPosition = new Vector3(transform.localPosition.x, originalPosLocal.y - maxXchangeValue, transform.localPosition.z);
                        
                    }
                    else if (transform.localPosition.y < originalPosLocal.y - maxXchangeValue)
                        transform.Translate(new Vector3(0, movementSpeed, 0));
                }
                else if (maxXchangeValue < 0) // if maxchange is negative it means it needs to go up
                {

                    if (transform.localPosition.y < originalPosLocal.y - maxXchangeValue)
                    {
                        transform.Translate(new Vector3(0, movementSpeed, 0));
                        if (transform.localPosition.y > originalPosLocal.y - maxXchangeValue) // catch going over the limit
                            transform.localPosition = new Vector3(transform.localPosition.x, originalPosLocal.y - maxXchangeValue, transform.localPosition.z);
                    }
                    else if (transform.localPosition.y > originalPosLocal.y - maxXchangeValue)
                        transform.Translate(new Vector3(0, -movementSpeed, 0));
                }
                else if (maxXchangeValue == 0)
                {
                    if (transform.localPosition.y > originalPosLocal.y)
                    {
                        transform.Translate(new Vector3(0, -movementSpeed, 0));
                        if (transform.localPosition.y < originalPosLocal.y) //catch going over the origanl position
                            transform.localPosition = originalPosLocal;
                    }
                    else if (transform.localPosition.y < originalPosLocal.y)
                    {
                        transform.Translate(new Vector3(0, movementSpeed, 0));
                        if (transform.localPosition.y > originalPosLocal.y) //catch going over the origanl position
                            transform.localPosition = originalPosLocal;
                    }
                }
                break;
        }
    }


    private void RotateWheels()
    {
        if (triggered)
        {
            if (transform.localPosition.y < originalPosLocal.y)// if u are below ur starting position
            {
                if (transform.localPosition.y != originalPosLocal.y - maxXchangeValue) //max not reached
                    wheelDownScript.RotateWheelForward(true);
                else if(transform.localPosition.y == originalPosLocal.y - maxXchangeValue) //max reached
                    wheelDownScript.RotateWheelForward(false);
            }
            else if (transform.localPosition.y > originalPosLocal.y) //if u are above ur starting position
            {
                if (transform.localPosition.y != originalPosLocal.y - maxXchangeValue) //max not reached
                    wheelUpScript.RotateWheelForward(true);
                else if (transform.localPosition.y == originalPosLocal.y - maxXchangeValue) //max reached
                    wheelUpScript.RotateWheelForward(false);
            }
        }
    }

    private void Update()
    {
        ModeSwitch();
    }
}

