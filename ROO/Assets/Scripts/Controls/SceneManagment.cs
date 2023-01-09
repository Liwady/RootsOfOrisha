using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public GameObject soundon, soundoff;

    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void Quit()
    {
        Debug.Log("test");
        Application.Quit();
    }
    public void Switch()
    {
        if (soundon.activeInHierarchy)
        {
            soundon.SetActive(false);
            soundoff.SetActive(true);
        }
        else
        {
            soundon.SetActive(true);
            soundoff.SetActive(false);
        }
    }
}
