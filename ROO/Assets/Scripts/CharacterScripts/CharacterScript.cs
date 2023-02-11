using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    // A reference to the PlayerManager component in the scene
    private PlayerManager playerManager;

    // Game objects related to the player character's interaction with the world
    public GameObject checker, floatCheck, grabbedObject, detectedObject, grabPoint, grabPointBoat, EyePoint, myRender, interactibleObject;

    // The Rigidbody component of the player character, used for controlling its physics-based movements
    public Rigidbody rb;

    // Variables to control various aspects of the player character's behavior and state
    public bool left, canMove, usedAbility, usedSizeAbility, usedFloatAbility, canResize, canWalkOnWater, isHoldingCollectible, isHoldingGrabbable, isGrounded, isOnWater, hitWhileFloating, moving;
    public float movementSpeed, lastDir, grabPointPosLeft = 1.9f, grabPointPosRight = -1.9f;
    public int size, weight;

    // A reference to a LeverScript component, used to determine if the player character is in range of an interactive lever
    public LeverScript inRangeLever;

    // A variable to store the type of "Fruit Eye" that the player character is holding
    public CollectibleScript.FruitEye typeEF;

    // The Awake function is called before the first frame update, it is used to initialize the state of the player character.
    void Awake()
    {
        // Set the initial state of the player character's abilities
        usedSizeAbility = false;
        usedFloatAbility = false;
        usedAbility = false;
        canWalkOnWater = false;
        isHoldingCollectible = false;
        isHoldingGrabbable = false;
        canResize = true;
        canMove = true;
        moving = false;

        // Get a reference to the PlayerManager component in the scene
        playerManager = FindObjectOfType<PlayerManager>();
    }
    public void ToggleLever()
    {
        // if inRangeLever is not null, toggle its lever
        if (inRangeLever != null)
        {
            inRangeLever.ToggleLever();
        }
    }

    public void Move(float movement)
    {
        // Call the Rotate method with the movement parameter
        Rotate(movement);

        // Call the MoveGrabPoint method with the movement parameter
        MoveGrabPoint(movement);

        // Set the velocity of the rigidbody component to a new Vector3 with the X value being the product of movement and movementSpeed, and the Y value being the current Y value of the velocity
        rb.velocity = new Vector3(movement * movementSpeed, rb.velocity.y);

        // Set the value of lastDir to movement
        lastDir = movement;
    }

    public void Rotate(float movement)
    {
        // If this is the character1script, do the following
        if (playerManager.character1script == this)
        {
            // If the movement is less than 0 and lastDir is also less than 0
            if (movement < 0 && lastDir < 0)
            {
                // Set the eulerAngles of the transform of myRender to a new Vector3 with X and Z values as 0, and the Y value being -35
                myRender.transform.eulerAngles = new Vector3(0, -35, 0);
            }
            // If the movement is greater than 0 and lastDir is also greater than 0
            else if (movement > 0 && lastDir > 0)
            {
                // Set the eulerAngles of the transform of myRender to a new Vector3 with X and Z values as 0, and the Y value being 170
                myRender.transform.eulerAngles = new Vector3(0, 170, 0);
            }
        }
        // If this is not the character1script, do the following
        else
        {
            // If the movement is less than 0 and lastDir is also less than 0
            if (movement < 0 && lastDir < 0)
            {
                // Set the eulerAngles of the transform of myRender to a new Vector3 with X and Z values as 0, and the Y value being -120
                myRender.transform.eulerAngles = new Vector3(0, -120, 0);
            }
            // If the movement is greater than 0 and lastDir is also greater than 0
            else if (movement > 0 && lastDir > 0)
            {
                // Set the eulerAngles of the transform of myRender to a new Vector3 with X and Z values as 0, and the Y value being 120
                myRender.transform.eulerAngles = new Vector3(0, 120, 0);
            }
        }
    }

    public void MoveTowardsPlace(Transform _currentPlayerFloatPoint)
    {
        // Move the object towards the `_currentPlayerFloatPoint` position
        // If the object hasn't hit something while floating, move it directly towards the `_currentPlayerFloatPoint` position
        if (!hitWhileFloating)
            transform.position = Vector3.MoveTowards(transform.position, _currentPlayerFloatPoint.position, 5 * Time.deltaTime);
        else
            // If it has hit something while floating, only move it on the x and z axis, keeping the y axis position unchanged
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_currentPlayerFloatPoint.position.x, transform.position.y, _currentPlayerFloatPoint.position.z), 5 * Time.deltaTime);
    }

    public void MoveGrabPoint(float movement)
    {
        // If the player is moving left and last direction was also left,
        // set the grabPoint position to the left grabPointPos
        if (movement < 0 && lastDir < 0)
            grabPoint.transform.localPosition = new Vector3(grabPointPosLeft, grabPoint.transform.localPosition.y, grabPoint.transform.localPosition.z);
        else if (movement > 0 && lastDir > 0)
            // If the player is moving right and last direction was also right,
            // set the grabPoint position to the right grabPointPos
            grabPoint.transform.localPosition = new Vector3(grabPointPosRight, grabPoint.transform.localPosition.y, grabPoint.transform.localPosition.z);
        // If there is an object grabbed, position it to the grabPoint
        if (grabbedObject != null)
            grabbedObject.transform.position = new Vector3(grabPoint.transform.position.x, grabPoint.transform.position.y, grabbedObject.transform.position.z);
    }

    public void InteractWithObject()
    {
        // If there is an interactible object, call the Interact() method on its InteractibleScript component
        if (interactibleObject != null)
        {
            interactibleObject.GetComponent<InteractibleScript>().Interact();
        }
    }
    public void GrabObject()
    {
        // Check if the player is holding a grabbable object
        if (isHoldingGrabbable)
        {
            // If the player is holding an object, set the `isHoldingGrabbable` flag to false
            // Remove the parent-child relationship between the player and the grabbed object
            // Set the `grabbedObject` to null and the `detectedObject` to null
            // Make the `Rigidbody` component of the grabbed object non-kinematic so that it can be affected by physics
            isHoldingGrabbable = false;
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject = null;
            detectedObject = null;
        }
        else if (detectedObject != null)
        {
            // If the player is not holding an object, set the `isHoldingGrabbable` flag to true
            // Set the `grabbedObject` to the `detectedObject`
            // Parent the `grabbedObject` to the player, so that it follows the player's movement
            // Make the `Rigidbody` component of the grabbed object kinematic so that it won't be affected by physics
            isHoldingGrabbable = true;
            grabbedObject = detectedObject;
            grabbedObject.transform.parent = gameObject.GetComponentInChildren<Transform>();
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

            // If the `detectedObject` has the tag "Statue", set its position to the position of the `grabPoint`
            if (detectedObject.CompareTag("Statue"))
            {
                grabbedObject.transform.position = new Vector3(grabPoint.transform.position.x, grabPoint.transform.position.y, grabbedObject.transform.position.z);
            }

            // If the player is `playerManager.character1` and the `detectedObject` has the tag "Boat" and the player is on water
            // Check the position of the `detectedObject` relative to the player
            // If the `detectedObject` is on the left side of the player and within a distance of `minDistance` from the player
            // Move the `detectedObject` to the left side of the player
            // If the `detectedObject` is on the right side of the player and within a distance of `minDistance` from the player
            // Move the `detectedObject` to the right side of the player
            float minDistance = 2.5f;
            if (gameObject == playerManager.character1)
            {
                if (detectedObject.CompareTag("Boat") && isOnWater)
                {
                    if (grabbedObject.transform.position.x - transform.position.x < 0 && grabbedObject.transform.position.x - transform.position.x > -minDistance)
                    {
                        //left side
                        grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x - 0.8f, grabbedObject.transform.position.y, grabbedObject.transform.position.z);
                    }
                    if (grabbedObject.transform.position.x - transform.position.x > 0 && grabbedObject.transform.position.x - transform.position.x < minDistance) //right side
                    {
                        grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x + 0.4f, grabbedObject.transform.position.y, grabbedObject.transform.position.z);
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // If the collider's tag is "Statue" or "Boat" and the player is not holding any object, set detectedObject to the collider's game object
        if (other.CompareTag("Statue") || (other.CompareTag("Boat") && !isHoldingGrabbable))
            detectedObject = other.gameObject;

        // If the collider's tag is "Interactible", set interactibleObject to the collider's game object
        if (other.CompareTag("Interactible"))
            interactibleObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        // If the collider's game object is equal to the detectedObject, set detectedObject to null
        if (other.gameObject == detectedObject)
            detectedObject = null;

        // If the collider's game object is equal to the interactibleObject, set interactibleObject to null
        if (other.gameObject == interactibleObject)
            interactibleObject = null;
    }
}
