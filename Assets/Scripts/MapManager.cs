using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    public TileBase soil;
    public TileBase barley_seeds;
    public TileBase corn_seeds;

    public TileBase tomato_seeds;
    public TileBase avocado_seeds;
    public TileBase coffee_seeds;
    public TileBase chili_seeds;

    public int selected_crop = 0;

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
            if(clickedTile && dataFromTiles.ContainsKey(clickedTile)){
                selected_crop = dataFromTiles[clickedTile].crop_type;
                print("Selected crop "+selected_crop +" at "+gridPos);
            }

        }



    if(Input.GetKeyDown("p")){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlantCrop(mousePos);
        }
    if(Input.GetKeyDown("c")){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CollectAll();
        }
    }

    public void PlantCrop(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        if(tileDatas==null){
            return;
        }
        TileBase tile = tilemap.GetTile(gridPosition);
        if(dataFromTiles.ContainsKey(tile)){
            print("Key found");
            if(dataFromTiles[tile].crop_type==-1){
                print("Selecting crop");
                switch(selected_crop){
                        case 1:
                            PlantAll(barley_seeds);
                            break;
                        case 2:
                            PlantAll(corn_seeds);
                            break;
                        case 3:
                            PlantAll(tomato_seeds);
                            break;
                        case 4:
                            PlantAll(avocado_seeds);
                            break;
                        case 5:
                            PlantAll(coffee_seeds);
                            break;
                        case 6: 
                            PlantAll(chili_seeds);
                            break;   
                    }
            }
            print("Planted");

        }
        
    }
    public void CollectCrop(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tileDatas==null){
            return;
        }
        if(dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type!=-1){
            tilemap.SetTile(gridPosition, soil);
            cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, (int) (dataFromTiles[tile].quantity*dataFromTiles[tile].crop_growth/100));
        }
        

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
    public bool IsPlanted(Vector2 worldPosition){
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
    public void PlantAll(TileBase seed){

        int i;

        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(cropManager.GetCropSeeds(selected_crop)>0 && tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type==-1){
                    tilemap.SetTile(gridPosition, seed);
                    cropManager.UpdateCropSeeds(selected_crop, -1);
                    
                }
            }
        }
    }

    public void CollectAll(){
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type!=-1 && !dataFromTiles[tile].isBox){
                    tilemap.SetTile(gridPosition, soil);
                    cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, dataFromTiles[tile].quantity);
                    
                }
            }
        }
    }

}
