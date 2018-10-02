using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : Interactable {
    
    // To check if this object is scalable
    [Header("Scale function")]
    public bool b_CanScaleUp;
    public float f_scaleThreshold = 2f;
    public float[] fa_scaleClamp = {0.8f, 1.2f};

    public bool b_canMove;
    float f_currDist;
    float f_preDist;

    float ScreenWidth;
    float ScreenHeight;

    // difference from center to pointer
    Vector3 v_diff = Vector3.zero;
	// Use this for initialization
	protected override void Start () {
        base.Start();

    }
    public override void OnTouch()
    {
        base.OnTouch();
        Vector3 pos = Camera.main.ScreenToWorldPoint(v_onScreenPos[0]);

        v_diff = new Vector3(pos.x, pos.y,0) - transform.position;
    }

    // Update is called once per frame
    void Update () {

        if (b_Touched)
        {

            if (b_canMove)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(v_onScreenPos[0]);
                pos.z = 0;
                transform.position = pos - v_diff;
                  
                if (transform.position.y > Camera.main.orthographicSize)
                    transform.position = new Vector3(transform.position.x, Camera.main.orthographicSize, 0);
                if (transform.position.y < -Camera.main.orthographicSize)
                    transform.position = new Vector3(transform.position.x, -Camera.main.orthographicSize, 0);
                if (transform.position.x > Camera.main.orthographicSize * Camera.main.aspect)
                    transform.position = new Vector3( Camera.main.orthographicSize * Camera.main.aspect,transform.position.y, 0);
                if (transform.position.x < -Camera.main.orthographicSize * Camera.main.aspect)
                    transform.position = new Vector3( -Camera.main.orthographicSize * Camera.main.aspect, transform.position.y, 0);

            }
            // can not scale
            if (b_CanScaleUp)
            {
                // enable two finger function
                if (touches.Count > 1)
                {

                    f_preDist = f_currDist;
                    f_currDist = Vector2.Distance(v_onScreenPos[0], v_onScreenPos[1]);
                    float delta = f_currDist - f_preDist;
                    if(delta>0 && delta> f_scaleThreshold)
                    {
                        float speed = delta - f_scaleThreshold;
                        // scale up
                        if(transform.localScale.x < fa_scaleClamp[1])
                        {
                            transform.localScale = new Vector3(transform.localScale.x + (speed * Time.deltaTime), transform.localScale.y + (speed * Time.deltaTime), 1);
                        }
                        else
                        {
                            transform.localScale = new Vector3(fa_scaleClamp[1], fa_scaleClamp[1], 1);
                        }
                    }
                    if (delta < 0 && delta < -f_scaleThreshold)
                    {
                        float speed = delta + f_scaleThreshold;

                        if (transform.localScale.x > fa_scaleClamp[0])
                        {
                            transform.localScale = new Vector3(transform.localScale.x + (speed * Time.deltaTime), transform.localScale.y + (speed * Time.deltaTime), 1);
                        }
                        else
                        {
                            transform.localScale = new Vector3(fa_scaleClamp[0], fa_scaleClamp[0], 1);
                        }
                    }
                }

            
            }

        }
        
    }
}
