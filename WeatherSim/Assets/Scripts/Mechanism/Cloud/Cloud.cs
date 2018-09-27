using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    // mass could be the size of the could
    public float f_Mass;
    public float f_AirDrag = 1f;
    public float f_MaxSpeed;
    float f_vertSpd;



	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (WindManager.instance.b_blowing)
        {
            f_vertSpd += WindManager.instance.direction *(WindManager.instance.Scale - f_AirDrag) * Time.deltaTime;
        }

		if(f_vertSpd > 0.01f || f_vertSpd < -0.01f)
        {
            f_vertSpd += -Mathf.Sign(f_vertSpd) * f_AirDrag * Time.deltaTime;

            if (f_vertSpd > f_MaxSpeed)
                f_vertSpd = f_MaxSpeed;
            if (f_vertSpd < - f_MaxSpeed)
                f_vertSpd = -f_MaxSpeed;

            transform.position += new Vector3(f_vertSpd, 0, 0);
        }
        else
        {
            f_vertSpd = 0;
        }
	}
}
