using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
[CreateAssetMenu]
public class GeneralCounts : ScriptableObject
{
   public static bool Kill = false;
   public int DashCount = 0;
   public int DeathCount = 0;

   public bool CarinhoIsMorto = false;
   public bool TristezaIsMorto = false;

   public bool ExpressividadeIsMorto = false;

   public float CarinhoCompleteTimer = 0f;
   public float TristezaCompleteTimer = 0f;
   public float ExpressividadeCompleteTimer = 0f;
   public float MDMCompleteTimer = 0f;
}
