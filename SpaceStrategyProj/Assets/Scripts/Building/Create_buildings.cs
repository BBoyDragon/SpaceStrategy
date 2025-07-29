using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Create_buildings : MonoBehaviour
{
    public GameObject building;
    public GameObject building2;
    // Start is called before the first frame update
    void Start()
    {
        
        
        int Col = 10;
        int Cur = 1;
        int Sovp = 0;
        int Cur2 = 0;
        Vector3 New_Pos = Vector3.zero;
        List<Vector3> Uge = new List<Vector3>();

        while (Cur <= Col) {
            int randomx = UnityEngine.Random.Range(1, 11);
            int randomy = UnityEngine.Random.Range(1, 11);
            int randomz = UnityEngine.Random.Range(1, 11);
            New_Pos = new Vector3(randomx * 10, randomy * 10, randomz * 10);
            if (Cur != 1)
            {

                while (Cur2 < Cur-1)
                {
                    if (Uge[Cur2] == New_Pos)
                    {
                        Sovp = 1;
                        break;
                    }
                    Cur2++;
                }
                if (Sovp == 0)
                {
                    Uge.Add(New_Pos);
                    Instantiate(building, New_Pos, Quaternion.identity);
                    
                    Cur += 1;

                }
                Sovp = 0;
                Cur2 = 0;
            }
            else
            {
                Uge.Add(New_Pos);
                Instantiate(building, New_Pos, Quaternion.identity);
                
                Cur += 1;
                Cur2 = 0;
            }
        }






        Col = 10;
        Cur = 1;
        Sovp = 0;
        Cur2 = 0;

        while (Cur <= Col)
        {
            int randomx = UnityEngine.Random.Range(1, 11);
            int randomy = UnityEngine.Random.Range(1, 11);
            int randomz = UnityEngine.Random.Range(1, 11);
            New_Pos = new Vector3(randomx * 10, randomy * 10, randomz * 10);
            if (Cur != 1)
            {

                while (Cur2 < Cur + 9)
                {
                    if (Uge[Cur2] == New_Pos)
                    {
                        Sovp = 1;
                        break;
                    }
                    Cur2++;
                }
                if (Sovp == 0)
                {
                    Uge.Add(New_Pos);
                    Instantiate(building2, New_Pos, Quaternion.identity);

                    Cur += 1;

                }
                Sovp = 0;
                Cur2 = 0;
            }
            else
            {
                Uge.Add(New_Pos);
                Instantiate(building2, New_Pos, Quaternion.identity);

                Cur += 1;
                Cur2 = 0;
            }
        }

    }

    
}
