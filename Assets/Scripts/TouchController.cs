using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Camera targetCamera;
    public float smoothing;
    private Vector2 origin;
    private RaycastHit originHit;
    private bool hit;
    private Vector2 direction;
    private int pointerID;
    private Vector2 smoothDirection;

    [System.NonSerialized]
    public bool pointerUp;

    string message;
    private bool touched;
    void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            Ray originRay = targetCamera.ScreenPointToRay(Input.mousePosition);
            hit = Physics.Raycast(originRay, out originHit);
            touched = true;
            pointerID = data.pointerId;
            origin = data.position;
            pointerUp = false;
        }
    }
    public void OnDrag(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            Vector2 currentPosition = data.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
            pointerUp = true;
        }
    }
    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            direction = Vector2.zero;
            touched = false;
            hit = false;
            pointerUp = false;
        }
    }
    void Update()
    {

    }
    public Vector2 GetDirection()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
        return smoothDirection;
    }

    public Vector2 ReturnOrigin()
    {
        return origin;
    }
    public GameObject GetGameObjectFromMouseDrag()
    {
        //Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);

        if (hit)
        {
            if (originHit.collider.gameObject)
            {
                return originHit.collider.gameObject;
            }
        }

        return null;
    }
    public GameObject GetGameObjectFromMouseClick()
    {
        if (hit)
        {
            if (originHit.collider.gameObject)
            {
                return originHit.collider.gameObject;
            }
        }

        return null;
    }
}
