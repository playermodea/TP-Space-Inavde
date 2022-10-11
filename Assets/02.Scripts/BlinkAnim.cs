using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    public string color;
    float time;
    void Update()
    {
        if(color == "Red")
        {
            if(time<0.5f)
            {
                GetComponent<Text>().color = new Color(255/255f, 1/255f, 1/255f, 255/255f - time);
            }
            else
            {
                GetComponent<Text>().color = new Color(255/255f, 1/255f, 1/255f, time);
                if(time>1f)
                {
                    time = 0;
                }
            }
        }
        if(color == "Yellow")
        {
            if (time < 0.5f)
            {
                GetComponent<Text>().color = new Color(255 / 255f, 255 / 255f, 1 / 255f, 255 / 255f - time);
            }
            else
            {
                GetComponent<Text>().color = new Color(255 / 255f, 255 / 255f, 1 / 255f, time);
                if (time > 1f)
                {
                    time = 0;
                }
            }
        }
        time += Time.deltaTime;
    }
}
