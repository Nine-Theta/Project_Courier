using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

[Serializable, Obsolete]
public struct QuestObject 
{
    public string name { get; private set; }
    public byte stage;
    
    public QuestObject(string pName, byte pStage = 0)
    {
        name = pName;
        stage = pStage;
    }
}
