using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_AndOrXor : MonoBehaviour
{
    //https://github.com/EloiStree/HelloSharpForUnity3D/issues/345
    [Header("Two values")]
    public bool m_left;
    public bool m_right;


    [Header("Two operators")]
    public bool m_and;
    public bool m_or;
    public bool m_xor;
    public bool m_nand;
    public bool m_nor;
    public bool m_nxor;


    [Header("Inverse one operator")]
    public bool m_notLeft;

    void OnValidate()
    {
        ComputeOperation();
    }

    public bool Inverse(bool value)
    {
        //0 1
        //1 0
        return !value;
    }
    public bool AreBothTrue(bool left, bool right)
    {

        //00 0
        //01 0
        //10 0
        //11 1
        return left && right;
    }
    public bool AreNotBothTrue(bool left, bool right)
    {

        //00 0 1
        //01 0 1 
        //10 0 1
        //11 1 0
        return !(left && right);
    }
    public bool IsOneTrue(bool left, bool right)
    {

        //00 0
        //01 1
        //10 1
        //11 1
        return left || right;
    }
    public bool AreNotOneTrue(bool left, bool right)
    {
        //00 0 1
        //01 1 0
        //10 1 0
        //11 1 0
        return !(left || right);
    }

    public bool IsOneOnlyTrue(bool left, bool right)
    {
        //00 0
        //01 1
        //10 1
        //11 0
        return left ^ right;
    }
    public bool IsBothTheSameValue(bool left, bool right)
    {
        //00 0 1
        //01 1 0
        //10 1 0
        //11 0 1
        return !(left ^ right);
    }


    private void ComputeOperation()
    {
        m_notLeft = !m_left;
        m_and = m_left && m_right;
        m_or = m_left || m_right;
        m_xor = m_left ^ m_right;
        m_nand = !(m_left && m_right);
        m_nor = !(m_left || m_right);
        m_nxor = !(m_left ^ m_right);
    }
}
