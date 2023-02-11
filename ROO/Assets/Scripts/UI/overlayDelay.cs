using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlayDelay : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup overlay;
    [SerializeField]
    private float overlayTimer;
    private bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        overlay.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered)
        {
            if (overlayTimer < 0)
            {
                overlay.alpha = 1;
                triggered = true;
            }
            else
                overlayTimer -= Time.deltaTime;
        }

    }
}
