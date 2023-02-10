using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EyeScript : MonoBehaviour
{
    [SerializeField]
    private Transform zoudoo, kouboo;

    // Update is called once per frame
    void Update()
    {
        //middle of the two characters
        transform.position = Vector3.Lerp(kouboo.position, zoudoo.position, 0.5f);
        //y+4.5 so its above the characters
        transform.position= new Vector3(transform.position.x,transform.position.y+4.5f,transform.position.z);  
    }
}
