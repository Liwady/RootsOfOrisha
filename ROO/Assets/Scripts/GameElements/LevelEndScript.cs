using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    private GameObject Lplayer;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject loadingscreen, overlay;
    public bool eshu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2"))
        {
            if (Lplayer != other.gameObject && Lplayer != null)
            {
                Lplayer = null;
                overlay.SetActive(false);
                if (loadingscreen != null)
                    loadingscreen.SetActive(true);
                gameManager.ResetChar();
                SetCompletedLevel();
                gameManager.GoToEshu();
            }
            else
                Lplayer = other.gameObject;

        }
    }
    private void SetCompletedLevel()
    { 
        if (LevelTracker.level < LevelTracker.completedLevel.Length)
            LevelTracker.completedLevel[LevelTracker.level] = true;
    }
}
