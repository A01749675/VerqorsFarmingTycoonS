using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    public TileBase soil;
    public TileBase wheat;
    public TileBase carrot;

    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;
    // Start is called before the first frame update
    public CropManager cropManager;
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
    if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);

            TileBase clickedTile = tilemap.GetTile(gridPos);
            print("At pos: "+gridPos+" tile: "+clickedTile);
            print("Crop type: "+dataFromTiles[clickedTile].crop_type);

        }

    if(Input.GetKeyDown("p")){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlantCrop(mousePos,1);
        }
    if(Input.GetKeyDown("c")){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CollectCrop(mousePos);
        }
    }
    public void PlantCrop(Vector2 worldPosition, int cropType){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return;
        }
        switch(cropType){
            case 1:
                tilemap.SetTile(gridPosition, wheat);
                break;
            case 2:
                tilemap.SetTile(gridPosition, carrot);
                break;
        }
        cropManager.UpdateCropSeeds(cropType, -1);

        
    }
    public void CollectCrop(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return;
        }
        tilemap.SetTile(gridPosition, soil);
        cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, (int) (dataFromTiles[tile].quantity*dataFromTiles[tile].crop_growth/100));

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
    public void UpdateGrowths(){
        foreach(var tileData in tileDatas){
            foreach(var tile in tileData.tiles){
                if(tileData.isPlanted){
                    tileData.crop_growth += 1;
                }
            }
        }
    }

}
