using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class HexGrid : MonoBehaviour
{
    [field: SerializeField] public HexOrientation Orientation { get; private set; }
    [field: SerializeField] public int Size { get; private set; }
    //[field:SerializeField] public int Width { get; private set; }
    //[field: SerializeField] public int Height { get; private set; }
    [field: SerializeField] public int HexSize { get; private set; }
    [field: SerializeField] public GameObject HexPrefab { get; private set; }
    [field:SerializeField] public int BatchSize { get; private set; }
    [field: SerializeField] public int BoardPiece { get; private set; }
    [SerializeField] private List<HexCell> cells = new List<HexCell>();
    [SerializeField] public List<TerrainType> TerrainTypes = new List<TerrainType>();
    private List<List<List<int>>> boardPieces = new List<List<List<int>>>();
    private Task<List<HexCell>> hexGenerationTask;
    private Vector3 gridOrigin;
    public event System.Action OnMapInfoGenerated;
    public event System.Action<float> OnCellBatchGenerated;
    public event System.Action OnCellInstancesGenerated;

    private void Awake()
    {
        gridOrigin = transform.position;
        // A boardPiece = 0
        boardPieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  1,  1,  1,  1 },
                            new List<int> { -1, -1,  1,  1,  1,  1,  1 },
                            new List<int> { -1,  1,  1,  4,  1,  8,  1 },
                            new List<int> {  1,  4,  1,  8,  1,  4,  1 },
                            new List<int> {  1,  0,  1,  1,  1,  1, -1 },
                            new List<int> {  8,  0,  1,  1,  4, -1, -1 },
                            new List<int> {  1,  12,  1,  1, -1, -1, -1 }
                        });
        // B boardPiece = 1
        boardPieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  1,  1,  1,  1 },
                            new List<int> { -1, -1,  1,  1,  1,  1,  1 },
                            new List<int> { -1,  1,  1,  1,  4,  1,  1 },
                            new List<int> {  1,  1,  8,  1,  1,  0,  8 },
                            new List<int> {  1,  1,  4,  1,  4, 12, -1 },
                            new List<int> {  1,  1,  4,  1,  8, -1, -1 },
                            new List<int> {  8,  1,  1,  1, -1, -1, -1 }
                        });
        // C boardPiece = 2
        boardPieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  1,  1,  8,  8 },
                            new List<int> { -1, -1,  4,  0,  1,  4,  8 },
                            new List<int> { -1,  4,  0,  8,  8,  4,  4 },
                            new List<int> {  8,  4,  0,  0,  8,  0,  0 },
                            new List<int> {  8,  8,  4,  4,  0,  8, -1 },
                            new List<int> {  1,  4,  0,  8,  8, -1, -1 },
                            new List<int> {  1,  1,  0,  0, -1, -1, -1 }
                        });
        // G boardPiece = 3
        boardPieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  1,  1,  4,  0 },
                            new List<int> { -1, -1,  1,  2,  5,  0,  4 },
                            new List<int> { -1,  1,  4,  5,  7,  5,  1 },
                            new List<int> {  1,  0,  0,  6,  5,  2,  1 },
                            new List<int> {  1,  4,  5,  0,  4,  1, -1 },
                            new List<int> {  1,  2,  4,  0,  1, -1, -1 },
                            new List<int> { 12,  1,  1,  1, -1, -1, -1 }
                        });
        // K boardPiece = 4
        boardPieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1, 12,  1,  1,  2 },
                            new List<int> { -1, -1,  2,  1,  2,  1,  2 },
                            new List<int> { -1,  2,  7,  1,  3,  1,  2 },
                            new List<int> {  1,  1,  3,  1,  3,  1,  1 },
                            new List<int> {  2,  1,  3,  1, 10,  2, -1 },
                            new List<int> {  2,  1,  2,  1,  2, -1, -1 },
                            new List<int> {  2,  1,  1, 12, -1, -1, -1 }
                        });
        // J boardPiece = 5
        boardPieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  0,  0,  0,  0 },
                            new List<int> { -1, -1,  4,  0,  0,  0,  0 },
                            new List<int> { -1,  4,  5,  1,  1,  0,  0 },
                            new List<int> {  4,  5,  1, 12,  2,  8,  8 },
                            new List<int> {  4,  5,  2,  1,  9,  8, -1 },
                            new List<int> {  4,  4,  9,  0,  8, -1, -1 },
                            new List<int> {  4,  8,  8,  8, -1, -1, -1 }
                        });
        // N boardPiece = 6
        boardPieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  1,  1,  4,  4 },
                            new List<int> { -1, -1,  1,  1,  5,  5,  8 },
                            new List<int> { -1,  1,  2,  1,  6,  8,  8 },
                            new List<int> {  1,  1,  8,  7,  8,  1,  1 },
                            new List<int> {  1,  8,  6,  1,  2,  1, -1 },
                            new List<int> {  8,  8,  5,  1,  1, -1, -1 },
                            new List<int> {  8,  4,  4,  1, -1, -1, -1 }
                        });
    }

    private void Start()
    {
        hexGenerationTask = Task.Run(()=>GenerateHexCellData());
    }

    private void Update()
    {
        if(hexGenerationTask != null && hexGenerationTask.IsCompleted)
        {
            cells = hexGenerationTask.Result;
            OnMapInfoGenerated?.Invoke();
            StartCoroutine(InstantiateCells());
            hexGenerationTask = null; //Clear the task
        }
    }

    private List<HexCell> GenerateHexCellData()
    {
        Debug.Log("Generating Hex Cell Data");
        List<HexCell> hexCells = new List<HexCell>();
        for (int z = 0; z <= Size * 2; z++)
        {
            for (int x = 0; x <= Size * 2; x++)
            {
                if (x + z >= Size && x + z <= Size * 3)
                {
                    Vector3 centrePosition = HexMetrics.Center(HexSize, x, z, Orientation) + gridOrigin;
                    HexCell cell = new HexCell();
                    cell.SetCoordinates(new Vector2(x, z), Orientation);
                    cell.Grid = this;
                    cell.HexSize = HexSize;
                    TerrainType terrain = TerrainTypes[boardPieces[BoardPiece][z][x]];
                    cell.SetTerrainType(terrain);
                    hexCells.Add(cell);
                }

            }
        }
        return hexCells;
    }

    private IEnumerator InstantiateCells()
    {
        int batchCount = 0;
        int totalBatches = Mathf.CeilToInt(cells.Count / BatchSize);
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].CreateTerrain();
            if(i % BatchSize == 0)
            {
                batchCount++;
                OnCellBatchGenerated?.Invoke((float)batchCount / totalBatches);
                yield return null;
            }
        }
        OnCellInstancesGenerated?.Invoke();
    }

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
