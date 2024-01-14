using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [field: SerializeField] public HexOrientation Orientation { get; private set; }
    [field: SerializeField] public int Size { get; private set; }
    //[field:SerializeField] public int Width { get; private set; }
    //[field: SerializeField] public int Height { get; private set; }
    [field: SerializeField] public int HexSize { get; private set; }
    [field: SerializeField] public GameObject HexPrefab { get; private set; }

    private void OnDrawGizmos()
    {
        for(int z = 0; z <= Size * 2; z++)
        {
            for (int x = 0; x <= Size * 2; x++)
            {
                if (x + z >= Size && x + z <= Size*3)
                {
                    Vector3 centrePosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;
                    for (int s = 0; s < HexMetrics.Corners(HexSize, Orientation).Length; s++)
                    {
                        Gizmos.DrawLine(
                            centrePosition + HexMetrics.Corners(HexSize, Orientation)[s % 6],
                            centrePosition + HexMetrics.Corners(HexSize, Orientation)[(s + 1) % 6]
                            );
                    }
                }
                
            }
        }
    }
}

public enum HexOrientation
{
    FlatTop,
    PointyTop
}
