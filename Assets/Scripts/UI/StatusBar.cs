﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(RectTransform))]
public class StatusBar : MonoBehaviour
{
    public RectTransform[] Parts;
    float[] values = new float[0];

    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        float f = 0;
        for (int i = 0; i < Parts.Length; i++)
        {
            float v = i < values.Length ? values[i] : 0;
            
            Parts[i].anchorMin = new Vector2(f, 0);
            Parts[i].anchorMax = new Vector2(f+v, 1);

            f+= v;
        }
    }

    public void UpdateSlider(float[] values){
        this.values = values;
    }

    /*public void UpdateSlider(float v1){
        UpdateSlider(new float[] {v1, 1-v1});
    }*/

}