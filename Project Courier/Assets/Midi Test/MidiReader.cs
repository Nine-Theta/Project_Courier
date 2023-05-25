using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MidiReader : MonoBehaviour
{
    public TextAsset MidiFile;

    private readonly byte[] headerID = new byte[4] { 77, 84, 104, 100 }; //MThd
    private readonly byte[] trackID = new byte[4] { 77, 84, 114, 107 }; //MTrk

    private readonly byte sysexID = 255;

    private byte[] midiData;
    private byte[] header;
    private byte[] format;
    private byte[] trackCount;
    private byte[] division;

    private int fileIndex = 0;

    private List<MidiTrack> Tracks = new List<MidiTrack>();

    public void Start()
    {
        midiData = MidiFile.bytes;
        //Debug.Log(BitConverter.ToString(midiData)); //.Format("{0} {1} {2} {3}", midiData[0], midiData[1], midiData[2], midiData[3]));
        byte[] byteLength = new byte[4];
        
        Debug.Log(Encoding.ASCII.GetString(getBlock(midiData, 0, 4)));
        //Debug.Log(string.Format("{0} {1} {2} {3}", midiData[0], midiData[1], midiData[2], midiData[3]));

        Buffer.BlockCopy(midiData, fileIndex += 4, byteLength, 0, 4);

        Array.Reverse(byteLength);

        Int32 length = BitConverter.ToInt32(byteLength,0);

        Debug.Log(BitConverter.ToString(byteLength));
        Debug.Log(length);

        byte[] words = getBlock(midiData, fileIndex += 4, length);

        format = new byte[] { words[1], words[0] };
        trackCount = new byte[] { words[3], words[2] };
        division = new byte[] { words[5], words[4] };

        writeByte(words);

        byte[] track;
        Debug.Log(Encoding.ASCII.GetString(track = getBlock(midiData, fileIndex += length, 4)));
        writeByte(track);

        Tracks.Add(parseTrack());


        //header = mid

    }

    private byte[] getBlock(byte[] src, int start, int count, bool reverse = false)
    {
        byte[] block = new byte[count];
        Buffer.BlockCopy(src, start, block, 0, count);
        if(reverse) { Array.Reverse(block); }
        return block;
    }

    private void writeByte(byte[] input)
    {
        string outputDec = "DEC: ";
        string outputHex = "HEX: ";
        for (int i = 0; i < input.Length; i++)
        {
            outputDec += string.Format("{0:D} ", input[i]);
            outputHex += string.Format("{0:X} ", input[i]);
        }
        Debug.Log(outputDec);
        Debug.Log(outputHex);
    }

    private MidiTrack parseTrack()
    {
        MidiTrack midiTrack = new MidiTrack();
        byte[] debugLength = getBlock(midiData, fileIndex += 4, 4, true);
        writeByte(debugLength);
        midiTrack.length = BitConverter.ToInt32(debugLength);
        Debug.Log(midiTrack.length);

        MTrkEvent eve = new MTrkEvent();

        //eve.DeltaTime

        midiTrack.events.Add(eve);

        return midiTrack;
    }

    private void parseEvent()
    {
        int delta = getDeltaRecursive();

        

        switch (midiData[fileIndex])
        {
            case 240: //F0
                break;
            case 247: //F7
                break;
            case 255: //FF
                break;
            default:
                break;
        }
    }

    private int getDeltaRecursive(short depth = 0)
    {   
        if (midiData[fileIndex+depth] <= 127 || depth == 4)
        {
            fileIndex += depth;
            return midiData[fileIndex+depth];
        }
        else
        {
            return midiData[fileIndex + depth] + getDeltaRecursive(depth);
        }
    }

    /*
     * MThd
     * length
     * Format
     * Track Count
     * Division / Delta Times
     */
}
