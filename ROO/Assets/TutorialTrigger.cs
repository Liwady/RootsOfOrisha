using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private GameObject Lplayer;
    private GameManager gameManager;
    [SerializeField]
    private int gate,trigger;
    public bool isGate;
    

    private void Awake()
    {
        gameManager=FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            
            if (Lplayer != other.gameObject && Lplayer != null)
            {
                Lplayer = null;
                if (isGate)
                    gameManager.UpdateMechanicsTutorial(gate);
                else
                    gameManager.TutorialTriggers(trigger);
                Destroy(gameObject);
                Destroy(this);
            }
            else
                Lplayer = other.gameObject;
        }
    }
}
