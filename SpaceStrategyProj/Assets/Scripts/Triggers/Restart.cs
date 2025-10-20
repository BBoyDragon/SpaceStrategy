using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    public Button a;
    void abobba()
    {
        SceneManager.LoadScene("SampleScene");
    }
    private void Start()
    {
        a.onClick.AddListener(abobba);
    }
    private void OnDestroy()
    {
        a?.onClick.RemoveListener(abobba);
    }
}
