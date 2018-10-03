using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour {
    public static PopUpManager Instance = null;
    public GameObject Panel;
    public Text Title;
    public Text Description;
    public Image Image;
    public string[] Titles = new string[4];
    public string[] Contents = new string[4];
    public Sprite[] Picture = new Sprite[4];
    public GameObject[] Achievements = new GameObject[4];
    public float existtime;
    private bool is_alive;
    private float currenttime;
    private int unlocked = 0;
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        this.Panel.SetActive(false);
        is_alive = false;
    }
    public void SetContent(int index) {
        Text temt = Achievements[unlocked].GetComponentInChildren<Text>();
        Image temi = Achievements[unlocked].GetComponentInChildren<Image>();
        this.Panel.SetActive(true);
        this.Title.text = Titles[index];
        temt.text = Titles[index];
        this.Description.text = Contents[index];
        this.Image.sprite = Picture[index];
        temi.sprite = Picture[index];
        is_alive = true;
        currenttime = 0;
        unlocked++;
    }
    void Start()
    {
        SetContent(1);
    }
    // Use this for initialization	
    // Update is called once per frame
    void Update () {
        if (is_alive) {
            currenttime += Time.deltaTime;
            if (currenttime > existtime) {
                is_alive = false;
                Panel.SetActive(false);
            }
        }
	}
}
