using System.Xml.XPath;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldReader : MonoBehaviour
{
    public TMP_InputField InputX;
    public TMP_InputField InputY;
    public TMP_InputField InputZ;
    public int xcord;
    public int ycord;
    public int zcord;

    public Vector3Int ReadInput()
    {
        string inputStringX = InputX.text;
        string inputStringY = InputY.text;
        string inputStringZ = InputZ.text;
        if (int.TryParse(inputStringX, out int result)) { xcord = result; }
        if (int.TryParse(inputStringY, out int resultt)) { ycord = resultt; }
        if (int.TryParse(inputStringZ, out int resulttt)) { zcord = resulttt; }
        return new Vector3Int(xcord, ycord, zcord);
    }
}