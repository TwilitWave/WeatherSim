using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcButton : MonoBehaviour {
    public GameObject AchievePanel;
    private bool is_active = false;
    private Button my_Button;
	// Use this for initialization
	void Start () {
        my_Button = this.GetComponent<Button>();
        my_Button.onClick.AddListener(Click);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void Click() {
        is_active = !is_active;
        AchievePanel.SetActive(is_active);
    }
}
