using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public InteractibleScript map;
    private void Start()
    {
        LevelTracker.completedLevel = new bool[6];
        for (int i = 0; i < LevelTracker.completedLevel.Length; i++)
            LevelTracker.completedLevel[i] = false; 
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            LevelTracker.completedLevel[0] = true;
            LevelTracker.level = 0;
            map.GetComponent<InteractibleScript>().MapScene();
        }
            
    }
}
