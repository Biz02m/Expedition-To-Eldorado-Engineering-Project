using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BoardSingleton : MonoBehaviour
{
    public static BoardSingleton instance { get; private set; }
    private static TerrainType[] AllTerrains;
    public List<TerrainType> TerrainTypes = new List<TerrainType>();
    public List<List<List<int>>> Pieces = new List<List<List<int>>>();

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        SetTerrainTypes();
        SetBoardPieces();
    }
    private void SetTerrainTypes()
    {
        //Creating Array with all ScriptableObjects with type TerrainTypes
        AllTerrains = (TerrainType[])Resources.FindObjectsOfTypeAll(typeof(TerrainType));
        //Adding all TerrainTypes to TerrainTypes List according to ID value
        for (int i = 0; i < AllTerrains.Length; i++)
        {
            TerrainType terrain = AllTerrains.SingleOrDefault(x => x.ID == i);
            TerrainTypes.Add(terrain);
        }
        
    }

    private void SetBoardPieces()
    {
        // A boardPiece = 0
        Pieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1, 13, 14, 15, 16 },
                            new List<int> { -1, -1,  1,  1,  1,  1,  1 },
                            new List<int> { -1,  1,  1,  4,  1,  8,  1 },
                            new List<int> {  1,  4,  1,  8,  1,  4,  1 },
                            new List<int> {  1,  0,  1,  1,  1,  1, -1 },
                            new List<int> {  8,  0,  1,  1,  4, -1, -1 },
                            new List<int> {  1,  12,  1,  1, -1, -1, -1 }
                        });
        // B boardPiece = 1
        Pieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1, 13, 14, 15, 16 },
                            new List<int> { -1, -1,  1,  1,  1,  1,  1 },
                            new List<int> { -1,  1,  1,  1,  4,  1,  1 },
                            new List<int> {  1,  1,  8,  1,  1,  0,  8 },
                            new List<int> {  1,  1,  4,  1,  4, 12, -1 },
                            new List<int> {  1,  1,  4,  1,  8, -1, -1 },
                            new List<int> {  8,  1,  1,  1, -1, -1, -1 }
                        });
        // C boardPiece = 2
        Pieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  1,  1,  8,  8 },
                            new List<int> { -1, -1,  4,  0,  1,  4,  8 },
                            new List<int> { -1,  4,  0,  8,  8,  4,  4 },
                            new List<int> {  8,  4,  0,  0,  8,  0,  0 },
                            new List<int> {  8,  8,  4,  4,  0,  8, -1 },
                            new List<int> {  1,  4,  0,  8,  8, -1, -1 },
                            new List<int> {  1,  1,  0,  0, -1, -1, -1 }
                        });
        // G boardPiece = 3
        Pieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  1,  1,  4,  0 },
                            new List<int> { -1, -1,  1,  2,  5,  0,  4 },
                            new List<int> { -1,  1,  4,  5,  7,  5,  1 },
                            new List<int> {  1,  0,  0,  6,  5,  2,  1 },
                            new List<int> {  1,  4,  5,  0,  4,  1, -1 },
                            new List<int> {  1,  2,  4,  0,  1, -1, -1 },
                            new List<int> { 12,  1,  1,  1, -1, -1, -1 }
                        });
        // K boardPiece = 4
        Pieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1, 12,  1,  1,  2 },
                            new List<int> { -1, -1,  2,  1,  2,  1,  2 },
                            new List<int> { -1,  2,  7,  1,  3,  1,  2 },
                            new List<int> {  1,  1,  3,  1,  3,  1,  1 },
                            new List<int> {  2,  1,  3,  1, 10,  2, -1 },
                            new List<int> {  2,  1,  2,  1,  2, -1, -1 },
                            new List<int> {  2,  1,  1, 12, -1, -1, -1 }
                        });
        // J boardPiece = 5
        Pieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  0,  0,  0,  0 },
                            new List<int> { -1, -1,  4,  0,  0,  0,  0 },
                            new List<int> { -1,  4,  5,  1,  1,  0,  0 },
                            new List<int> {  4,  5,  1, 12,  2,  8,  8 },
                            new List<int> {  4,  5,  2,  1,  9,  8, -1 },
                            new List<int> {  4,  4,  9,  0,  8, -1, -1 },
                            new List<int> {  4,  8,  8,  8, -1, -1, -1 }
                        });
        // N boardPiece = 6
        Pieces.Add(new List<List<int>> {
                            new List<int> { -1, -1, -1,  1,  1,  4,  4 },
                            new List<int> { -1, -1,  1,  1,  5,  5,  8 },
                            new List<int> { -1,  1,  2,  1,  6,  8,  8 },
                            new List<int> {  1,  1,  8,  7,  8,  1,  1 },
                            new List<int> {  1,  8,  6,  1,  2,  1, -1 },
                            new List<int> {  8,  8,  5,  1,  1, -1, -1 },
                            new List<int> {  8,  4,  4,  1, -1, -1, -1 }
                        });
    }
}
