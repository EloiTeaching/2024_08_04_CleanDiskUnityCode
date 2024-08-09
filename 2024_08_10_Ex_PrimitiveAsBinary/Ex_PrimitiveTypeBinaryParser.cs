using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PrimitiveTypeBinaryParser : MonoBehaviour
{

    public string m_notVerified = "This script is not verified, it is just a test script to show how to convert primitive types to binary";

    [SerializeField]
    private List<string> m_binaryRepresentations = new List<string>();
    private void OnValidate()
    {
        Refresh();
    }
    // Example values for each primitive type
    public bool boolValue = true;
    public byte byteValue = 255;
    public sbyte sbyteValue = -128;
    public short shortValue = -32768;
    public ushort ushortValue = 65535;
    public int intValue = -2147483648;
    public uint uintValue = 4294967295;
    public long longValue = -9223372036854775808;
    public ulong ulongValue = 18446744073709551615;
    public float floatValue = 3.14f;
    public double doubleValue = 3.141592653589793;

    void Reset()
    {
        Refresh();
    }

    private void Refresh()
    {
        // Add each binary representation to the list
        m_binaryRepresentations.Clear(); // Clear existing entries
        m_binaryRepresentations.Add($"boolValue: {ToBinaryString(BitConverter.GetBytes(boolValue))}");
        m_binaryRepresentations.Add($"byteValue: {ToBinaryString(BitConverter.GetBytes(byteValue))}");
        m_binaryRepresentations.Add($"sbyteValue: {ToBinaryString(BitConverter.GetBytes(sbyteValue))}");
        m_binaryRepresentations.Add($"shortValue: {ToBinaryString(BitConverter.GetBytes(shortValue))}");
        m_binaryRepresentations.Add($"ushortValue: {ToBinaryString(BitConverter.GetBytes(ushortValue))}");
        m_binaryRepresentations.Add($"intValue: {ToBinaryString(BitConverter.GetBytes(intValue))}");
        m_binaryRepresentations.Add($"uintValue: {ToBinaryString(BitConverter.GetBytes(uintValue))}");
        m_binaryRepresentations.Add($"longValue: {ToBinaryString(BitConverter.GetBytes(longValue))}");
        m_binaryRepresentations.Add($"ulongValue: {ToBinaryString(BitConverter.GetBytes(ulongValue))}");
        m_binaryRepresentations.Add($"floatValue: {ToBinaryString(BitConverter.GetBytes(floatValue))}");
        m_binaryRepresentations.Add($"doubleValue: {ToBinaryString(BitConverter.GetBytes(doubleValue))}");
    }

    [ContextMenu("Set to max")]
    public void SetAllPrimitiveToMax() {
        boolValue = true;
        byteValue = byte.MaxValue;
        sbyteValue = sbyte.MaxValue;
        shortValue = short.MaxValue;
        ushortValue = ushort.MaxValue;
        intValue = int.MaxValue;
        uintValue = uint.MaxValue;
        longValue = long.MaxValue;
        ulongValue = ulong.MaxValue;
        floatValue = float.MaxValue;
        doubleValue = double.MaxValue;
            

        Refresh();

    }
    [ContextMenu("Set to min")]
    public void SetAllPrimitiveToMin()
    {
        boolValue = false;
        byteValue = byte.MinValue;
        sbyteValue = sbyte.MinValue;
        shortValue = short.MinValue;
        ushortValue = ushort.MinValue;
        intValue = int.MinValue;
        uintValue = uint.MinValue;
        longValue = long.MinValue;
        ulongValue = ulong.MinValue;
        floatValue = float.MinValue;
        doubleValue = double.MinValue;


        Refresh();

    }

    string ToBinaryString(byte[] bytes)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            sb.Append(Convert.ToString(b, 2).PadLeft(8, '0')).Append(" ");
        }
        return sb.ToString().TrimEnd();
    }
}
