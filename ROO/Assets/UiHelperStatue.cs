using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHelperStatue : MonoBehaviour
{
    private AnimationManager animationManager;
    private int countPlayers;
    private void Awake()
    {
        animationManager = FindObjectOfType<AnimationManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            if (countPlayers == 0)
                animationManager.ShowHelpStatue(true);
          countPlayers++;
        }
        
    }



    private void OnTriggerExit(Collider other)
    {
        countPlayers--;
        if (countPlayers == 0)
        {
            animationManager.ShowHelpStatue(false);
        }
    }


}
