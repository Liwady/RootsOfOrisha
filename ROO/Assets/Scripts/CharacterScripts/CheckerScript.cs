using UnityEngine;

public class CheckerScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    public bool isTall;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (isTall)
            gameObject.transform.position = new Vector3(gameManager.character2.transform.position.x, gameManager.character2.transform.position.y + 1.5f, 1);
        else
            gameObject.transform.position = new Vector3(gameManager.character1.transform.position.x, gameManager.character1.transform.position.y + 0.5f, 1);
    }

}
