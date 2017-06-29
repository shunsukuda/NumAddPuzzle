using UnityEngine;
using UnityEngine.UI;
using UnityExtension;
using System.Collections;

public class TextLifeValue : MonoBehaviourExtension
{
    [SerializeField]
    Text text;
    [SerializeField]
    Slider slider;
    
	void Start()
    {
        text = this.SafeGetComponent<Text>();
        slider = GameObject.FindGameObjectWithTag("LifeSlider").SafeGetComponent<Slider>();
        slider.value = 20;
	}
	
	void Update ()
    {
        text.text = "ライフ：" + slider.value.ToString();
	}
}