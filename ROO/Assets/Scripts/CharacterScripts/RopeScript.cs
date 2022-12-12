using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Rigidbody hook;
    public GameObject linkPrefab;
    public int links = 8;
    public LineRenderer lineRenderer;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GenerateRope();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = links;
        lineRenderer.SetPosition(0, gameManager.character1.transform.position);
    }

    void GenerateRope()
    {
        Transform[] transforms = new Transform[links + 1];
        Rigidbody previousRB = hook;
        for (int i = 0; i < links; i++)
        {
            GameObject link = Instantiate(linkPrefab, transform);
            HingeJoint jointS = link.GetComponent<HingeJoint>();
            jointS.connectedBody = previousRB;
            transforms[i] = link.transform;
            previousRB = link.GetComponent<Rigidbody>();
        }
        HingeJoint joint = gameManager.character2.GetComponent<HingeJoint>();
        joint.connectedBody = previousRB;
    }
}
