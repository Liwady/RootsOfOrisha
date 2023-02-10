using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightManager : MonoBehaviour
{
    public int weightOn1, weightOn2; //weights on plates
    public float weightOn1New, weightOn2New; //updated weight on plates

    private void Awake()
    {
        weightOn1 = 0;
        weightOn2 = 0;
    }

    public void balanceWeights() //calculates the new weight on the plates
    {
        weightOn1New = weightOn1 - weightOn2;
        weightOn2New = weightOn2 - weightOn1;
    }
}
