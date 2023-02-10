using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera newKoubooCam, newZoubooCam; // two Cinemachine virtual cameras for two characters
    private bool triggered; // a flag to check if the trigger has already been activated
    private PlayerManager playerManager; // reference to the PlayerManager component

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>(); // find the PlayerManager component in the scene
    }

    private void OnTriggerExit(Collider other)
    {
        if (!triggered) // check if the trigger has not already been activated
        {
            playerManager.zoubooCam.Priority = -10; // lower the priority of the current cameras
            playerManager.koobouCam.Priority = -10;
            playerManager.zoubooCam = newZoubooCam; // assign the new cameras to the PlayerManager component
            playerManager.koobouCam = newKoubooCam;
            if (other.GetComponentInParent<CharacterScript>().Equals(playerManager.character1script)) // check which character triggered the exit event
            {
                playerManager.zoubooCam.Priority = 1; // set the priority of the new cameras for the corresponding characters
                playerManager.koobouCam.Priority = 0;
            }
            else
            {
                playerManager.zoubooCam.Priority = 0;
                playerManager.koobouCam.Priority = 1;
            }
        }
    }
}

