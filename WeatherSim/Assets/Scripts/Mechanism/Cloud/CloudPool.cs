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
    int i_PoolAmount;
    public List<Cloud> cloudList = new List<Cloud>();
    [SerializeField] Sprite[] cloudSprites;
    [SerializeField] Transform cloudParent;
    [SerializeField] float f_CloudInitYOffset;
    private int i_currentCloudID;
    void Start () {
        i_PoolAmount = VaporManager.instance.transform.childCount;
        for (int i = 0; i < i_PoolAmount; i++)
        {
            GameObject temp = Instantiate(CloudPrereb, Vector3.one, Quaternion.identity, cloudParent);
            temp.SetActive(false);
            temp.name = "CloudReceiver_" + i;
            Cloud tempCloud = temp.transform.GetChild(0).GetComponent<Cloud>();
            tempCloud.i_ID = i;
            
            cloudList.Add(tempCloud);
        }

        for (int i = 0; i < i_PoolAmount; i++)
        {
            GenerateNewCloud(VaporManager.instance.transform.GetChild(i).position + new Vector3(0, f_CloudInitYOffset, 0));
        }

    }
	
	// Update is called once per frame
	void Update () {
        //foreach (Cloud item in cloudList)
        //{
        //    if (item.gameObject.activeInHierarchy)
        //    {
        //        item.MoveSlightly();
        //    }
        //}
    }
    // enable new cloud
    public void GenerateNewCloud(Vector3 _position)
    {
        cloudList[i_currentCloudID].transform.parent.gameObject.SetActive(true);
        int temp = Random.Range(0, cloudSprites.Length);
        cloudList[i_currentCloudID].InitCloud(cloudSprites[temp], _position);
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
        StartCoroutine(DelayGenerateNew(i_id));
    }

    IEnumerator DelayGenerateNew(int i_id)
    {
        yield return new WaitForSeconds(2f);
        GenerateNewCloud(VaporManager.instance.transform.GetChild(i_id).position + new Vector3(0, f_CloudInitYOffset, 0));
    }
}
