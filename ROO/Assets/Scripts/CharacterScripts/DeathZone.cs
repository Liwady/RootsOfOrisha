using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private PlayerManager playerManager;
    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            playerManager.RespawnCharacters();
        }
            
    }
}
