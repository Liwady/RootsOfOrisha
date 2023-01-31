using Cinemachine;
using UnityEngine;
public class GateCMActivate : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera gateCam;
    public bool activated;
    PlayerManager playerManager;

    [SerializeField]
    private float duration;
    private float timer;

    [HideInInspector]
    private bool cutSceneActive;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        timer = duration;
        gateCam.Priority = -5;
    }

    // Update is called once per frame
    void Update()
    {
        if (cutSceneActive && !activated)
        {
            if (timer < 0)
            {
                playerManager.cutscenePlaying = false;
                gateCam.Priority = -5;
                activated = true;
            }
            else if (cutSceneActive)
            {
                playerManager.cutscenePlaying = true;
                gateCam.Priority = 3;
                timer -= Time.deltaTime;
            }
        }

    }

    public void activatedCutscene()
    {
        cutSceneActive = true;
    }

}
