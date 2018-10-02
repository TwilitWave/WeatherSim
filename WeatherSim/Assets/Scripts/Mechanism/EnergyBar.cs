using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    Image _image;
    float _energy;
    // Use this for initialization
    void Start()
    {
        _image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

            _energy = VaporManager.instance.f_energy;
        float ratio = _energy / 100;
        _image.fillAmount = ratio;


    }
}
