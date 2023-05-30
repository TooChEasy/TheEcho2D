using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeText : MonoBehaviour
{
    private TextMeshProUGUI tmp; 
    public Slider slider;
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = "100";
        slider.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = slider.value.ToString("F0");
    }
}
