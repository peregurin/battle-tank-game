﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tank.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObjects/NewTankScriptableObject")]
    public class TankScriptableObject : ScriptableObject
    {
        public string TankName;
        public TankType TankType;
        public float Speed;
    }
}