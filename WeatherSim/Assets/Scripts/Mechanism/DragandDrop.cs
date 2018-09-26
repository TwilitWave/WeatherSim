using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour {
    public int speed = 4;
    public GameObject selectedObject;
    public float minPinchSpeed = 5.0f;
    public float variancelnDistances = 5.0f;
    public float range = 3.0f;
    private Vector2 curDist = new Vector2(0,0);
    private Vector2 preDist = new Vector2(0, 0);
    private float touchDelta = 0.0f;
    private float speedTouch0 = 0.0f;
    private float speedTouch1 = 0.0f;
    private Vector3 privousscale = new Vector3(0, 0, 0);
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount != 0 && inrange())
        {
            if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                curDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
                preDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition));
                touchDelta = curDist.magnitude - preDist.magnitude;
                speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
                speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
                privousscale = selectedObject.transform.localScale;
                Vector3 deltascale = new Vector3(speedTouch0 + speedTouch1, speedTouch0 + speedTouch1, 1);
                selectedObject.transform.localScale = deltascale + privousscale;
            }
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                curDist = Input.GetTouch(0).deltaPosition;
                selectedObject.transform.Translate(curDist);
            }
        }
    }
    bool inrange() {
        float deltax = Input.GetTouch(0).position.x - selectedObject.transform.position.x;
        float deltay = Input.GetTouch(0).position.y - selectedObject.transform.position.y;
        if (Mathf.Abs(deltax) <= range && Mathf.Abs(deltay) <= range) {
            return true;
        }
        return false;
    }
}
