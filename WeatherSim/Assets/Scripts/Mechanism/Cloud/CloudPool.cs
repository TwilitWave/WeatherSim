using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPool : MonoBehaviour {
    public static CloudPool instance;

    private void Awake()
    {
        if(instance == null || instance != this)
        {
            instance = this;
        }
    }
    [SerializeField] GameObject CloudPrereb;
    public int i_PoolAmount;
    public List<Cloud> clouds = new List<Cloud>();
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
