using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {


   [SerializeField] Sprite[] sun_moon;
    SpriteRenderer sun_sprite;
    float f_angle = 53;
    // There are 5 levels
    [SerializeField] float[] fa_rotSpdLevel = {0.1f ,0.5f,1f,2f,3f,10f };
   [SerializeField] float f_rotSpeed;
    public bool b_IsPaused;
    public bool b_AtNight;
    int i_currentSpd = 5;
    // Use this for initialization
    void Start () {
        sun_sprite = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        f_rotSpeed = fa_rotSpdLevel[i_currentSpd];
	}
	
	// Update is called once per frame
	void Update () {
        if (!b_IsPaused)
        {
            
            transform.Rotate(new Vector3(0, 0, 1), f_rotSpeed * Time.deltaTime);
            float deltaAngle = transform.eulerAngles.z;
            if(deltaAngle > 180)
            {
                deltaAngle = 360 -deltaAngle;
            }
            float ratio = 1- deltaAngle / f_angle;

            // from left to right
            if(transform.rotation.eulerAngles.z >= f_angle && transform.rotation.eulerAngles.z < 360- f_angle)
            {
                Debug.Log(deltaAngle);
                transform.rotation = Quaternion.Euler(0, 0, -f_angle);
                b_AtNight = !b_AtNight;
                if (!b_AtNight)
                    VaporManager.instance.ModifyEnergy(ratio);
                sun_sprite.sprite = sun_moon[b_AtNight ? 1 : 0];
                sun_sprite.transform.parent.GetChild(b_AtNight ? 1 : 2).gameObject.SetActive(false);
                sun_sprite.transform.parent.GetChild(b_AtNight ? 2 : 1).gameObject.SetActive(true);

            }
        }
    }

    public void SpeedUp()
    {
        i_currentSpd++;
        if (i_currentSpd >= fa_rotSpdLevel.Length)
            i_currentSpd = fa_rotSpdLevel.Length-1;
        f_rotSpeed = fa_rotSpdLevel[i_currentSpd];

    }
    public void SpeedDown()
    {
        i_currentSpd--;
        if (i_currentSpd < 0)
            i_currentSpd = 0;
        f_rotSpeed = fa_rotSpdLevel[i_currentSpd];

    }
}
