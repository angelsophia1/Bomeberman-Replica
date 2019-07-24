using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
public class MapDestroyer : MonoBehaviour {
    public Tilemap tilemap;
    public Tile wallTile, destructibleTile;
    public GameObject explosionPrefab , restartButton;
    public static bool isOver;
    private void Start()
    {
        isOver = false;
    }
    public void Explode(Vector3 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        ExplodeCell(originCell);
        if(ExplodeCell(originCell + new Vector3Int(1, 0, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(2, 0, 0));
        }
        
        if(ExplodeCell(originCell + new Vector3Int(0, 1, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(0, 2, 0));
        }
        
        if(ExplodeCell(originCell + new Vector3Int(-1, 0, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(-2, 0, 0));
        }
        if(ExplodeCell(originCell + new Vector3Int(0, -1, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(0, -2, 0));
        }

    }

    bool ExplodeCell(Vector3Int cell)
    {
       Tile tile = tilemap.GetTile<Tile>(cell);
        if (tile == wallTile)
        {
            return false;
        }else if (tile == destructibleTile)
        {
            tilemap.SetTile(cell,null);
        }
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);
        if (IfGameOver())
        {
            isOver = true;
            restartButton.SetActive(true);
        }
        return true;

    }
    bool IfGameOver()
    {
        foreach (Tile tile in tilemap.GetTilesBlock(tilemap.cellBounds))
        {
            if (tile == destructibleTile)
                return false;
        }
        return true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
