﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.Quit();
        }
    }
}
