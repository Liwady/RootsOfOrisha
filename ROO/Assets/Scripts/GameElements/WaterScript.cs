using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private bool update;
    [SerializeField]
    private Collider myWaterBody;


    private CharacterScript myChar = null;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            update = true;
            myChar = other.gameObject.GetComponentInParent<CharacterScript>();
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            update = false;
            myChar.GetComponentInParent<Rigidbody>().drag = 0;
        }
    }

    private void Update()
    {
        if (update)
        {
            if (myChar.canWalkOnWater)
                myWaterBody.isTrigger = false;
            else
            {
                myChar.GetComponentInParent<Rigidbody>().drag = 10;
                myWaterBody.isTrigger = true;
            }
        }
    }
}
