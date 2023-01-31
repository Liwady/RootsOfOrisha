using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera newKoubooCam, newZoubooCam;
    private bool triggered;
    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!triggered)
        {
            playerManager.zoubooCam.Priority = -10;
            playerManager.koobouCam.Priority = -10;
            playerManager.zoubooCam = newZoubooCam;
            playerManager.koobouCam = newKoubooCam;
            if (other.GetComponentInParent<CharacterScript>().Equals(playerManager.character1script))
            {
                playerManager.zoubooCam.Priority = 1;
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
