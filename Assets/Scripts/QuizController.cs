using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// public class ConstState
// {
//     public const string FALSE_NA = "Case FALSE_NA";
//     public const string TRUE_NA = "Case TRUE_NA";
//     public const string TRUE_TRUE = "Case TRUE_TRUE";
//     public const string TRUE_FALSE = "Case TRUE_FALSE";
//     public const string RESET1_1 = "Case RESET1_1";
//     public const string RESET0_1 = "Case RESET0_1";
// }
public class QuizController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text upperText;
    private int upperValue;
    public Text lowerText;
    private int lowerValue;
    public int total;
    public float waitTime;
    public MathController mathController;
    [System.NonSerialized]
    public Stack<bool> IsFirstHit;
    ResetBall resetBall;
    [System.NonSerialized]
    public Animator anim;
    void Start()
    {
        //Initialize value
        IsFirstHit = new Stack<bool>();
        anim = GetComponent<Animator>();
        upperValue = 0;
        lowerValue = 0;
        GameObject resetBallObject = GameObject.FindWithTag("ResetTable");
        if (resetBallObject != null)
        {
            resetBall = resetBallObject.GetComponent<ResetBall>();
        }
        if (resetBallObject == null)
        {
            Debug.Log("Cannot find 'ResetBall' script");
        }
        StartCoroutine(GenValue());
        ResetState();
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
        while (anim.GetBool("IsReset"))
        {
            anim.SetBool(("IsReset"), false);
            RandomValue();
            UpdateText();
        }
        yield return null;
    }

    void UpdateText()
    {
        upperText.text = upperValue.ToString();
        lowerText.text = lowerValue.ToString();
    }

    public void CheckResult()
    {
        if (IsFirstHit.Count != 0)
        {
            if (IsFirstHit.Peek())
            {
                if (mathController.mathValue == upperValue)
                {
                    anim.SetBool("IsFirstHit", true);
                    anim.SetBool("FirstValueTrue", true);
                    if (lowerValue == 0)
                    {
                        anim.SetBool("IsFirstHit", false);
                        anim.SetBool("FirstValueTrue", true);
                        anim.SetBool("SecondValueTrue", false);
                    }
                }
                else if (upperValue == 0 && (mathController.mathValue == lowerValue))
                {
                    anim.SetBool("IsFirstHit", false);
                    anim.SetBool("FirstValueTrue", true);
                    anim.SetBool("SecondValueTrue", false);
                }
                else
                {
                    anim.SetBool("IsFirstHit", true);
                    anim.SetBool("FirstValueTrue", false);
                }
            }
            else
            {
                anim.SetBool("IsFirstHit", false);
                Debug.Log("Second value");
                if (lowerValue == 0 || (mathController.mathValue == (upperValue + lowerValue)))
                {
                    anim.SetBool("SecondValueTrue", true);
                }
                else
                {
                    anim.SetBool("SecondValueTrue", false);
                }
            }
        }
    }

    // public void changeEffect(bool result)
    // {
    //     if (!isFirstAttempted && !result && !isFinished)
    //     {
    //         upperText.color = Color.Lerp(upperText.color, Color.red, lerpSpeedColor);
    //         //upperText.color = Color.Lerp(Color.red, originalColor, lerpSpeedColor);
    //     }
    //     else if (isFirstAttempted && result && !isFinished)
    //         upperText.color = Color.Lerp(upperText.color, Color.green, lerpSpeedColor);
    //     else if (isFirstAttempted && !result && !isFinished)
    //     {
    //         lowerText.color = Color.Lerp(upperText.color, Color.red, lerpSpeedColor);
    //         //lowerText.color = Color.Lerp(Color.red, originalColor, lerpSpeedColor);
    //     }
    //     else if (isFirstAttempted && result && isFinished)
    //         lowerText.color = Color.Lerp(upperText.color, Color.green, lerpSpeedColor);
    //     if (result && isFinished)
    //     {
    //         upperText.color = Color.white;
    //         lowerText.color = Color.white;
    //     }
    // }
    public void UpdateResult()
    {
        CheckResult();
        //changeEffect(result);
    }

    public void ResetState()
    {
        anim.SetBool("SecondValueTrue", false);
        anim.SetBool("FirstValueTrue", false);
        anim.SetBool("IsFirstHit", false);
        IsFirstHit.Clear();
        if (!anim.GetBool("IsBegin"))
        {
            resetBall.Reset();
        }
    }

}
