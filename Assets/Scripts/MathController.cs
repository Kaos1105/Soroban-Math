using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathController : MonoBehaviour
{
    // Start is called before the first frame update
    public long mathValue = 0;
    public Text guiText;
    void Start()
    {
        mathValue = 0;
        UpdateValue();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(mathValue);

    }

    public void UpdateValue()
    {
        guiText.text = mathValue.ToString();
    }
}
