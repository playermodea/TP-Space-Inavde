using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCtrl : MonoBehaviour
{
    public Slider bgmSlider;

    // Start is called before the first frame update
    void Start()
    {
        bgmSlider = this.GetComponent<Slider>();
        bgmSlider.onValueChanged.AddListener(AudioManager.instance.SetBgmVolume);
    }
}
