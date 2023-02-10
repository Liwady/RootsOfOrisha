using Cinemachine;
using UnityEngine;

public class startCamMove : MonoBehaviour
{
    private float timer2;
    [SerializeField]
    private float timer, newBlendTime;
    public int scene; // scene identifier
    public bool triggered, newBlendAsigned, triggerAni; // boolean flags
    [SerializeField]
    private CinemachineVirtualCamera startCam; // reference to the virtual camera
    [SerializeField]
    private CinemachineBrain brain; // reference to the brain
    [SerializeField]
    private GameObject title; // reference to the title game object

    private void Update()
    {
        // When the timer is less than 0 and not triggered yet, trigger it
        if (timer < 0 && !triggered)
        {
            triggered = true;
            startCam.Priority = -100; // Change the camera priority
            timer2 = brain.m_DefaultBlend.m_Time; // Get the default blend time
        }
        else if (!triggered)
            timer -= Time.deltaTime; // Decrement timer

        else if (triggered) // Triggered
        {
            if (triggerAni) // Play the animation
            {
                title.SetActive(true); // Show the title
                title.GetComponent<Animator>().SetInteger("scene", scene); // Set the scene in the animator
                triggerAni = false; // Stop triggering the animation
            }
            timer2 -= Time.deltaTime; // Decrement timer2
            if (timer2 < 0 && !newBlendAsigned)
            {
                newBlendAsigned = true;
                brain.m_DefaultBlend.m_Time = newBlendTime; // Set the new blend time
                if (title != null)
                {
                    title.SetActive(false); // Hide the title
                    Destroy(title); // Destroy the title game object
                }
            }
            else if (!newBlendAsigned)
                timer2 -= Time.deltaTime; // Decrement timer2
        }
    }
}

