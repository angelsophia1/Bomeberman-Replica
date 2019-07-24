using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour {
    public Tilemap tilemap;
    public Tile dirt;
    public GameObject bombPrefab;

	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && !MapDestroyer.isOver)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector3 cellCentrePos = tilemap.GetCellCenterWorld(cell);
            if (tilemap.GetTile<Tile>(cell) == dirt)
            {
                Instantiate(bombPrefab, cellCentrePos, Quaternion.identity);
            }
        }
	}
}
