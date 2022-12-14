using UnityEngine;

public class Libra : MonoBehaviour
{
    [SerializeField]
    private Transform[] drawArmPoints;
    [SerializeField]
    private GameObject arm;

    void Start()
    {
        SetUpArm();
    }
    void SetUpArm()
    {
        arm.transform.position = new Vector3((drawArmPoints[0].position.x + drawArmPoints[1].position.x) / 2, (drawArmPoints[0].position.y + drawArmPoints[1].position.y) / 2, this.transform.position.z);
        arm.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(drawArmPoints[1].position.y - drawArmPoints[0].position.y, drawArmPoints[1].position.x - drawArmPoints[0].position.x) * Mathf.Rad2Deg);
    }
    private void Update()
    {
        SetUpArm();
    }
}
