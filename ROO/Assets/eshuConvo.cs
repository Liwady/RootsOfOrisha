using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class eshuConvo : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.StartCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void start()
    {

    }
}
