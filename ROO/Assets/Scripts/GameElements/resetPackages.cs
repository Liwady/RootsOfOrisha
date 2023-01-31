using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPackages : MonoBehaviour
{
    public GameObject[] packages;
    private Vector3[] packagesOriginalPos;
    private void Awake()
    {
        packagesOriginalPos = new Vector3[packages.Length];
        for (int i = 0; i < packages.Length; i++)
            packagesOriginalPos[i] = packages[i].transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < packages.Length; i++)
            packages[i].transform.position = packagesOriginalPos[i];
    }
}
