using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MidiReader : MonoBehaviour
{
    public TextAsset MidiFile;

    private readonly byte[] _headerID = new byte[4] { 77, 84, 104, 100 }; //MThd
    private readonly byte[] _trackID = new byte[4] { 77, 84, 114, 107 }; //MTrk

    private byte[] _midiData;
    private byte[] _header;
    private Int16 _format;
    private Int16 _trackCount;
    private Int16 _division;

    private int fileIndex = 0;

    private List<MidiTrack> Tracks = new List<MidiTrack>();

    public void Start()
    {
        _midiData = MidiFile.bytes;
        //Debug.Log(BitConverter.ToString(_midiData)); //._format("{0} {1} {2} {3}", _midiData[0], _midiData[1], _midiData[2], _midiData[3]));
        byte[] byteLength = new byte[4];

        Debug.Log(Encoding.ASCII.GetString(getBlock(_midiData, 0, 4)));
        //Debug.Log(string._format("{0} {1} {2} {3}", _midiData[0], _midiData[1], _midiData[2], _midiData[3]));

        Buffer.BlockCopy(_midiData, fileIndex += 4, byteLength, 0, 4);

        Array.Reverse(byteLength);

        Int32 length = BitConverter.ToInt32(byteLength, 0);

        Debug.Log(BitConverter.ToString(byteLength));
        Debug.Log(length);

        byte[] words = getBlock(_midiData, fileIndex += 4, length);

        _format = BitConverter.ToInt16(new byte[] { words[1], words[0] });
        _trackCount = BitConverter.ToInt16(new byte[] { words[3], words[2] });
        _division = BitConverter.ToInt16(new byte[] { words[5], words[4] });

        writeByte(words);

        byte[] track;
        Debug.Log(Encoding.ASCII.GetString(track = getBlock(_midiData, fileIndex += length, 4)));
        writeByte(track);

        for (int i = 0; i < _trackCount; i++)
        {
            Tracks.Add(ParseTrack());
        }


        //_header = mid

    }

    private byte[] getBlock(byte[] pSrc, int pStart, int pCount, bool pReverse = false)
    {
        byte[] block = new byte[pCount];
        Buffer.BlockCopy(pSrc, pStart, block, 0, pCount);
        if (pReverse) { Array.Reverse(block); }
        return block;
    }

    private void writeByte(byte[] pInput)
    {
        string outputDec = "DEC: ";
        string outputHex = "HEX: ";
        for (int i = 0; i < pInput.Length; i++)
        {
            outputDec += string.Format("{0:D} ", pInput[i]);
            outputHex += string.Format("{0:X} ", pInput[i]);
        }
        Debug.Log(outputDec);
        Debug.Log(outputHex);
    }

    private MidiTrack ParseTrack()
    {
        MidiTrack midiTrack = new MidiTrack();
        byte[] lengthBytes = getBlock(_midiData, fileIndex += 4, 4, true);
        fileIndex += 4;
        writeByte(lengthBytes);
        midiTrack.length = BitConverter.ToInt32(lengthBytes);
        Debug.Log("track length: " + midiTrack.length);

        for (int i = fileIndex; i + midiTrack.length > fileIndex;)
        {
            midiTrack.events.Add(ReadEvent());
        }

        return midiTrack;
    }

    private MTrkEvent ReadEvent()
    {
        MTrkEvent eve = new MTrkEvent();

        int delta = getVLQRecursive();

        eve.DeltaTime = delta;

        Debug.Log("delta time: " + delta);

        eve.eventID = _midiData[fileIndex += 1];

        switch (_midiData[fileIndex])
        {
            case 240: //F0
                Debug.Log("Event ID F0 (sysex)");
                break;
            case 247: //F7
                Debug.Log("Event ID F7 (sysex, cont.)");
                break;
            case 255: //FF
                Debug.Log("Event ID FF (meta event)");
                HandleMetaEvent(eve);
                break;
            default:
                Debug.Log("Non-specified Event ID, handling as midi event");
                eve.eventID = 0;
                HandleMidiEvent(eve);
                break;
        }

        return eve;
    }

    private void HandleMetaEvent(MTrkEvent pEvent)
    {
        pEvent.eventType = _midiData[fileIndex += 1];
        int dataLength = _midiData[fileIndex += 1];

        byte[] data = getBlock(_midiData, fileIndex += 1, dataLength);
        writeByte(data);
        pEvent.eventData = data;

        Debug.Log("We're dealing with a type [" + pEvent.eventType + "] situation Here");

        switch (pEvent.eventType)
        {
            case 0: //00 Sequence Number
                break;
            case 1: //01 Text Event
                break;
            case 2: //02 Copyright Notice
                break;
            case 3: //03 Sequence/Track Name
                break;
            case 4: //04 Instrument Name
                break;
            case 5: //05 Lyric
                break;
            case 6: //06 Marker
                break;
            case 7: //07 Cue Point
                break;
            case 32: //20 Midi Channel Prefix
                break;
            case 47: //2F End of Track
                Debug.Log(" - Track Ended - ");
                break;
            case 81: //51 Set Tempo
                break;
            case 84: //54 SMPTE Offset
                break;
            case 88: //58 Time Signature
                break;
            case 89: //59 Key Signature
                break;
            case 127: //7F Sqequencer-Specific
                break;
            default:
                Debug.LogError("Unknown meta-event, contact your local government official about this issue");
                break;
        }

        fileIndex += (dataLength);
    }

    private void HandleMidiEvent(MTrkEvent pEvent)
    {
        pEvent.eventType = _midiData[fileIndex];
        byte channel;
        byte command;

        command = (byte)(pEvent.eventType / 16);

        channel = (byte)(pEvent.eventType % 16);

        switch (command)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10: //A
                break;
            case 11: //B
                break;
            case 12: //C
                break;
            case 13: //D
                break;
            case 14: //E
                break;
            case 15: //F
                break;
            default:
                Debug.LogError("I don't know how you managed this, but your command ID is out of range");
                break;
        }


    }

    private int getVLQRecursive(short depth = 0)
    {
        if (_midiData[fileIndex + depth] <= 127 || depth == 4)
        {
            fileIndex += depth;
            Debug.Log("VLQ depth: " + depth);
            return _midiData[fileIndex];
        }
        else
        {
            return _midiData[fileIndex + depth] + getVLQRecursive(++depth) - 1;
        }
    }

    /*
     * MThd
     * length
     * _format
     * Track Count
     * _division / Delta Times
     */
}
