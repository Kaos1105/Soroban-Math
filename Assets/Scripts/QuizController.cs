using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text upperText;
    private int upperValue;
    public Text lowerText;
    private int lowerValue;
    public int total;
    public int waitTime;
    void Start()
    {
        upperValue = 0;
        lowerValue = 0;
        StartCoroutine(GenValue());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void RandomValue()
    {
        var randomValue = Random.Range(0, total);
        upperValue = randomValue;
        lowerValue = total - upperValue;
    }

    IEnumerator GenValue()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            RandomValue();
            UpdateText();
        }
    }

    void UpdateText()
    {
        upperText.text = upperValue.ToString();
        lowerText.text = lowerValue.ToString();
    }

}
