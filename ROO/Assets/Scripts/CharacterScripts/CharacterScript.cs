using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    private PlayerManager playerManager;
    public GameObject checker, floatCheck, grabbedObject, detectedObject, grabPointS, grabPointL, grabPointBoat;
    public Rigidbody rb;
    public bool left, canMove, usedAbility, canResize, canWalkOnWater, isHoldingCollectible,isHoldingGrabbable, isGrounded, isOnWater;
    public float movementSpeed;
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
        rb = GetComponentInChildren<Rigidbody>();
        playerManager = FindObjectOfType<PlayerManager>();
    }
    public void ToggleLever()
    {
        if (inRangeLever != null)
            inRangeLever.ToggleLever();
    }
    public void Move(float _movement)
    {
        rb.velocity = new Vector3(_movement * movementSpeed, rb.velocity.y, rb.velocity.z);
    }
    public void MoveTowardsPlace(Transform _currentPlayerFloatPoint)
    {

        transform.position = Vector3.MoveTowards(transform.position, _currentPlayerFloatPoint.position, 5 * Time.deltaTime);

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
                grabbedObject.transform.position = grabbedObject.transform.position;
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
                        grabbedObject.transform.position = grabPointS.transform.position;
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
                    grabbedObject.transform.position = grabPointL.transform.position;
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
