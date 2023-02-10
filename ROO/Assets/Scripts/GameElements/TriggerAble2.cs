using System.Collections.Generic;
using UnityEngine;

public class TriggerAble2 : MonoBehaviour
{
    // Enum for different modes of operation for the trigger
    enum Mode
    {
        up,
        down,
        weight,
    }

    // Variables to be set in the inspector
    [Header("General Must Set")]
    [SerializeField]
    private Mode mode; // The mode of operation for the trigger
    [SerializeField]
    private float maxXchangeValue, movementSpeed; // Maximum change in the position of the object and the speed at which it moves

    // Variables specific to the "Up" mode
    [Header("Up Mode Specifics ")]
    [SerializeField]
    private GameObject deathZone; // The deathzone to be activated/deactivated when the trigger is triggered/not triggered

    // Variables specific to the "Weight" mode
    [Header("Weight Mode Specifics")]
    [SerializeField]
    private GameObject wheelUp; // The upper wheel for the weight system
    [SerializeField]
    private GameObject wheelDown; // The lower wheel for the weight system
    [SerializeField]
    private bool firstPackage; // Whether the triggerable is the first package in the weight system
    [SerializeField]
    private WeightManager weightManager; // The weight manager to calculate and balance the weights

    // Private variables
    private Vector3 originalPosLocal; // The original local position of the object
    private bool triggered; // Whether the trigger has been triggered or not
    private Wheel wheelUpScript, wheelDownScript; // Scripts for the upper and lower wheels
    private PressurePlate2 pressurePlate; // The pressure plate for the weight system
    private GateCMActivate cutSceneStarter; // The cutscene starter, if any, for the triggerable
    private bool hasCutscene; // Whether the triggerable has a cutscene or not

    // List to keep track of characters on the triggerable
    [HideInInspector]
    public List<CharacterScript> characterScriptsOnMe = new List<CharacterScript>();



