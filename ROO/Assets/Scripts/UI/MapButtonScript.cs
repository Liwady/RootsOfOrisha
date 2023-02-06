using UnityEngine;
using UnityEngine.SceneManagement;

public class MapButtonScript : MonoBehaviour
{
    [SerializeField]
    private SceneManagment sceneManagment;
    public static bool canClick;
    private int button;
    private void Update()
    {
        if (canClick)
        {
            canClick = false;
            if (button != 0)
                sceneManagment.PlayScene(button);
        }
    }
    public void SelectButton(int _buttonNr)
    {
        button = _buttonNr;
    }
}
