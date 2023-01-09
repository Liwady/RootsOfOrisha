using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private GameManager gameManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
            gameManager.RespawnCharacters();
    }
}
