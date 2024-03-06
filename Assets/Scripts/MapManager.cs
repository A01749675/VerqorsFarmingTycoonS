using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private List<TIleData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;
    // Start is called before the first frame update
    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tileData in tileDatas)
        {
            foreach(var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTileCropType(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return -1f;
        }
        int CropType = dataFromTiles[tile].crop_type;
        return CropType;
    }
    public int GetTileCropGrowth(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return -1f;
        }
        int CropGrowth = dataFromTiles[tile].crop_growth;
        return CropGrowth;
    }
    public bool isPlanted(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return false;
        }
        bool isPlanted = dataFromTiles[tile].isPlanted;
        return isPlanted;
    }

}
