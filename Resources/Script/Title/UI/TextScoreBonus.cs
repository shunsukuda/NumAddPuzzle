using UnityEngine;
using UnityEngine.UI;
using UnityExtension;
using System.Collections;

public class TextScoreBonus : MonoBehaviourExtension
{
    [SerializeField]
    Text text;
    [SerializeField]
    Slider slider;

    void Start()
    {
        text = this.SafeGetComponent<Text>();
        slider = GameObject.FindGameObjectWithTag("LifeSlider").SafeGetComponent<Slider>();
    }

    void Update()
    {
        text.text = "スコアボーナス：" + Bonus().ToString();
    }

    float Bonus()
    {
        float value = slider.value;
        float bonusRate = 0.0f;
        if (value >= 10) bonusRate = 1.5f - 0.1f*(value - 10.0f);
        else if(value >= 2) bonusRate = 4.0f - 0.25f*(value - 2.0f);
        else if(value == 1) bonusRate = 5.0f;
        else bonusRate = 0.0f;
        DataObject.SetLife((int)value);
        DataObject.SetScoreRate(bonusRate);
        return bonusRate;
    }
}
