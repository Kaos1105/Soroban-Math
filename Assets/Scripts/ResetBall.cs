using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    // Start is called before the first frame update
    [System.NonSerialized]
    public GameObject[] normalBall;
    [System.NonSerialized]
    public GameObject[] oddBall;
    private Vector3[] listCorrectPosition;
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
        Reset();
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

    public void Reset()
    {
        if (normalBall.Length != 0 && oddBall.Length != 0)
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
        }

        mathController.mathValue = 0;
        mathController.UpdateValue();
        this.gameObject.transform.localScale = Vector3.Lerp(changeScale, originalScale, Time.deltaTime * 0.1f);
    }
}
