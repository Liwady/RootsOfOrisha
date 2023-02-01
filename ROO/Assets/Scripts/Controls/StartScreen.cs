using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public InteractibleScript map;
    void Update()
    {
        if (Input.anyKeyDown)
            map.start = true;
    }
}
