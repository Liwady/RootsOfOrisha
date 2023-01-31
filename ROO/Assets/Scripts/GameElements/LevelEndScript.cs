using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    private GameObject Lplayer;
    [SerializeField]
    private SceneManagment sceneManagment;
    [SerializeField]
    private GameObject loadingscreen,overlay;
    public bool eshu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            if (Lplayer != other.gameObject && Lplayer != null)
            {
                Lplayer=null;
                overlay.SetActive(false);
                loadingscreen.SetActive(true);
                sceneManagment.GoNextScene(eshu);
            }            
            else
                Lplayer = other.gameObject;

        }
    }
}
