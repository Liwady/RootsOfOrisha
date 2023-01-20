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
    [SerializeField]
    private TriggerAble relatedWeightTriggerable;
    [SerializeField]
    private Mode mode;
    [SerializeField]
    private Vector3 movementVector, maxPos;
    [SerializeField]
    private bool hasDeathZone;
    [SerializeField]
    private GameObject deathZone;
    public GameObject triggeredChar;
    private Vector3 originalPos, originalPosLocal;
    private bool triggered;

    private void Start()
    {
        originalPos = transform.position;
        originalPosLocal = transform.localPosition;
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
                    if (transform.localPosition.y < maxPos.y)
                        transform.Translate(movementVector);
                }
                else if (transform.position.y > originalPos.y)
                {
                    if (hasDeathZone)
                        deathZone.SetActive(true);
                    transform.Translate(-movementVector);
                }
                else if (transform.position.y == originalPos.y && hasDeathZone && deathZone.activeSelf)
                    deathZone.SetActive(false);
                break;

            case Mode.down:
                if (triggered)
                {
                    if (transform.localPosition.y > maxPos.y) //max pos downwards 
                        transform.Translate(-movementVector);
                }
                else if (transform.position.y < originalPos.y)
                    transform.Translate(movementVector);
                break;

            case Mode.weightdown:
                if (triggered)
                {
                    maxPos = new Vector3(0, gameObject.GetComponent<PressurePlate>().triggeredChar.GetComponentInParent<CharacterScript>().weight, 1);
                    relatedWeightTriggerable.mode = Mode.weightup;
                    relatedWeightTriggerable.maxPos = maxPos;
                    relatedWeightTriggerable.movementVector = movementVector;
                    relatedWeightTriggerable.triggered = true;
                    if (transform.localPosition.y > originalPosLocal.y - maxPos.y)
                    {
                        if (hasDeathZone)
                            deathZone.SetActive(true);
                        transform.Translate(-movementVector);
                    }
                }
                else if (transform.position.y < originalPos.y)
                {
                    transform.Translate(movementVector);
                    deathZone.SetActive(false);
                    relatedWeightTriggerable.triggered = false;
                }
                else if (transform.position.y == originalPos.y)
                    relatedWeightTriggerable.mode = Mode.weightdown;
                break;

            case Mode.weightup:
                if (triggered)
                {
                    if (transform.localPosition.y < originalPosLocal.y + maxPos.y)
                        transform.Translate(movementVector);
                }
                else if (transform.position.y > originalPos.y)
                {
                    if (hasDeathZone)
                        deathZone.SetActive(true);
                    transform.Translate(-movementVector);
                }
                relatedWeightTriggerable.mode = Mode.weightdown;
                relatedWeightTriggerable.maxPos = maxPos;
                relatedWeightTriggerable.movementVector = movementVector;
                break;
        }
    }
    private void Update()
    {
        ModeSwitch();
    }
}
