using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAble2 : MonoBehaviour
{
    enum Mode
    {
        up,
        down,
        weightdown,
        weightup,
    }
    [SerializeField]
    private TriggerAble relatedWeightTriggerable;
    [SerializeField]
    private Mode mode;
    [SerializeField]
    private GameObject deathZone, wheel;

    [SerializeField]
    private float maxXchangeValue, movementSpeed;

    public GameObject triggeredChar;
    private Vector3 originalPosLocal;
    private bool triggered, hasWheel;
    private Wheel wheelScript;
    public int weightOnMe;

    private void Start()
    {
        originalPosLocal = transform.localPosition;
        if (wheel != null)
        {
            wheelScript = wheel.GetComponent<Wheel>();
            hasWheel = true;
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
                    transform.Translate(new Vector3(0, - movementSpeed, 0));
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
            case Mode.weightdown:

                break;
            case Mode.weightup:

                break;
        }
    }
    private void Update()
    {
        ModeSwitch();
    }
}

