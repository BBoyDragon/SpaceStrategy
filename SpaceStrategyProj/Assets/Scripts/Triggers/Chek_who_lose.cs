using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chek_who_lose : MonoBehaviour
{
    public ControllerController controller;
    public bool Are_some_ships_alive1()
    {
        bool ans = false;
        for (int i = 0; i < controller.player1shipControllers.Count; i++)
        {
            if (controller.player1shipControllers[i].gameObject.activeSelf)
            {
                ans = true;
                break;
            }
        }
        return ans;
    }
    public bool Are_some_ships_alive2()
    {
        bool ans = false;
        for (int i = 0; i < controller.player2shipControllers.Count; i++)
        {
            if (controller.player2shipControllers[i].gameObject.activeSelf)
            {
                ans = true;
                break;
            }
        }
        return ans;
    }
    public void is_somebody_lose()
    {
        if (!Are_some_ships_alive1())
        {
            SceneManager.LoadScene("Scene3");
        }
        else if (!Are_some_ships_alive2())
        {
            SceneManager.LoadScene("Scene3");
        }
    }


    private void Update()
    {
        is_somebody_lose();
    }


}
