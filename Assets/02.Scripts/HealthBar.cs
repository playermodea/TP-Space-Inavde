using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float dampening = 5f;
    public GameObject player;

    Material mat;
    float fillTarget = .5f;
    float delta = 0f;

    void Awake()
    {
        Renderer rend = GetComponent<Renderer>();
        Image img = GetComponent<Image>();
        if (rend != null)
        {
            mat = new Material(rend.material);
            rend.material = mat;
        }
        else if (img != null)
        {
            mat = new Material(img.material);
            img.material = mat;
        }
        else
        {
            Debug.LogWarning("No Renderer or Image attached to " + name);
        }
    }

    void Update()
    {
        float newFill = player.GetComponent<PlayerCtrl>().health * 0.01f;

        delta -= fillTarget - newFill;
        fillTarget = newFill;

        delta = Mathf.Lerp(delta, 0, Time.deltaTime * dampening);

        mat.SetFloat("_Delta", delta);
        mat.SetFloat("_Fill", fillTarget);
    }
}
