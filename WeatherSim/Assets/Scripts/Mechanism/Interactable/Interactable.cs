using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    protected SpriteRenderer sr_sprite;
    protected List<Touch> touches = new List<Touch>();
    public bool b_Touched;
    public Vector2[] v_onScreenPos = { Vector2.zero, Vector2.zero };

    // events
    public UnityEvent onTouch;
    public UnityEvent onStay;
    public UnityEvent onLeave;
    protected virtual void  Start()
    {
        sr_sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }
    public void AssignTouch(Touch _touch)
    {
        if (touches.Count < 2)
        {
            touches.Add(_touch);

        }

    }
    public void RemoveTouch(int i_touchID)
    {
        if (touches.Count > 0)
        {
            int deleteID = -1;
            for (int i = 0; i < touches.Count; i++)
            {
                if (touches[i].fingerId == i_touchID)
                {
                    deleteID = i;
                    break;
                }
            }
            if (deleteID != -1)
                touches.RemoveAt(deleteID);
        }


    }
    public void SetPosition(Touch _touch)

    {
        if (touches.Count <= 2)
            v_onScreenPos[_touch.fingerId] = _touch.position;
    }

    public virtual void OnTouch()
    {
        
        if (onTouch != null)
        {
            onTouch.Invoke();
        }
    }
    public virtual void OnStay()
    {
        if (onStay != null)
        {
            onStay.Invoke();
        }
    }
    public virtual void OnLeave()
    {
        if (onLeave != null)
        {
            onLeave.Invoke();
        }
    }

    public virtual void Interact()
    {

    }

    protected void UpdateSpriteLayer()
    {
        
    }
}