    private void Start()
    {
        // Check if the triggerable has a cutscene and set up the cutscene starter
        if (GetComponent<GateCMActivate>() != null)
        {
            hasCutscene = true;
            cutSceneStarter = GetComponent<GateCMActivate>();
        }

        // Set up the original position of the object
        originalPosLocal = transform.localPosition;

        // Set up the weight system, if the mode is "Weight"
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

    // Function to set the triggered value
    public void Toggle(bool _value)
    {
        triggered = _value;
    }

    private void ModeSwitch() //depending on which mode the TriggerAble is executes the corresponding code
    {
        // Calculate the movement speed based on the movement speed and the delta time.
        float _movementSpeed = movementSpeed * Time.deltaTime;
        // Switch statement to execute code based on the mode of the TriggerAble.
        switch (mode)
        {
            // Up mode specific code.
            case Mode.up:
                // If triggered, move the object up until it reaches the maximum height.
                if (triggered)
                {
                    // Disable the death zone.
                    if (deathZone != null)
                        deathZone.SetActive(false);
                    // Check if the object has not reached the maximum height.
                    if (transform.localPosition.y < originalPosLocal.y + maxXchangeValue)
                        // Move the object up by the calculated movement speed.
                        transform.Translate(new Vector3(0, _movementSpeed, 0));
                }
                // If not triggered, move the object down until it reaches its original position.
                else if (transform.localPosition.y > originalPosLocal.y)
                {
                    // Enable the death zone.
                    if (deathZone != null)
                        deathZone.SetActive(true);
                    // Move the object down by the calculated movement speed.
                    transform.Translate(new Vector3(0, -_movementSpeed, 0));
                }
                break;
            // Down mode specific code.
            case Mode.down:
                // If triggered, move the object down until it reaches the maximum height.
                if (triggered)
                {
                    // Enable the death zone.
                    if (deathZone != null)
                        deathZone.SetActive(true);
                    // Check if the object has not reached the maximum height.
                    if (transform.localPosition.y > originalPosLocal.y - maxXchangeValue)
                        // Move the object down by the calculated movement speed.
                        transform.Translate(new Vector3(0, -_movementSpeed, 0));
                }
                // If not triggered, move the object up until it reaches its original position.
                else if (transform.localPosition.y < originalPosLocal.y)
                {
                    // Disable the death zone.
                    if (deathZone != null)
                        deathZone.SetActive(false);
                    // Move the object up by the calculated movement speed.
                    transform.Translate(new Vector3(0, _movementSpeed, 0));
                }
                break;
            // Weight mode specific code.
            case Mode.weight:
                // Determine the weight on the pressure plate.
                if (firstPackage)
                {
                    // Update the weight on the first package and balance the weights.
                    weightManager.weightOn1 = pressurePlate.weightOnMe;
                    weightManager.balanceWeights();
                    // Update the maximum change value based on the new weight on the first package.
                    maxXchangeValue = weightManager.weightOn1New;
                }
                else
                {
                    // Update the weight on the second package.
                    weightManager.weightOn2 = pressurePlate.weightOnMe;
                    // Update the maximum change value based on the new weight on the second package.
                    maxXchangeValue = weightManager.weightOn2New;
                }
                // Rotate the wheels.
                RotateWheels();
                // Check if the max change value is greater than 0
                if (maxXchangeValue > 0)
                {
                    // Check if the object's current y-position is greater than the original position minus the max change value
                    if (transform.localPosition.y > originalPosLocal.y - maxXchangeValue)
                    {
                        // Translate the object downwards
                        transform.Translate(new Vector3(0, -_movementSpeed, 0));
                        // Check if the object has gone over the limit
                        if (transform.localPosition.y < originalPosLocal.y - maxXchangeValue)
                        {
                            // Set the object's position to the limit
                            transform.localPosition = new Vector3(transform.localPosition.x, originalPosLocal.y - maxXchangeValue, transform.localPosition.z);
                        }
                    }
                    // Check if the object's current y-position is less than the original position minus the max change value
                    else if (transform.localPosition.y < originalPosLocal.y - maxXchangeValue)
                    {
                        // Translate the object upwards
                        transform.Translate(new Vector3(0, _movementSpeed, 0));
                    }
                }
                // Check if the max change value is less than 0
                else if (maxXchangeValue < 0)
                {
                    // Check if the object's current y-position is less than the original position minus the max change value
                    if (transform.localPosition.y < originalPosLocal.y - maxXchangeValue)
                    {
                        // Translate the object upwards
                        transform.Translate(new Vector3(0, _movementSpeed, 0));
                        // Check if the object has gone over the limit
                        if (transform.localPosition.y > originalPosLocal.y - maxXchangeValue)
                        {
                            // Set the object's position to the limit
                            transform.localPosition = new Vector3(transform.localPosition.x, originalPosLocal.y - maxXchangeValue, transform.localPosition.z);
                        }
                    }
                    // Check if the object's current y-position is greater than the original position minus the max change value
                    else if (transform.localPosition.y > originalPosLocal.y - maxXchangeValue)
                    {
                        // Translate the object downwards
                        transform.Translate(new Vector3(0, -_movementSpeed, 0));
                    }
                }
                // Check if the max change value is equal to 0
                else if (maxXchangeValue == 0)
                {
                    // Check if the object's current y-position is greater than the original position
                    if (transform.localPosition.y > originalPosLocal.y)
                    {
                        // Translate the object downwards
                        transform.Translate(new Vector3(0, -_movementSpeed, 0));
                        // Check if the object has gone over the original position
                        if (transform.localPosition.y < originalPosLocal.y)
                        {
                            // Set the object's position to the original position
                            transform.localPosition = originalPosLocal;
                        }
                    }
                    // Check if the object's current y-position is less than the original position
                    else if (transform.localPosition.y < originalPosLocal.y)
                    {
                        // Translate the object upwards
                        transform.Translate(new Vector3(0, _movementSpeed, 0));
                        if (transform.localPosition.y > originalPosLocal.y) //catch going over the origanl position
                            transform.localPosition = originalPosLocal;
                    }
                }
                break;
        }
    }


    //This method is used to rotate wheels if triggered
    private void RotateWheels()
    {
        if (triggered) // if triggered is true
        {
            if (transform.localPosition.y < originalPosLocal.y) // if the object is below its original position
            {
                if (transform.localPosition.y != originalPosLocal.y - maxXchangeValue) // if max is not reached
                    wheelDownScript.RotateWheelForward(true); // rotate the wheel down
                else if (transform.localPosition.y == originalPosLocal.y - maxXchangeValue) // if max is reached
                    wheelDownScript.RotateWheelForward(false); // stop the wheel from rotating down
            }
            else if (transform.localPosition.y > originalPosLocal.y) // if the object is above its original position
            {
                if (transform.localPosition.y != originalPosLocal.y - maxXchangeValue) // if max is not reached
                    wheelUpScript.RotateWheelForward(true); // rotate the wheel up
                else if (transform.localPosition.y == originalPosLocal.y - maxXchangeValue) // if max is reached
                    wheelUpScript.RotateWheelForward(false); // stop the wheel from rotating up
            }
        }
    }

    //This method is used to activate cutscene if hasCutscene is true and triggered is true
    private void ActivatedCutscene()
    {
        if (hasCutscene && triggered) // if hasCutscene is true and triggered is true
        {
            cutSceneStarter.activatedCutscene(); // activate cutscene
        }
    }

    //This method is called every frame
    private void Update()
    {
        ModeSwitch(); // call ModeSwitch() method
        ActivatedCutscene(); // call ActivatedCutscene() method
    }
}

