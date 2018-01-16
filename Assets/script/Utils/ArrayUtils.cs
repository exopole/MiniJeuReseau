using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayUtils {

	static public string printArray(ArrayList array)
    {
        string result = "";
        foreach (var value in array)
        {
            result += value + " ";
        }
        return result;
    }
    static public string printArray2D(int[,] array, int size)
    {
        string result = "";
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result += array[i, j] + " ";
            }
            result += "\n";
        }
        return result;
    }

    static public string printArray2D(int[,] array, int size, int size2)
    {
        string result = "";
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size2; j++)
            {
                result += array[i, j] + " ";
            }
            result += "\n";
        }
        return result;
    }

    static public void RemplissageMatrice2D(ArrayList array, int values, int count )
    {
       
    }
}
