// Gate Camera Activation Script
using Cinemachine;
using UnityEngine;

public class GateCMActivate : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera gateCam; // the virtual camera to activate
    public bool activated; // boolean to check if the camera is activated
    PlayerManager playerManager;

    [SerializeField]
    private float duration; // the duration of the cutscene
    private float timer; // timer for the cutscene

    [HideInInspector]
    private bool cutSceneActive; // boolean to check if the cutscene is active

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>(); // finds the PlayerManager
        timer = duration;
        gateCam.Priority = -5; // deactivates the virtual camera
    }

    // Update is called once per frame
    void Update()
    {
        if (cutSceneActive && !activated) // if the cutscene is active and not activated
        {
            if (timer < 0) // if the timer is up
            {
                playerManager.cutscenePlaying = false;
                gateCam.Priority = -5;
                activated = true; // activate the camera
            }
            else if (cutSceneActive) // if the cutscene is still active
            {
                playerManager.cutscenePlaying = true;
                gateCam.Priority = 3; // activates the virtual camera
                timer -= Time.deltaTime; // reduces the timer
            }
        }

    }

    public void activatedCutscene() // activates the cutscene
    {
        cutSceneActive = true;
    }

}
