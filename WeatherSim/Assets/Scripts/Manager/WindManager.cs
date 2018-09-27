using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour {
    public enum Direction {LEFT, RIGHT,DEFAULT};
    public bool b_CanGenerateWind;
    private Direction direction;
    private float Scale;
    public static WindManager instance;

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
        direction = Direction.DEFAULT;
        lr_PathOfWind = transform.GetChild(0).GetComponent<LineRenderer>();
        lr_PathOfWind.enabled = false;
    }
    public Direction GetDirection()
    {
        return direction;
    }
    public float GetScale()
    {
        return Scale;
    }
    public void SetWind(Direction direction, float Scale) {

        this.Scale = Scale;
        this.direction = direction;
        Debug.Log("Scale: " + Scale);
        Debug.Log("Direction: " + direction);
    }

    private void Update()
    {
        if (b_CanGenerateWind)
        {
            List<Touch> touches = InputHelper.GetTouches();
            if (touches.Count > 0)
            {
                foreach (Touch touch in touches)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            {
                               
                                lr_PathOfWind.SetPosition(0, new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y,0));
                                lr_PathOfWind.enabled = true;
                                break;
                            }
                        case TouchPhase.Moved:
                            {
                                lr_PathOfWind.SetPosition(1, new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y, 0));
                                break;
                            }
                        case TouchPhase.Ended:
                            {
                                //lr_PathOfWind.enabled = false;
                                float delta = lr_PathOfWind.GetPosition(1).x - lr_PathOfWind.GetPosition(0).x;
                                // set wind
                                SetWind(delta > 0 ? Direction.RIGHT : Direction.LEFT, Mathf.Abs(delta));
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }
    }
}
