using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    [SerializeField] float[] fa_scaleClamp;
    [SerializeField] float[] fa_heightClamp;
    [SerializeField] float f_buffer;
    private float f_scale;
    private float f_yPercent;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        f_yPercent = 1- (transform.position.y - fa_heightClamp[1]) / (fa_heightClamp[0] - fa_heightClamp[1]);
        f_scale = f_yPercent * (fa_scaleClamp[1] - fa_scaleClamp[0]) + fa_scaleClamp[0];
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * f_scale, 10 * Time.deltaTime);

        float pos = transform.position.y;
        pos = Mathf.Clamp(pos, fa_heightClamp[1], fa_heightClamp[0]);
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, pos, transform.position.z), Time.deltaTime);

        if (transform.position.y > fa_heightClamp[0] + f_buffer)
            transform.position = new Vector3(transform.position.x, fa_heightClamp[0]  + f_buffer, transform.position.z);
        if (transform.position.y < fa_heightClamp[1] - f_buffer)
            transform.position = new Vector3(transform.position.x, fa_heightClamp[1] - f_buffer, transform.position.z);


    }
}
