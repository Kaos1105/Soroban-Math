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
    public TouchController touchController;
    public Vector2 direction;
    private float originY;
    private MathController mathController;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        GameObject mathControllerObject = GameObject.FindWithTag("MathController");
        if (mathControllerObject != null)
        {
            mathController = mathControllerObject.GetComponent<MathController>();
        }
        if (mathControllerObject == null)
        {
            Debug.Log("Cannot find 'MathController' script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (touchController.GetDirection().y != 0)
            direction = touchController.GetDirection();
        if (touchController.GetGameObjectFromMouseDrag() == this.gameObject)
        {
            Moving(direction.y);
        }
    }
    void FixedUpdate()
    {
    }

    public void Moving(float yDirection)
    {
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
        //if (yDirection != 0)
        originY = yDirection;
    }
}

