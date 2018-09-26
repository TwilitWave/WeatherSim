using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance;
    private void Awake()
    {
        if (instance == null || instance!=this)
        {
            instance = this;
        }
    }
    public Interactable InteractingObj;

    int i_touchCount;
    List<Touch> l_doubleTouchMovements = new List<Touch>();
    // store the previous and current distance among two finger
    float[] fa_distATouches = { 0, 0 };

    [SerializeField]
    float f_scaleThreshold = 2f;

    // Use this for initialization
    void Start () {
        InteractingObj = null;

    }
	
	// Update is called once per frame
	void Update () {
        List<Touch> touches = InputHelper.GetTouches();

        if (touches.Count > 0)
        {
            foreach (Touch touch in touches)
            {
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        {
                            if (i_touchCount < 2)
                            {
                                l_doubleTouchMovements.Add(touch);
                                i_touchCount++;
                            }


                            RaycastHit2D hit2D = Physics2D.Raycast(touchedPos, Camera.main.transform.forward);
                            if (hit2D.collider != null && hit2D.transform.GetComponent<Interactable>()!=null)
                            {
                                // assign current interactable obj
                                InteractingObj = hit2D.transform.GetComponent<Interactable>();
                                InteractingObj.AssignTouch(touch);
                            }
                            break;
                        }
                    case TouchPhase.Moved:
                        {

                            break;
                        }
                    case TouchPhase.Ended:
                        {
                            l_doubleTouchMovements.Remove(touch);
                            i_touchCount--;

                            InteractingObj = null;
                            break;
                        }

                }
            }
        }

        if(i_touchCount == 2)
        {
            // assign old distance
            fa_distATouches[0] = fa_distATouches[1];
            // new distance
            fa_distATouches[1] = Vector2.Distance(l_doubleTouchMovements[0].position, l_doubleTouchMovements[1].position);
            float deltaDist = fa_distATouches[1] - fa_distATouches[0];

            // scale up
            if(deltaDist > 0 && deltaDist> f_scaleThreshold)
            {
                Debug.Log("Call scale up");
            }
            if(deltaDist < 0 && deltaDist < -f_scaleThreshold)
            {
                Debug.Log("Call scale down");
            }

        }
        else
        {
            // reset all
            l_doubleTouchMovements.Clear();
            fa_distATouches[0] = 0;
            fa_distATouches[1] = 0;
        }
}
}
