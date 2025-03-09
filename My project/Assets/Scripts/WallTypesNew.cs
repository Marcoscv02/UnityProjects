using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public static class WallTypesNew
{

    public static HashSet<int> wallTop = new HashSet<int>
    {
        0b0010
    };

    public static HashSet<int> wallSideLeft = new HashSet<int>
    {
        0b0100
    };

    public static HashSet<int> wallSideRight = new HashSet<int>
    {
        0b0001
    };

    public static HashSet<int> wallBottom = new HashSet<int>
    {
        0b1000
    };
    
}
