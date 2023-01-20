using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private bool update;

    private CharacterScript myChar = null;
    private Collider myCharCo;


    private void OnTriggerEnter(Collider other) //checks if something enters the Water collider
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            update = true;
            myChar = other.gameObject.GetComponentInParent<CharacterScript>();
            myCharCo = other;
        }
    }

    private void OnTriggerExit(Collider other) //checks if something leaves the Water collider
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
            if (!myChar.canWalkOnWater)
            {
                myCharCo.isTrigger = true;
                myChar.GetComponentInParent<Rigidbody>().drag = 10;
            }
        }
    }
}
