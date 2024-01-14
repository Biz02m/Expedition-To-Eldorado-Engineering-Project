using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class HexGridMeshGenerator : MonoBehaviour
{
    [field:SerializeField] public LayerMask gridLayer { get; private set; }
    [field: SerializeField] public HexGrid hexGrid { get; private set; }

    private void Awake()
    {
        if (hexGrid == null)
        {
            hexGrid = GetComponentInParent<HexGrid>();
        }
        if (hexGrid == null)
        {
            Debug.LogError("HexGridMeshGenerator could not find a HexGrid component in its parent or itself.");
        }
    }

    public void CreateHexMesh()
    {
        CreateHexMesh(hexGrid.Size, hexGrid.HexSize, hexGrid.Orientation, gridLayer);
    }

    public void CreateHexMesh(HexGrid hexGrid, LayerMask layerMask)
    {
        this.hexGrid = hexGrid;
        this.gridLayer = layerMask;
        CreateHexMesh(hexGrid.Size, hexGrid.HexSize, hexGrid.Orientation, layerMask);
    }

    public void CreateHexMesh(int size, float hexSize, HexOrientation orientation, LayerMask layerMask)
    {
        ClearHexGridMesh();
        int numOfHex = (3 * size * size) + (3 * size) + 1;
        Vector3[] vertices = new Vector3[7 * numOfHex];
        int currentHex = 0;
        for (int z = 0; z <= size * 2; z++)
        {
            for (int x = 0; x <= size * 2; x++)
            {
                if (x + z >= size && x + z <= size * 3)
                {
                    Vector3 centrePosition = HexMetrics.Center(hexSize, x, z, orientation);
                    vertices[currentHex * 7] = centrePosition;
                    for (int s = 0; s < HexMetrics.Corners(hexSize, orientation).Length; s++)
                    {
                        vertices[currentHex * 7 + s + 1] = centrePosition + HexMetrics.Corners(hexSize, orientation)[s % 6];
                    }
                    currentHex++;
                }

            }
        }

        int[] triangles = new int[3 * 6 * numOfHex];
        int currentHexForTriangles = 0;
        for (int z = 0; z <= size * 2; z++)
        {
            for (int x = 0; x <= size * 2; x++)
            {
                if (x + z >= size && x + z <= size * 3)
                {
                    for (int s = 0; s < HexMetrics.Corners(hexSize, orientation).Length; s++)
                    {
                        int cornerIndex = s + 2 > 6 ? s + 2 - 6 : s + 2;
                        triangles[3 * 6 * currentHexForTriangles + s * 3 + 2] = currentHexForTriangles * 7;
                        triangles[3 * 6 * currentHexForTriangles + s * 3 + 1] = currentHexForTriangles * 7 + s + 1;
                        triangles[3 * 6 * currentHexForTriangles + s * 3 + 0] = currentHexForTriangles * 7 + cornerIndex;
                    }
                    currentHexForTriangles++;
                }

            }
        }

        Mesh mesh = new Mesh();
        mesh.name = "Hex Mesh";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        mesh.RecalculateUVDistributionMetrics();

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        int gridLayerIndex = GetLayerIndex(layerMask);
        Debug.Log("Layer Index: " + gridLayerIndex);

        gameObject.layer = gridLayerIndex;
    }

    public void ClearHexGridMesh()
    {
        if (GetComponent<MeshFilter>().sharedMesh == null)
            return;
        GetComponent<MeshFilter>().sharedMesh.Clear();
        GetComponent<MeshCollider>().sharedMesh.Clear();
    }

    private int GetLayerIndex(LayerMask layerMask)
    {
        int layerMaskValue = layerMask.value;
        Debug.Log("Layer Mask Value: " + layerMaskValue);
        for (int i = 0; i < 32; i++)
        {
            if(((1 << i) & layerMaskValue) != 0)
            {
                Debug.Log("Layer Index Loop: " + i);
                return i;
            }
        }
        return 0;
    }
}