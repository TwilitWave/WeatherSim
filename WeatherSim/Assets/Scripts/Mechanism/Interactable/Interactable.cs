using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    protected List<Touch> touches = new List<Touch>();
    public bool b_Touched;
    public Vector2[] v_onScreenPos = { Vector2.zero,Vector2.zero};
    public enum InteractType
    {
        Tap,
        Drag
    }
	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AssignTouch(Touch _touch)
    {
        if (touches.Count < 2)
            touches.Add(_touch);
    }
    public void RemoveTouch(Touch _touch)
    {
        if(touches.Count>0)
        touches.Remove(_touch);
    }
    public void SetPosition(Vector2 _pos)
    {
        v_onScreenPos[0] = _pos;
    }

    public virtual void Interact()
    {

    }
}
