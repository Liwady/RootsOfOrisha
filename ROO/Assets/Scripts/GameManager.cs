using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfFruit;
    public TMP_Text fruitText;

    private void Awake()
    {
        amountOfFruit = 0;
    }
    
}
