using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStackController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rigidBody;

    OddBallController thisBallController;

    QuizController quizController;

    void Start()
    {
        thisBallController = this.gameObject.GetComponent<OddBallController>();

        GameObject quizControllerObject = GameObject.FindWithTag("QuizController");

        if (quizControllerObject != null)
        {
            quizController = quizControllerObject.GetComponent<QuizController>();
        }
        if (quizControllerObject == null)
        {
            Debug.Log("Cannot find 'QuizController' script");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "SorobanBall":
                if (other.gameObject != null)
                {
                    //Debug.Log("This:" + this.gameObject);
                    //Debug.Log("Other:" + other.gameObject);
                    var otherBallController = other.gameObject.GetComponent<OddBallController>();
                    //Debug.Log("Controller: " + otherBallController);
                    //Debug.Log("This Controller:" + thisBallController.direction.y);
                    //Debug.Log("Other Controller:" + otherBallController.direction.y);
                    otherBallController.direction = thisBallController.direction;
                    otherBallController.Moving(thisBallController.direction.y);
                }

                break;
            case "CollideCube":
                if (quizController)
                {
                    quizController.IsFirstHit.Push(true);
                    Debug.Log(quizController.IsFirstHit.Peek() + "Hit bar");
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            // case "SorobanBall":
            //     isFinishedMove = true;
            //     break;
            // case "CollideCube":
            //     isFirstHit = false;
            //     break;
            default:
                break;
        }
    }

}
