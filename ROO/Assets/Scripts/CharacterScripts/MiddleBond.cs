using Unity.VisualScripting;
using UnityEngine;

public class MiddleBond : MonoBehaviour
{
    public GameObject startPosition;
    public GameObject endPosition;
    float rot;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //position
        transform.position = Vector3.Lerp(startPosition.transform.position, endPosition.transform.position, 0.5f);
       
        // rotatation
        if (startPosition.transform.position.y > endPosition.transform.position.y)
            rot = Vector3.Angle(endPosition.transform.position - startPosition.transform.position, transform.position) * -1;
        else
            rot = Vector3.Angle(endPosition.transform.position - startPosition.transform.position, transform.position) * 1;
        transform.eulerAngles = new Vector3(0, 0, rot);

        // scale
        Vector3 scale = new Vector3(1, 1, 1)
        {
            x = Vector3.Distance(startPosition.transform.position, endPosition.transform.position)-2
        };
        transform.localScale = scale;
    }
}
