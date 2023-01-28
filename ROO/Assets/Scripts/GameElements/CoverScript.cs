using UnityEngine;
using System.Collections.Generic;

public class CoverScript : MonoBehaviour
{
    [SerializeField]
    private float duration;
    private float time;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private bool change;
    private bool vanishing;
    private int numPlayers;
    private void Update()
    {
        if (change)
            ChangeColor(vanishing);
    }
    private void ChangeColor(bool st)
    {
        if (st)
            spriteRenderer.color = Color.Lerp(Color.white, Color.clear, time);
        else
            spriteRenderer.color = Color.Lerp(Color.clear, Color.white, time);

        if (time < 1)
            time += Time.deltaTime / duration;
        else 
        {
            change = false;
            time = 0;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            if (numPlayers == 0)
            {
                change = true;
                vanishing = true;
            }
            numPlayers++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            numPlayers--;
            if (numPlayers == 0)
            {
                change = true;
                vanishing = false;
            }
        }
    }
}