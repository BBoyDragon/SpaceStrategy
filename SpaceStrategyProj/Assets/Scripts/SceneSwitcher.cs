using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName = "Scene1"; // ��� �����, ������� ����� ���������

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
