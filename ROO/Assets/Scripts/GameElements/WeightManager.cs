using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightManager : MonoBehaviour
{
    //[HideInInspector]
    public int weightOn1, weightOn2;

    [HideInInspector]
    public float weightOn1New, weightOn2New;


    private void Awake()
    {
        weightOn1 = 0;
        weightOn2 = 0;
    }
    public void balanceWeights()
    {
        weightOn1New  = weightOn1 - weightOn2;
        weightOn2New = weightOn2 - weightOn1;
    }
}
