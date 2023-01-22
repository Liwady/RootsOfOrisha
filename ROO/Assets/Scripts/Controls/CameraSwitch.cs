using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private GameObject Lplayer;
    public bool switchBounds, activated;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            if (Lplayer != other.gameObject && Lplayer != null)
            {
                switchBounds = true;
                Lplayer = null;
                activated = true;
            }
            else
                Lplayer = other.gameObject;

        }
    }
}
