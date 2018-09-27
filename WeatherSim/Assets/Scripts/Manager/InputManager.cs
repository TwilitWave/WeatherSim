using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;
    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }
    public Interactable InteractingObj;

    [SerializeField]
    float f_scaleThreshold = 2f;

    // Use this for initialization
    void Start()
    {
        InteractingObj = null;

    }

    // Update is called once per frame
    void Update()
    {

        List<Touch> touches = InputHelper.GetTouches();
        if (touches.Count > 0)
        {
            foreach (Touch touch in touches)
            {
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                RaycastHit2D hit2D = Physics2D.Raycast(touchedPos, Camera.main.transform.forward);
                if (hit2D.collider != null && hit2D.transform.GetComponent<Interactable>() != null)
                {
                    // assign current interactable obj
                    InteractingObj = hit2D.transform.GetComponent<Interactable>();
                    InteractingObj.b_Touched = true;
                }
                if(InteractingObj != null)
                {
                    switch (touch.phase)
                    {

                        case TouchPhase.Began:
                            {

                                InteractingObj.AssignTouch(touch);
                                InteractingObj.SetPosition(touch);
                                InteractingObj.OnTouch();
                                break;
                            }
                        case TouchPhase.Moved:
                            {
                                InteractingObj.SetPosition(touch);
                                InteractingObj.OnStay();
                                break;
                            }
                        case TouchPhase.Ended:
                            {
                                InteractingObj.OnLeave();

                                InteractingObj.RemoveTouch(touch.fingerId);
                                InteractingObj.b_Touched = false;
                                InteractingObj = null;
                                break;
                            }

                    }
                }
              
            }
        }

      
    }
}
