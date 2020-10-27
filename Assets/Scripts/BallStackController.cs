using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStackController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rigidBody;
    OddBallController thisBallController;

    void Start()
    {
        thisBallController = this.gameObject.GetComponent<OddBallController>();
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
                    Debug.Log("This:" + this.gameObject);
                    Debug.Log("Other:" + other.gameObject);
                    var otherBallController = other.gameObject.GetComponent<OddBallController>();
                    //Debug.Log("Controller: " + otherBallController);
                    Debug.Log("This Controller:" + thisBallController.direction.y);
                    Debug.Log("Other Controller:" + otherBallController.direction.y);
                    otherBallController.direction = thisBallController.direction;
                    otherBallController.Moving(thisBallController.direction.y);
                }
                break;
            default:
                break;
        }
        // if (other.gameObject == upperObject)
        // {
        //     mathController.mathValue += value;
        //     Debug.Log(mathController.mathValue);
        // }
        // else if (other.gameObject == lowerObject)
        // {
        //     mathController.mathValue -= value;
        //     Debug.Log(mathController.mathValue);
        // }
    }
}
