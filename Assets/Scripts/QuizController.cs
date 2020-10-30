using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<Vector3> correctPos;
    private List<Vector3> checkPos;
    void Start()
    {
        //Initialize value
        IsFirstHit = new Stack<bool>();
        anim = GetComponent<Animator>();
        correctPos = new List<Vector3>();
        checkPos = new List<Vector3>();
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

    private void GenValue()
    {
        while (anim.GetBool("IsReset"))
        {
            //Debug.Log("Random value");
            anim.SetBool(("IsReset"), false);
            RandomValue();
            UpdateText();
        }
    }

    void UpdateText()
    {
        upperText.text = upperValue.ToString();
        lowerText.text = lowerValue.ToString();
    }

    public IEnumerator CheckResult()
    {
        if (IsFirstHit.Count != 0)
        {
            if (IsFirstHit.Peek())
            {
                anim.SetBool("IsFirstHit", true);
                if (mathController.mathValue == upperValue)
                {
                    if (lowerValue == 0)
                    {
                        anim.SetBool("IsZero", true);
                    }
                    anim.SetBool("FirstValueTrue", true);
                    correctPos = resetBall.normalBall.Select(item => item.transform.position).ToList();
                }
                else if (upperValue == 0 && (mathController.mathValue == lowerValue))
                {

                    anim.SetBool("FirstValueTrue", true);
                    anim.SetBool("IsZero", true);
                }
                else
                {
                    anim.SetBool("FirstValueTrue", false);
                }
            }
            else
            {
                anim.SetBool("IsFirstHit", false);
                //Debug.Log("Second value");
                if (lowerValue == 0 || (mathController.mathValue == (upperValue + lowerValue)))
                {
                    anim.SetBool("SecondValueTrue", true);
                }
                else
                {
                    anim.SetBool("SecondValueTrue", false);
                    checkPos = resetBall.normalBall.Select(item => item.transform.position).ToList();
                    RevertState();
                }
            }
            yield return null;
        }
    }

    private void RevertState()
    {
        for (int i = 0; i < correctPos.Capacity; i++)
        {
            // Debug.Log("Correct" + correctPos[i]);
            // Debug.Log("After" + checkPos[i]);
            var oddBallController = resetBall.normalBall[i].GetComponent<OddBallController>();
            if ((correctPos[i].y - checkPos[i].y) > 0)
            {
                oddBallController.Moving(1f);
            }
            else if ((correctPos[i].y - checkPos[i].y) < 0)
            {
                oddBallController.Moving(-1f);
            }
        }
    }

    private void StateTrue_NA()
    {
    }

    public void ResetState()
    {
        anim.SetBool("SecondValueTrue", false);
        anim.SetBool("FirstValueTrue", false);
        anim.SetBool("IsFirstHit", false);
        anim.SetBool("IsZero", false);
        IsFirstHit.Clear();
        correctPos.Clear();
        checkPos.Clear();
        if (!anim.GetBool("IsBegin"))
        {
            resetBall.Reset();
        }
        GenValue();
    }

}
