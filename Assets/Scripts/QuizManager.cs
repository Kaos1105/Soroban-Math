using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StateConst
{
    public const string normal = "Normal";
    public const string true_na = "True_NA";
    public const string true_false = "True_False";
    public const string true_true = "True_True";
    public const string false_na = "False_NA";
}
public class QuizManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] listAudio;
    public QuizController quizController;
    public Text upperText;
    public Text lowerText;
    public TouchController touchController;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeEffect();
    }

    public void ChangeEffect()
    {
        if (quizController.anim.GetCurrentAnimatorStateInfo(0).IsName(StateConst.true_na))
        {
            upperText.color = Color.green;
        }
        else if (quizController.anim.GetCurrentAnimatorStateInfo(0).IsName(StateConst.true_true))
        {
            lowerText.color = Color.green;
        }
        else if (quizController.anim.GetCurrentAnimatorStateInfo(0).IsName(StateConst.true_false))
        {
            lowerText.color = Color.red;
        }
        else if (quizController.anim.GetCurrentAnimatorStateInfo(0).IsName(StateConst.false_na))
        {
            upperText.color = Color.red;
        }
        else if (quizController.anim.GetCurrentAnimatorStateInfo(0).IsName(StateConst.normal))
        {
            upperText.color = Color.white;
            lowerText.color = Color.white;
        }
    }

    void OnPlayAudio(bool isTrue)
    {

    }
}
