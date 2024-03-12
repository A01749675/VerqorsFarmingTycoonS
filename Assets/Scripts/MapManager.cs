using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    public TileBase soil;
    public TileBase soil2;
    public TileBase soil3;
    public TileBase barley_seeds;
    public TileBase corn_seeds;
    public TileBase tomato_seeds;
    public TileBase avocado_seeds;
    public TileBase coffee_seeds;
    public TileBase chili_seeds;
    private int selected_crop = 0;
    private int current_cycle = 0;

    private int chili_cycle = 0;
    private int barley_cycle = 0;
    private int corn_cycle = 0;
    private int tomato_cycle = 0;
    private int avocado_cycle = 0;


    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Dictionary<int,TileBase> soilFromCrop;
    private Dictionary<int,bool> plantedCrops;
    [SerializeField]
    public List<TileBase> chilli_grow_tiles;
    public List<TileBase> barley_grow_tiles;
    public List<TileBase> corn_grow_tiles;
    public List<TileBase> tomato_grow_tiles;
    public List<TileBase> avocado_grow_tiles;
    public List<TileBase> coffee_grow_tiles;

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
        plantedCrops = new Dictionary<int, bool>(){
            {1, false},
            {2, false},
            {3, false},
            {4, false},
            {5, false},
            {6, false}
        };
        soilFromCrop = new Dictionary<int, TileBase>(){
            {-1, soil},
            {-2,soil2},
            {-3,soil3}
        };
        InvokeRepeating("UpdateCycle", 0, 1f);

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

    public void UpdateCycle(){
        print(current_cycle);
        current_cycle++;
        ChangeCropSprite();
    }

    public void PlantCrop(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        if(tileDatas==null){
            return;
        }
        TileBase tile = tilemap.GetTile(gridPosition);
        if(dataFromTiles.ContainsKey(tile)){
            print("Key found");
            if(dataFromTiles[tile].crop_type==-selected_crop){
                print("Selecting crop");
                switch(selected_crop){
                        case 1:
                            plantedCrops[1] = true;
                            PlantAll(barley_seeds);
                            break;
                        case 2: 
                            plantedCrops[2] = true;
                            PlantAll(corn_seeds);
                            break;
                        case 3:
                            plantedCrops[3] = true;
                            PlantAll(tomato_seeds);
                            break;
                        case 4:
                            plantedCrops[4] = true;
                            PlantAll(avocado_seeds);
                            break;
                        case 5:
                            plantedCrops[5] = true;
                            PlantAll(coffee_seeds);
                            break;
                        case 6: 
                            plantedCrops[6] = true;
                            PlantAll(chili_seeds);
                            break;   
                    }
            }
            print("Planted");

        }
        
    }
    public void ChangeCropSprite(){
        if(current_cycle%15==0){
            UpdateCropCycle();
            int i;
            for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
                for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                    Vector3Int gridPosition = new Vector3Int(i, j, 0);
                    TileBase tile = tilemap.GetTile(gridPosition);
                    if(tileDatas==null){
                        return;
                    }
                    if(tile && dataFromTiles.ContainsKey(tile) && !dataFromTiles[tile].isBox){  
                        switch(dataFromTiles[tile].crop_type){
                            case 1:
                                tilemap.SetTile(gridPosition, barley_grow_tiles[barley_cycle]);
                                break;
                            case 2:
                                tilemap.SetTile(gridPosition, corn_grow_tiles[corn_cycle]);
                                break;
                            case 3:
                                tilemap.SetTile(gridPosition, tomato_grow_tiles[tomato_cycle]);
                                break;
                            case 4:
                                tilemap.SetTile(gridPosition, avocado_grow_tiles[avocado_cycle]);
                                break;
                            case 6:
                                tilemap.SetTile(gridPosition, chilli_grow_tiles[chili_cycle]);
                                break;
                            }
                        
                    }
                }
            }
        }
    }

    private void UpdateCropCycle(){
        if(plantedCrops[1]){
            if(barley_cycle==barley_grow_tiles.Count-1){
                barley_cycle=barley_grow_tiles.Count-1;
            }
            else{
                barley_cycle=(barley_cycle+1)%barley_grow_tiles.Count;
            }
        }
        if(plantedCrops[2]){
            if(corn_cycle==corn_grow_tiles.Count-1){
                corn_cycle=corn_grow_tiles.Count-1;
            }
            else{
                corn_cycle=(corn_cycle+1)%corn_grow_tiles.Count;
            }
        }
        if(plantedCrops[3]){
            if(tomato_cycle==tomato_grow_tiles.Count-1){
                tomato_cycle=tomato_grow_tiles.Count-1;
            }
            else{
                tomato_cycle=(tomato_cycle+1)%tomato_grow_tiles.Count;
            }
        }
        if(plantedCrops[4]){
            if(avocado_cycle==avocado_grow_tiles.Count-1){
                avocado_cycle=avocado_grow_tiles.Count-1;
            }
            else{
                avocado_cycle=(avocado_cycle+1)%avocado_grow_tiles.Count;
            }
        }
        if(plantedCrops[6]){
            if(chili_cycle==chilli_grow_tiles.Count-1){
                chili_cycle=chilli_grow_tiles.Count-1;
            }
            else{
                chili_cycle=(chili_cycle+1)%chilli_grow_tiles.Count;
            }
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
                if(cropManager.GetCropSeeds(selected_crop)>0 && tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type==-selected_crop){
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
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0 && !dataFromTiles[tile].isBox){
                    plantedCrops[dataFromTiles[tile].crop_type] = false;
                    tilemap.SetTile(gridPosition, soilFromCrop[-dataFromTiles[tile].crop_type]);
                    cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, dataFromTiles[tile].quantity);
                }
            }
        }
    }

}
