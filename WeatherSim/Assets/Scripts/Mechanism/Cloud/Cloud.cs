using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public int i_ID;
    [SerializeField] SpriteRenderer sr_FillCloud;
   SpriteRenderer sr_bgSprite;
    [SerializeField] private float f_WaterVolume;
    [Header("Cloud Property")]
    // mass could be the size of the could
    public float f_Mass;

    public float f_MaxSpeed;


    [Header("Cloud Grow")]
    public bool b_IsCloudFull;
    public bool b_CanRain;
    // the water volume required to full the cloud
    public float f_CloudFullVolume;
    // the water volume allowed the cloud to rain
    public float f_RainFullVolume;
    // current water volume for the cloud
    [Header("Rain")]
    [SerializeField] private GameObject rainVFX;
    private float f_rainSpeed = 0.5f;
    private bool b_isRaining;
    private bool b_cloudDie;
    private void OnEnable()
    {
        sr_bgSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
        sr_FillCloud.material.SetFloat("_Fill", 0f);
        if (!b_IsCloudFull)
        {
            transform.localScale = new Vector3(0.01f, 0.01f, 1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Rain();
        Dissipate();
    }
    public void InitCloud(Sprite _sprite, Vector3 _pos)
    {  
        sr_bgSprite.sprite = _sprite;
        sr_FillCloud.sprite = _sprite;

        transform.parent.GetComponent<BoxCollider2D>().size = new Vector2(_sprite.rect.width*1.7f/280f, transform.parent.GetComponent<BoxCollider2D>().size.y);

        transform.parent.position = new Vector3(_pos.x, _pos.y + Random.Range(-1.5f,1), 0);
        //transform.localPosition = Vector3.zero;
        transform.parent.gameObject.layer = LayerMask.NameToLayer("Cloud_" + i_ID);

    }
    public void MoveCloud(float _speed)
    {
        transform.position += new Vector3(_speed, 0, 0) * Time.deltaTime;
    }

    public void GrowUp(float f_speed)
    {
        if (!b_cloudDie)
        {
            // increase size of cloud
            if (!b_IsCloudFull && f_WaterVolume < f_CloudFullVolume)
            {
                f_WaterVolume += f_speed;
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), f_speed * Time.deltaTime);
            }
            else
            {
                b_IsCloudFull = true;
            }
            if (b_IsCloudFull && !b_CanRain)
            {

                if (f_WaterVolume < f_RainFullVolume)
                {
                    f_WaterVolume += f_speed;
                    float ratio = 1 - (f_RainFullVolume - f_WaterVolume) / (f_RainFullVolume - f_CloudFullVolume);
                    sr_FillCloud.material.SetFloat("_Fill", ratio);
                }
                else
                {
                    CanRain();
                }
            }
        }
       
    }
    public void Rain()
    {
        if ( b_CanRain)
        {
            if (f_WaterVolume > f_CloudFullVolume)
            {

                f_WaterVolume -= f_rainSpeed;
                float ratio = 1 - (f_RainFullVolume - f_WaterVolume) / (f_RainFullVolume - f_CloudFullVolume);
                if (ratio >= 0)
                    sr_FillCloud.material.SetFloat("_Fill", ratio);

            }
            else {

                b_CanRain = false;
                CloudDie();
                rainVFX.GetComponent<ParticleSystem>().Stop();
            }
        }


    }

    public void CloudDie()
    {
        b_cloudDie = true;
        StartCoroutine(IEDissipate());
    }
    public void CanRain()
    {
        if(f_WaterVolume > f_CloudFullVolume)
        {
            b_CanRain = true;
            rainVFX.SetActive(true);
        }
    }

    void Dissipate()
    {
        if (b_cloudDie && sr_bgSprite.color.a>0)
        {
            Color temp = sr_bgSprite.color;
            temp.a -= Time.deltaTime;
            sr_bgSprite.color = temp;
             sr_FillCloud.material.SetColor("_Tint", temp);
        }
    }
    IEnumerator IEDissipate()
    {
        yield return new WaitForSeconds(2f);
        CloudPool.instance.ReclycleCloud(i_ID);
    }

    public void Reset()
    {
        transform.localScale = Vector3.one * 0.01f;
        sr_bgSprite.color = Color.white;
        sr_FillCloud.material.SetColor("_Tint", Color.white);
        rainVFX.gameObject.SetActive(false);
        f_WaterVolume = 0;

        b_cloudDie = false;
        b_IsCloudFull = false;
        b_isRaining = false;
    }
}
