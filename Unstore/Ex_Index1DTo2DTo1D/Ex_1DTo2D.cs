using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_1DTo2D : MonoBehaviour
{


    public static void Parse2DTo1D(ref int x, ref int y, ref int width, out int index1D ) { index1D= y * width + x; }
  
    public static void Parse1DTo2D(ref int index, ref int width, out int x, out int y) {
    
        x = index % width;
        y = index / width;
    }

}
