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
        if (canClick) //if the map script sets this to can click
        {
            canClick = false;
            if (button != 0)
                sceneManagment.PlayScene(button); //go to the given scene 
        }
    }
    public void SelectButton(int _buttonNr) //function called by the UI on click 
    {
        button = _buttonNr;
    }
}
