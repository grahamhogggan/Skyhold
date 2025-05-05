using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Encryption
{
    public static string Encrypt(string input)
    {
        string result = "";
        for(int i = 0; i<input.Length; i++)
        {
            result+= (char)((int) input[i]+1);
        }
        return result;
    }
    public static string Decrypt(string input)
    {
        string result = "";
        for(int i = 0; i<input.Length; i++)
        {
            result += (char)((int) input[i]-1);
        }
        return result;
    }
}
