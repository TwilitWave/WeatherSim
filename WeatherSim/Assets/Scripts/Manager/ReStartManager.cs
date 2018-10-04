using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReStartManager : MonoBehaviour
{
    private Button my_Button;
    // Use this for initialization
    void Start()
    {
        my_Button = this.GetComponent<Button>();
        my_Button.onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Click()
    {
        SceneManager.LoadScene("Main 1", LoadSceneMode.Single);
    }

}