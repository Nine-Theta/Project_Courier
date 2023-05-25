using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiTrack
{
    public Int32 length = 0;

    public List<MTrkEvent> events = new List<MTrkEvent>();
}

public class MTrkEvent
{
    public Int32 DeltaTime;
    public Int32 Length;
    public byte eventID = 0; // 0 = midi, F0/240 = sysex, F7/247 = sysex cont., FF/255 = meta
    public byte eventType;
    public byte[] eventData;
}
