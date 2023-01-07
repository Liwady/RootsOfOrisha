using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireblob : MonoBehaviour
{
    [SerializeField]
    private float maxYPos;
    private Vector3 startPos;

    [SerializeField]
    private float speed;
    private bool ismovingUp = false;


    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ismovingUp)
        {
            MoveUp();
        }
        else if (!ismovingUp)
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        
        this.transform.Translate(Vector3.up *Time.deltaTime * speed);
        if (this.transform.position.y >= maxYPos)
        {
            ismovingUp = false;
        }
    }

    private void MoveDown()
    {
        this.transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (this.transform.position.y <= startPos.y)
        {
            ismovingUp = true;
        }
    }
}
