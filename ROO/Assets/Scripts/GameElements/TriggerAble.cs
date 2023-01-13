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

    public GameObject triggeredChar;

    GameManager gameManager;
    private Vector3 originalPos, originalPosLocal;
    private bool triggered;

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
                        transform.Translate(movementVector);
                }
                else if (transform.position.y > originalPos.y)
                    transform.Translate(-movementVector);
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
                    maxPos = new Vector3(0, gameManager.currentChar.GetComponent<CharacterScript>().weight, 1);
                    relatedWeightTriggerable.mode = Mode.weightup;
                    relatedWeightTriggerable.maxPos = this.maxPos;
                    relatedWeightTriggerable.movementVector = this.movementVector;
                    relatedWeightTriggerable.triggered = true;
                    if (transform.localPosition.y > originalPosLocal.y - maxPos.y)
                        transform.Translate(-movementVector);
                }
                else if (transform.position.y < originalPos.y)
                {
                    transform.Translate(movementVector);
                    relatedWeightTriggerable.triggered = false;
                }
                else if (transform.position.y == originalPos.y)
                    relatedWeightTriggerable.mode = Mode.weightdown;
                break;

            case Mode.weightup:
                if (triggered)
                {
                    maxPos = new Vector3(0, gameManager.currentChar.GetComponent<CharacterScript>().weight, 1);
                    if (transform.localPosition.y < originalPosLocal.y + maxPos.y)
                        transform.Translate(movementVector);
                }
                else if (transform.position.y > originalPos.y)
                    transform.Translate(-movementVector);

                relatedWeightTriggerable.mode = Mode.weightdown;
                relatedWeightTriggerable.maxPos = this.maxPos;
                relatedWeightTriggerable.movementVector = this.movementVector;
                break;
        }
    }
}
