using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Heal
{
    public int Hod = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Hod != 0)
        {
            Healing();
            Hod = 0;
        }
    }
}
