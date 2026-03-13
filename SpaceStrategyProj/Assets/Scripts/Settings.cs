using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    public Button but_start;
    GameObject window;
    public Button but_end;
    public Button but_sound;
    public Button but_back;
    void Start()
    {
        but_back.onClick.AddListener(OnButtonClicked1);
        but_start.onClick.AddListener(OnButtonClicked2);
        but_end.onClick.AddListener(OnButtonClicked3);
        but_sound.onClick.AddListener(OnButtonClicked4);
    }

    void OnButtonClicked1()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void OnButtonClicked2()
    {
        window.SetActive(true);
    }
    void OnButtonClicked3()
    {
        window.SetActive(false);
    }
    void OnButtonClicked4()
    {
        return;
    }

}
