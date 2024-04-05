using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    public TileBase soil;
    public TileBase soil2;
    public TileBase soil3;
    public TileBase soil6;
    public TileBase barley_seeds;
    public TileBase corn_seeds;
    public TileBase tomato_seeds;
    public TileBase avocado_seeds;
    public TileBase coffee_seeds;
    public TileBase chili_seeds;
    private int selected_crop = 0;
    private int current_cycle = 0;
    private int crop_cycle_constant = 20;
    private int update_rate = 1;


    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Dictionary<int,TileBase> soilFromCrop;
    private Dictionary<int,bool> plantedCrops;

    private Dictionary<int,int> CropSpriteCounter;

    private Dictionary<int,int[,]> LandPosition;
    private int numberOfLands = 0;

    

    [SerializeField]
    public List<TileBase> chilli_grow_tiles;
    public List<TileBase> barley_grow_tiles;
    public List<TileBase> corn_grow_tiles;
    public List<TileBase> tomato_grow_tiles;
    public List<TileBase> avocado_grow_tiles;
    public List<TileBase> coffee_grow_tiles;

    public List<TileBase> water_tiles;
    public List<TileBase> building_tiles;

    // Start is called before the first frame update
    public CropManager cropManager;

    public UiControl ui;

    public ClimateManager climateManager;

    public GameObject herramienta;
    public GameObject regadera;

    private int dryrate = 5;


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
            {-3,soil3},
            {-6,soil6}
        };
        CropSpriteCounter = new Dictionary<int, int>(){
            {1,barley_grow_tiles.Count},
            {2,corn_grow_tiles.Count},
            {3,tomato_grow_tiles.Count},
            {4,avocado_grow_tiles.Count},
            {5,coffee_grow_tiles.Count},
            {6,chilli_grow_tiles.Count}
        };
        LandPosition = new Dictionary<int, int[,]>();
        FindLand();
        InvokeRepeating("UpdateCycle", 0, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    if(Input.GetMouseButtonDown(0))
        {
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            TileBase clickedTile = tilemap.GetTile(gridPos);
            if(clickedTile && dataFromTiles.ContainsKey(clickedTile)){
                selected_crop = dataFromTiles[clickedTile].crop_type;
                print("Selected crop "+selected_crop +" at "+gridPos);
            }
            print("Tile is at: " + CheckIfTileIsLand(gridPos));
        }
    if(Input.GetMouseButtonDown(1)){
            SeeWater(mousePos);
        }

        if(Input.GetKeyDown("p")){
                PlantCrop(mousePos);
            }
        if(Input.GetKeyDown("c")){
                CollectSpecificCrop();
            }
    }

    public void UpdateCycle(){
        current_cycle+=update_rate;
        ChangeCropSprite();
        if(current_cycle%climateManager.currentClimatecycle==0){
            climateManager.ClimateAlreadyExecuted = false;
        }
    }

    public void FastForward(int value){
        update_rate=10;
    }
    public void SlowDown(int value){
        update_rate=1;
    }

    public int GetCurrentCycle(){
        return current_cycle;
    }

    public void PlantCrop(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        if(tileDatas==null){
            return;
        }
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tile && dataFromTiles.ContainsKey(tile)){
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
        if(plantedCrops[1] || plantedCrops[2] || plantedCrops[3] || plantedCrops[4] || plantedCrops[5] || plantedCrops[6]){
            int i;
            for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
                for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                    Vector3Int gridPosition = new Vector3Int(i, j, 0);
                    TileBase tile = tilemap.GetTile(gridPosition);
                    if(tileDatas==null){
                        return;
                    }
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && (current_cycle-cropManager.cropCycleGrowth[gridPosition]["cycle"])%crop_cycle_constant==0){
                        if(tile && dataFromTiles.ContainsKey(tile) && !dataFromTiles[tile].isBox){  
                            switch(dataFromTiles[tile].crop_type){
                                case 1:
                                    tilemap.SetTile(gridPosition, barley_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,1);
                                    break;
                                case 2:
                                    tilemap.SetTile(gridPosition, corn_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,2);
                                    break;
                                case 3:
                                    tilemap.SetTile(gridPosition, tomato_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,3);
                                    break;
                                case 4:
                                    tilemap.SetTile(gridPosition, avocado_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,4);
                                    break;
                                case 6:
                                    tilemap.SetTile(gridPosition, chilli_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,6);
                                    break;
                                }
                                UpdateTileWater(gridPosition,dataFromTiles[tile].crop_type);
                        }
                    }

                }
            }
        }
    }
    private int UpdateCropSpriteCycle(Vector3Int gridPosition,int cropType){
        int cycle = cropManager.cropCycleGrowth[gridPosition]["growth"];
        switch(cropType){
            case 1:
                if(cycle==barley_grow_tiles.Count-1){
                    cycle=barley_grow_tiles.Count-1;
                }
                else{
                    cycle=(cycle+1)%barley_grow_tiles.Count;
                }
                break;
            case 2:
                if(cycle==corn_grow_tiles.Count-1){
                    cycle=corn_grow_tiles.Count-1;
                }
                else{
                    cycle=(cycle+1)%corn_grow_tiles.Count;
                }
                break;
            case 3:
                if(cycle==tomato_grow_tiles.Count-1){
                    cycle=tomato_grow_tiles.Count-1;
                }
                else{
                    cycle=(cycle+1)%tomato_grow_tiles.Count;
                }
                break;
            case 4:
                if(cycle==avocado_grow_tiles.Count-1){
                    cycle=avocado_grow_tiles.Count-1;
                }
                else{
                    cycle=(cycle+1)%avocado_grow_tiles.Count;
                }
                break;
            case 5:
                if(cycle==coffee_grow_tiles.Count-1){
                    cycle=coffee_grow_tiles.Count-1;
                }
                else{
                    cycle=(cycle+1)%coffee_grow_tiles.Count;
                }
                break;
            case 6:
                if(cycle==chilli_grow_tiles.Count-1){
                    cycle=chilli_grow_tiles.Count-1;
                }
                else{
                    cycle=(cycle+1)%chilli_grow_tiles.Count;
                }
                break;
        }
        return cycle;
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
                    cropManager.cropCycleGrowth.Add(gridPosition, new Dictionary<string,int>(){
                        {"growth", 0},
                        {"cycle", current_cycle},
                        {"water",30},
                        {"crop_type", selected_crop}
                    });
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
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["growth"]>=2){
                        cropManager.cropCycleGrowth.Remove(gridPosition);
                        plantedCrops[dataFromTiles[tile].crop_type] = false;
                        tilemap.SetTile(gridPosition, soilFromCrop[-dataFromTiles[tile].crop_type]);
                        cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, dataFromTiles[tile].quantity);
                        cropManager.cropCycleGrowth.Remove(gridPosition);
                    }
                }
            }
        }
    }

    public void CollectSpecificCrop(){
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type==selected_crop && !dataFromTiles[tile].isBox){
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["growth"]>=CropSpriteCounter[selected_crop]-1){
                        cropManager.cropCycleGrowth.Remove(gridPosition);
                        plantedCrops[dataFromTiles[tile].crop_type] = false;
                        tilemap.SetTile(gridPosition, soilFromCrop[-dataFromTiles[tile].crop_type]);
                        cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, dataFromTiles[tile].quantity);
                        cropManager.cropCycleGrowth.Remove(gridPosition);
                    }
                }
            }
        }
    }
    public void WaterAll(){
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0 && !dataFromTiles[tile].isBox){
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["water"]<100){
                        cropManager.cropCycleGrowth[gridPosition]["water"] += 25;
                    }
                }
            }
        }
    }
    public void WaterSpecificCrop(){
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type==selected_crop && !dataFromTiles[tile].isBox){
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["water"]<100){
                        cropManager.cropCycleGrowth[gridPosition]["water"] += 25;
                    }
                }
            }
        }
    }

    public void ClimateWaterUpdate(){
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0 && !dataFromTiles[tile].isBox){
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["water"]<100){
                        cropManager.cropCycleGrowth[gridPosition]["water"] = climateManager.GetCurrentClimate()["water"];
                        UpdateTileWater(gridPosition,dataFromTiles[tile].crop_type);
                    }
                }
            }
        }
    }

    public void WaterRate(int climate){
        switch(climate){
            case 0:
                dryrate = 25;
                break;
            case 1:
                dryrate = 5;
                break;
            case 2:
                dryrate = -10;
                break;
            case 3:
                ClimateWaterUpdate();
                break;
            case 4:
                ClimateWaterUpdate();
                break;
        }
    }

    public void UpdateTileWater(Vector3Int gridPosition,int crop_type){
        
        if(cropManager.cropCycleGrowth.ContainsKey(gridPosition)){
            cropManager.cropCycleGrowth[gridPosition]["water"]-= dryrate;
        }
        if(cropManager.cropCycleGrowth[gridPosition]["water"]<10 || cropManager.cropCycleGrowth[gridPosition]["water"]>110){
            tilemap.SetTile(gridPosition, soilFromCrop[-crop_type]);
            cropManager.cropCycleGrowth.Remove(gridPosition);
        }
    }

    public void SeeWater(Vector2 mousePos){
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        TileBase tile = tilemap.GetTile(gridPos);
        if(tile && cropManager.cropCycleGrowth.ContainsKey(gridPos)){
            print("The crop at: "+gridPos+" has this water: "+cropManager.cropCycleGrowth[gridPos]["water"]);
        }
    }

    public void UpdateVisualWater(int climate){
        print("Drying");
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type==0 && climate==0){
                    tilemap.SetTile(gridPosition, water_tiles[1]);
                }
                else if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type==0 && climate==3){
                    tilemap.SetTile(gridPosition, water_tiles[0]);
                }
            }
        }
    }

    public void FireAccidentTileUpdate(){

    }

    private bool IsSoil(int crop_type){
        if(crop_type <= -1 && crop_type>=-6){
            return true;
        }
        return false;
    }

    private int[,] GetLand(int x, int y){
        bool xTrue = true;
        bool yTrue = true;
        int x1 = x;
        int y1 = y;
            TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
            while(xTrue){
                tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if(tile && dataFromTiles.ContainsKey(tile) && IsSoil(dataFromTiles[tile].crop_type) && xTrue){
                    x++;
                }
                else{
                    xTrue = false;
                }
            }
            x--;
            while(yTrue){
                tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if(tile && dataFromTiles.ContainsKey(tile) && IsSoil(dataFromTiles[tile].crop_type) && yTrue){
                    y++;
                }
                else{
                    yTrue = false;
                }
            }
            y--;
        int[,] result = {{x1,y1},{x,y}};
        print("Land dimensions "+x1+" "+y1+"|"+x+" "+y);
        numberOfLands++;
        return result;

    }
    private void FindLand(){
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && IsSoil(dataFromTiles[tile].crop_type) && CheckIfTileIsLand(gridPosition)==-1){
                    print("Is Soil");
                    print("Found Land at: "+i+" "+j);
                    LandPosition.Add(numberOfLands,GetLand(i,j));
                }

            }
        }
    }


    private int CheckIfTileIsLand(Vector3Int gridPosition){
        foreach (KeyValuePair<int, int[,]> entry in LandPosition){
            if(gridPosition.x>=entry.Value[0,0] && gridPosition.x<=entry.Value[1,0] && gridPosition.y>=entry.Value[0,1] && gridPosition.y<=entry.Value[1,1]){
                return entry.Key;
            }
        }
        return -1;
    }

}
