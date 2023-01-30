using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPackages : MonoBehaviour
{
    public GameObject[] packages;
    private Transform[] packagesOriginalPos;
    private void Awake()
    {
        packagesOriginalPos = new Transform[packages.Length];
        for (int i = 0; i < packages.Length; i++)
            packagesOriginalPos[i] = packages[i].transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < packages.Length; i++)
            packages[i].transform.position = packagesOriginalPos[i].transform.position;
    }
}
