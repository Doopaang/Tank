﻿using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
}
