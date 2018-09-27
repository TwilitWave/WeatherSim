using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : Interactable {

    public bool b_canClick;
    bool b_isClicking;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    public override void OnTouch()
    {
       
        if (b_canClick)
        {
            base.OnTouch();
            b_isClicking = true;
            sr_sprite.color = new Color(sr_sprite.color.r, sr_sprite.color.g, sr_sprite.color.b, 0.5f);
        }
       
    }

    public override void OnLeave()
    {
        if (b_canClick)
        {
            Debug.Log("Call: " + transform.name);
            base.OnLeave();
            b_isClicking = false;
            sr_sprite.color = new Color(sr_sprite.color.r, sr_sprite.color.g, sr_sprite.color.b, 1);
        }
       
    }
}
