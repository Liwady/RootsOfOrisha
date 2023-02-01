using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class startCamMove : MonoBehaviour
{
    private float timer2;
    [SerializeField]
    private float timer, newBlendTime;
    public int scene;
    private bool triggered, newBlendAsigned;
    [SerializeField]
    private CinemachineVirtualCamera startCam;
    [SerializeField]
    private CinemachineBrain brain;
    [SerializeField]
    private GameObject title;


    private void Update()
    {

        if (timer < 0 && !triggered)
        {
            triggered = true;
            startCam.Priority = -100;
            timer2 = brain.m_DefaultBlend.m_Time;
            title.SetActive(true);
            title.GetComponent<Animator>().SetInteger("scene",scene);
        }
        else if (!triggered)
            timer -= Time.deltaTime;
        else if (triggered)
        {
            timer2 -= Time.deltaTime;
            if (timer2 < 0 && !newBlendAsigned)
            {
                newBlendAsigned = true;
                brain.m_DefaultBlend.m_Time = newBlendTime;
                title.SetActive(false);
                Destroy(title);
            }
            else if(!newBlendAsigned)
                timer2 -= Time.deltaTime;
        }


    }
}
