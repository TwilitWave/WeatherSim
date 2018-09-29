using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour {
    public static WindManager instance;

    // If player can generate wind
    public bool b_CanGenerateWind;
    // The direction of wind right: 1, left: -1
    public int direction;
    // The force of Wind
    public float Scale;

    // if there is a wind
    public bool b_blowing;
    // The drop down speed of Scale
    public float f_acceleraton;

    
    // The drawing hint of wind
    LineRenderer lr_PathOfWind;

    private void Awake()
    {
        if(instance == null || instance != this)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Scale = 0;
        direction = 0;
        lr_PathOfWind = transform.GetChild(0).GetComponent<LineRenderer>();
        lr_PathOfWind.enabled = false;
    }

    public void SetWind(int direction, float Scale) {
        b_blowing = true;
        this.Scale = Scale;
        this.direction = direction;
        Debug.Log("Scale: " + Scale);
        Debug.Log("Direction: " + direction);
    }

    private void Update()
    {
        if (b_CanGenerateWind && !b_blowing)
        {
            List<Touch> touches = InputHelper.GetTouches();

            if (touches.Count > 0)
            {
                foreach (Touch touch in touches)
                {
                    Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                    RaycastHit2D hit2D = Physics2D.Raycast(touchedPos, Camera.main.transform.forward);
                    if (hit2D.collider == null)
                    {
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                {

                                    lr_PathOfWind.SetPosition(0, new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y, -1));
                                    lr_PathOfWind.enabled = true;
                                    break;
                                }
                            case TouchPhase.Moved:
                                {
                                    lr_PathOfWind.SetPosition(1, new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y, -1));
                                    break;
                                }
                            case TouchPhase.Ended:
                                {
                                    //lr_PathOfWind.enabled = false;
                                    float delta = lr_PathOfWind.GetPosition(1).x - lr_PathOfWind.GetPosition(0).x;
                                    // set wind
                                    SetWind(delta > 0 ? 1 : -1, Mathf.Abs(delta));
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                   
                }
            }
        }

        if (b_blowing)
        {
            
            Scale -= Time.deltaTime * f_acceleraton;
            if(Scale < 0)
            {
                // wind stop
                Scale = 0;
                b_blowing = false;
                lr_PathOfWind.enabled = false;
            }
        }
    }
}
