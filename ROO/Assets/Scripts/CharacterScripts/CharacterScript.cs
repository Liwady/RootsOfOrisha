using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    private PlayerManager playerManager;
    public GameObject checker, floatCheck, grabbedObject, detectedObject, grabPoint, grabPointBoat, EyePoint, myRender;
    public Rigidbody rb;
    public bool left, canMove, usedAbility, canResize, canWalkOnWater, isHoldingCollectible, isHoldingGrabbable, isGrounded, isOnWater, hitWhileFloating, moving;
    public float movementSpeed, lastDir, grabPointPosLeft = 1.9f, grabPointPosRight = -1.9f;
    public int size, weight;
    public LeverScript inRangeLever;
    public CollectibleScript.FruitEye typeEF;


    void Awake()
    {
        usedAbility = false;
        canWalkOnWater = false;
        isHoldingCollectible = false;
        isHoldingGrabbable = false;
        canResize = true;
        canMove = true;
        moving = false;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void ToggleLever()
    {
        if (inRangeLever != null)
            inRangeLever.ToggleLever();
    }
    public void Move(float movement)
    {
        Rotate(movement);
        MoveGrabPoint(movement);
        rb.velocity = new Vector3(movement * movementSpeed, rb.velocity.y);
        lastDir = movement;

    }
    public void Rotate(float movement)
    {
        if (movement < 0 && lastDir < 0)
            myRender.transform.eulerAngles = new Vector3(0, -140, 0);
        else if (movement > 0 && lastDir > 0)
            myRender.transform.eulerAngles = new Vector3(0, 140, 0);
    }
    public void MoveTowardsPlace(Transform _currentPlayerFloatPoint)
    {
        if (!hitWhileFloating)
            transform.position = Vector3.MoveTowards(transform.position, _currentPlayerFloatPoint.position, 5 * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_currentPlayerFloatPoint.position.x, transform.position.y, _currentPlayerFloatPoint.position.z), 5 * Time.deltaTime);

    }

    public void MoveGrabPoint(float movement)
    {
        if (movement < 0 && lastDir < 0)
            grabPoint.transform.localPosition = new Vector3(grabPointPosLeft, grabPoint.transform.localPosition.y, grabPoint.transform.localPosition.z);
        else if (movement > 0 && lastDir > 0)
            grabPoint.transform.localPosition = new Vector3(grabPointPosRight, grabPoint.transform.localPosition.y, grabPoint.transform.localPosition.z);
        if (grabbedObject != null)
            grabbedObject.transform.position = new Vector3(grabPoint.transform.position.x, grabPoint.transform.position.y,grabbedObject.transform.position.z);
    }
    public void GrabObject()
    {
        if (detectedObject != null)
        {
            //if  holding item -> drop
            //set bool false, unparent object, grabbed object null
            if (isHoldingGrabbable)
            {
                isHoldingGrabbable = false;
                grabbedObject.transform.parent = null;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject = null;
                detectedObject = null;
            }
            //if  holding nothing -> grab
            //set bool to true, parent object to player, grabbed object to object 
            else
            {
                float minDistance = 2.5f;
                if (gameObject == playerManager.character1)
                {
                    if (detectedObject.CompareTag("Statue"))
                    {
                        isHoldingGrabbable = true;
                        grabbedObject = detectedObject;
                        grabbedObject.transform.parent = gameObject.GetComponentInChildren<Transform>();
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.position = new Vector3(grabPoint.transform.position.x, grabPoint.transform.position.y, grabbedObject.transform.position.z);
                    }
                    else if (detectedObject.CompareTag("Boat") && isOnWater)
                    {
                        isHoldingGrabbable = true;
                        grabbedObject = detectedObject;
                        grabbedObject.transform.parent = gameObject.GetComponentInChildren<Transform>();
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
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
                else
                {
                    isHoldingGrabbable = true;
                    grabbedObject = detectedObject;
                    grabbedObject.transform.parent = gameObject.GetComponentInChildren<Transform>();
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedObject.transform.position = new Vector3(grabPoint.transform.position.x, grabPoint.transform.position.y, grabbedObject.transform.position.z);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Statue") || other.CompareTag("Boat") && !isHoldingGrabbable)
            detectedObject = other.gameObject;
    }
}
