using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float yMin, yMax;
}

public class OddBallController : MonoBehaviour
{
    public Boundary boundary;
    // Start is called before the first frame update
    private Rigidbody rigidBody;
    public long value;
    private Touch touch;
    private TouchController touchController;

    [System.NonSerialized]
    public Vector2 direction;
    private float originY = 0f;
    private MathController mathController;
    private QuizController quizController;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        GameObject mathControllerObject = GameObject.FindWithTag("MathController");
        GameObject touchControllerObject = GameObject.FindWithTag("TouchController");
        GameObject quizControllerObject = GameObject.FindWithTag("QuizController");

        if (mathControllerObject != null)
        {
            mathController = mathControllerObject.GetComponent<MathController>();
        }
        if (mathControllerObject == null)
        {
            Debug.Log("Cannot find 'MathController' script");
        }

        if (touchControllerObject != null)
        {
            touchController = touchControllerObject.GetComponent<TouchController>();
        }
        if (touchControllerObject == null)
        {
            Debug.Log("Cannot find 'TouchController' script");
        }

        if (quizControllerObject != null)
        {
            quizController = quizControllerObject.GetComponent<QuizController>();
        }
        if (quizControllerObject == null)
        {
            Debug.Log("Cannot find 'QuizController' script");
        }
        StartCoroutine(BallCalculating());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {

    }

    IEnumerator BallCalculating()
    {
        while (true)
        {
            if (touchController.GetDirection().y != 0)
                direction = touchController.GetDirection();
            if (touchController.GetGameObjectFromMouseDrag() == this.gameObject)
            {
                if (touchController.pointerUp)
                {
                    Debug.Log(direction);
                    Moving(direction.y);
                    if (quizController)
                    {
                        Debug.Log("Before Result");
                        //touchController.gameObject.SetActive(false);
                        yield return new WaitForSeconds(quizController.waitTime);
                        quizController.UpdateResult();
                        Debug.Log("Updated Result");
                        //touchController.gameObject.SetActive(true);
                    }
                }
            }
            yield return null;
        }
    }

    public void Moving(float yDirection)
    {
        if (quizController)
        {
            quizController.IsFirstHit.Push(false);
            Debug.Log(quizController.IsFirstHit.Peek() + "Hit ball");
        }
        if (yDirection > 0)
        {
            // Vector3 interpolationVector = Vector3.Lerp(this.gameObject.transform.position, new Vector3
            //      (
            //          0.0f,
            //          boundary.yMax,
            //          0.0f
            //      ), Time.deltaTime * movingSpeed);
            // rigidBody.MovePosition(interpolationVector);
            rigidBody.MovePosition(new Vector3
              (
                  0.0f,
                  boundary.yMax,
                  0.0f
              ));
            if (originY <= 0)
            {
                mathController.mathValue += value;
                mathController.UpdateValue();
            }

        }
        else if (yDirection < 0)
        {
            rigidBody.MovePosition(new Vector3
               (
                   0.0f,
                   boundary.yMin,
                   0.0f
               ));
            if (originY >= 0)
            {
                mathController.mathValue -= value;
                mathController.UpdateValue();
            }
        }
        if (yDirection != 0)
            originY = yDirection;
    }
}

