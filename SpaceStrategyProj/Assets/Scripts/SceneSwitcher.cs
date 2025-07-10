using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName = "Scene1"; // Имя сцены, которую нужно загрузить

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
