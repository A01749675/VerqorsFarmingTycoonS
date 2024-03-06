using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private List<TileData> tileDatas;

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
    public void PlantCrop(Vector2 worldPosition, int cropType){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return;
        }
        dataFromTiles[tile].crop_type = cropType;
        dataFromTiles[tile].isPlanted = true;
    }
    public void CollectCrop(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return;
        }
        dataFromTiles[tile].crop_type = -1;
        dataFromTiles[tile].crop_growth = 0;
        dataFromTiles[tile].isPlanted = false;
        dataFromTiles[tile].quantity = 0;
    }
    public void WaterCrop(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return;
        }
        if(dataFromTiles[tile].water < 100 && dataFromTiles[tile].isPlanted){
            dataFromTiles[tile].water += 5;
        }
    }

    public int GetTileCropType(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return -1;
        }
        int CropType = dataFromTiles[tile].crop_type;
        return CropType;
    }
    public int GetTileCropGrowth(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return -1;
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
    public int GetTileQuantity(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return -1;
        }
        int Quantity = dataFromTiles[tile].quantity;
        return Quantity;
    }
    public int GetTileWater(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return -1;
        }
        int Water = dataFromTiles[tile].water;
        return Water;
    }

}
