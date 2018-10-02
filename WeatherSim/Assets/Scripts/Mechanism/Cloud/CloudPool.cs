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
    public List<Cloud> cloudList = new List<Cloud>();
    [SerializeField] Sprite[] cloudSprites;
    [SerializeField] Transform cloudParent;
    private int i_currentCloudID;
    void Start () {
        for (int i = 0; i < i_PoolAmount; i++)
        {
            GameObject temp = Instantiate(CloudPrereb, Vector3.one, Quaternion.identity, cloudParent);
            temp.SetActive(false);
            Cloud tempCloud = temp.transform.GetChild(0).GetComponent<Cloud>();
            tempCloud.i_ID = i;
            cloudList.Add(tempCloud);
        }

        InvokeRepeating("GenerateNewCloud", 0.1f, 6f);
    }
	
	// Update is called once per frame
	void Update () {
        foreach (Cloud item in cloudList)
        {
            if (item.gameObject.activeInHierarchy)
            {
                item.MoveSlightly();
            }
        }
    }
    // enable new cloud
    public void GenerateNewCloud()
    {
        cloudList[i_currentCloudID].transform.parent.gameObject.SetActive(true);
        int temp = Random.Range(0, cloudSprites.Length);
        cloudList[i_currentCloudID].InitCloud(cloudSprites[temp]);
        i_currentCloudID++;
        if (i_currentCloudID >= i_PoolAmount)
        {
            i_currentCloudID = 0;
        }

    }

    // put dead cloud to pool
    public void ReclycleCloud(int i_id)
    {
        cloudList[i_id].Reset();
        cloudList[i_id].transform.parent.gameObject.SetActive(false);
    }
}
