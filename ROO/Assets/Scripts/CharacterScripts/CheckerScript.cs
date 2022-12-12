using UnityEngine;

public class CheckerScript : MonoBehaviour
{
    private bool isEmpty = true;

    public bool checkIfColliderEmpty()
    {
        return isEmpty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log(this.name);
            Debug.Log(other.name);
            isEmpty = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            isEmpty = true;
        }
    }
}


