using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;

    public bool b_CanGenerateWind;
    public bool b_blowing;

    public float f_drag;


    private bool b_isTapping;

    private float f_currPos;
    private float f_prevPos;
    public float f_spd;

    Vector3[] va_Diff = new Vector3[5];
    Vector3 touchedPos = Vector3.zero;
    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }

    private void Start()
    {


    }

    private void Update()
    {
        if (b_CanGenerateWind && !b_blowing)
        {
            List<Touch> touches = InputHelper.GetTouches();

            if (touches.Count > 0)
            {

                touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touches[0].position.x, touches[0].position.y, 10));
                RaycastHit2D hit2D = Physics2D.Raycast(touchedPos, Camera.main.transform.forward);
                ParticleReceiver temp = null;
                if (hit2D)
                    temp = hit2D.collider.GetComponent<ParticleReceiver>();
                if (temp != null)
                {
                    switch (touches[0].phase)
                    {
                        case TouchPhase.Began:
                            {
                                f_currPos = touchedPos.x;
                                f_prevPos = touchedPos.x;
                                b_isTapping = true;
                                for (int i = 0; i < va_Diff.Length; i++)
                                {
                                    va_Diff[i] = CloudPool.instance.cloudList[i].transform.parent.position - touchedPos;
                                }

                                break;
                            }
                        case TouchPhase.Moved:
                            {
                                f_prevPos = f_currPos;
                                f_currPos = touchedPos.x;
                                f_spd = f_currPos - f_prevPos;
                                break;
                            }
                        case TouchPhase.Ended:
                            {
                                b_isTapping = false;
                                // set wind

                                break;
                            }
                        default:
                            break;
                    }
                }


            }
            else
            {

                b_isTapping = false;

            }
                
        }
        if (b_isTapping)
        {
            for (int i = 0; i < va_Diff.Length; i++)
            {
                Vector3 newPos = touchedPos + va_Diff[i];
                CloudPool.instance.cloudList[i].transform.parent.position = new Vector3(newPos.x, CloudPool.instance.cloudList[i].transform.parent.position.y, 0);
            }

        }

        if (b_blowing)
        {
            if (f_spd > 0)
            {
                f_spd -= f_drag * Time.deltaTime;
                
                for (int i = 0; i < va_Diff.Length; i++)
                {
                    CloudPool.instance.cloudList[i].transform.parent.position += new Vector3(f_spd, 0, 0) * Time.deltaTime;
                }
            }
            else
            {
                // cloud stop
                b_blowing = false;
                f_spd = 0;
            }

        }
    }
}
