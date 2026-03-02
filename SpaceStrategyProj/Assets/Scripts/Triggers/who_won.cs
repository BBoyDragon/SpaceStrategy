using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class who_won : MonoBehaviour
{
    public Chek_who_lose who_lost;
    public TMP_Text text;
    void Start()
    {
        who_lost = FindObjectOfType<Chek_who_lose>();
        if (who_lost.first_won == true)
        {
            text.text = "First player has won!";
        } else
        {
            text.text = "Second player has won!";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
