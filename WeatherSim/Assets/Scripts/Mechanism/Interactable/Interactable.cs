using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    protected List<Touch> touches = new List<Touch>();
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

    public virtual void Interact()
    {

    }
}
