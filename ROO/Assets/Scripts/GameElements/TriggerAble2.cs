using System.Collections;
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
    private GameObject wheel;
    [SerializeField]
    private bool firstPackage;
    private Vector3 originalPosLocal;
    private bool triggered, hasWheel;
    private Wheel wheelScript;
    private PressurePlate2 pressurePlate;
    private WeightManager weightManager;


    private void Start()
    {
        originalPosLocal = transform.localPosition;
        if (wheel != null)
        {
            wheelScript = wheel.GetComponent<Wheel>();
            hasWheel = true;
            weightManager = wheel.GetComponent<WeightManager>();
        }
        if (mode == Mode.weight)
        {
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
                        transform.Translate(new Vector3(0, movementSpeed,0));
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
                if (maxXchangeValue > 0)
                {
                    if (transform.localPosition.y > originalPosLocal.y - maxXchangeValue)
                    {
                        transform.Translate(new Vector3(0, -movementSpeed, 0));
                        if (transform.localPosition.y < originalPosLocal.y - maxXchangeValue) // catch going over the limit
                        {
                            transform.localPosition = new Vector3(transform.localPosition.x, originalPosLocal.y - maxXchangeValue, transform.localPosition.z);
                        }
                    }
                    else if (transform.localPosition.y < originalPosLocal.y - maxXchangeValue)
                    {
                        transform.Translate(new Vector3(0, movementSpeed, 0));
                    }
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
                    {
                        transform.Translate(new Vector3(0, -movementSpeed, 0));
                    }
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
    private void Update()
    {
        ModeSwitch();
    }
}

