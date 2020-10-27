using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] normalBall;
    private GameObject[] oddBall;
    private MathController mathController;
    public TouchController touchController;

    private Vector3 originalScale;
    private Vector3 changeScale;
    void Start()
    {
        originalScale = this.gameObject.transform.localScale;
        changeScale = new Vector3(originalScale.x, originalScale.y / 2, originalScale.z);
        GameObject mathControllerObject = GameObject.FindWithTag("MathController");
        if (mathControllerObject != null)
        {
            mathController = mathControllerObject.GetComponent<MathController>();
        }
        if (mathControllerObject == null)
        {
            Debug.Log("Cannot find 'MathController' script");
        }
        normalBall = GameObject.FindGameObjectsWithTag("SorobanBall");
        oddBall = GameObject.FindGameObjectsWithTag("SorobanBallOdd");
    }

    // Update is called once per frame
    void Update()
    {
        if (touchController.GetGameObjectFromMouseDrag() == this.gameObject)
        {
            Reset();
            //direction = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        this.gameObject.transform.localScale = Vector3.Lerp(originalScale, changeScale, Time.deltaTime * 0.1f);
    }

    void Reset()
    {
        foreach (var ball in normalBall)
        {
            var ballController = ball.GetComponent<OddBallController>();
            ballController.Moving(-1f);
        }
        foreach (var ball in oddBall)
        {
            //Debug.Log(ball);
            var ballController = ball.GetComponent<OddBallController>();
            ballController.Moving(1f);
        }
        mathController.mathValue = 0;
        mathController.UpdateValue();
        this.gameObject.transform.localScale = Vector3.Lerp(changeScale, originalScale, Time.deltaTime * 0.1f);
    }
}
