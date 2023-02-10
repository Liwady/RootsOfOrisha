// Import the UnityEngine library.
using UnityEngine;

// Create a new class called "Libra" that inherits from MonoBehaviour.
public class Libra : MonoBehaviour
{
    // Declare a serialized field "drawArmPoints" which is an array of the 2 transforms of the arm.
    [SerializeField]
    private Transform[] drawArmPoints;

    // Declare a serialized field "arm" which is the game object for the arm.
    [SerializeField]
    private GameObject arm;

    // This function is called when the script is first loaded.
    void Start()
    {
        // Call the function to set up the transforms and rotation of the arm.
        SetUpArm();
    }

    // This function sets up the transforms and rotation of the arm.
    void SetUpArm()
    {
        // Set the position of the arm game object to the midpoint of the two draw arm points.
        arm.transform.position = new Vector3((drawArmPoints[0].position.x + drawArmPoints[1].position.x) / 2, (drawArmPoints[0].position.y + drawArmPoints[1].position.y) / 2, this.transform.position.z);

        // Set the rotation of the arm game object to the angle between the two draw arm points.
        arm.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(drawArmPoints[1].position.y - drawArmPoints[0].position.y, drawArmPoints[1].position.x - drawArmPoints[0].position.x) * Mathf.Rad2Deg);
    }

    // This function is called every frame.
    private void Update()
    {
        // Call the function to set up the transforms and rotation of the arm.
        SetUpArm();
    }
}

