using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Add this function to the image or button's OnClick event in the Unity Inspector
    public void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }
}
