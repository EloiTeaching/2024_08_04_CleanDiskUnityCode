using System;
using UnityEngine;

public class GenerateRandomIdStringUtility {
    public const string m_id = "!#$%&'()*+,-./0123456789<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{}~";

    public static string GUID()
    {
        return Guid.NewGuid().ToString();
    }

    public const string m_idLetter = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";


    [ContextMenu("Generate Random ID")]
    public static string GenerateId(int numberOfValue=6)
    {
        string shortId = "";
        for (int i = 0; i < numberOfValue; i++)
        {
            shortId += m_id[UnityEngine.Random.Range(0, m_id.Length)];
        }
        return shortId;
    }
    [ContextMenu("Generate Random Letter ID")]
    public static string GenerateIdLetter(int numberOfValue = 6)
    {
        string  shortId = "";
        for (int i = 0; i < numberOfValue; i++)
        {
            shortId += m_idLetter[UnityEngine.Random.Range(0, m_idLetter.Length)];
        }
        return shortId;
    }
}

