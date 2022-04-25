using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReadInput2 : MonoBehaviour
{
    // Start is called before the first frame update
    string input;
    public static int x = 3;
    public static int y = 3;
    public static int z = 1;

    // Start is called before the first frame update
    public void ReadInputStringlev(string s)
    {
        input = s;
        Int32.TryParse(input, out x);
    }
    public void ReadInputStringinit(string s)
    {
        input = s;
        Int32.TryParse(input, out y);
    }

    public void ReadInputStringSound(string s)
    {
        input = s;
        Int32.TryParse(input, out z);
    }

    public int getInputlev()
    {
        return x;
    }
    public int getInputinit()
    {
        return y;
    }

    public int getInputSound()
    {
        return z;
    }
}
