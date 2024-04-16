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
    public TileBase soil4;
    public TileBase soil5;
    public TileBase soil6;

    
    private int selected_crop = 0;
    private int selected_land = -1;
    private int current_cycle = 0;
    private int crop_cycle_constant = 20;
    private int update_rate = 1;


    [SerializeField]
    private List<TileData> tileDatas;
    [SerializeField]
    private List<TileBase> seeds;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Dictionary<int,TileBase> soilFromCrop;
    private Dictionary<int,bool> plantedCrops;
    private Dictionary<int,int> CropSpriteCounter;
    private Dictionary<int,int[,]> LandPosition;
    private Dictionary<int,bool>  UnlockedLands;
    private Dictionary<int,int> Land_Crop_Assigned;
    private Dictionary<int,bool> LandIsPlanted;
    private Dictionary<int,int> CropsInLand;
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

    public FinanceManager financeManager;
    public UserController userController;
    public UiControl uiControl;

    public GameObject herramienta;
    public GameObject regadera;

    private int dryrate = 5;


    private bool Disaster = false;


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
        UnlockedLands =new Dictionary<int, bool>();
        Land_Crop_Assigned = new Dictionary<int, int>();
        LandIsPlanted = new Dictionary<int, bool>();
        CropsInLand = new Dictionary<int, int>();
        FindLand();
        UpdateUnlockedLands(new int[]{8,11,12,16,17});
        print("map manager finise¿hed configuration");
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
            print("Tile is at the land " + CheckIfTileIsLand(gridPos));
            if(UnlockedLands.ContainsKey(CheckIfTileIsLand(gridPos))){
                print("The Land is unlocked? "+UnlockedLands[CheckIfTileIsLand(gridPos)]);
                print("Average water " + GetAverageWaterAtLand(CheckIfTileIsLand(gridPos)));
                print("The crop is: "+Land_Crop_Assigned[CheckIfTileIsLand(gridPos)]);
            }
            
        }
    if(Input.GetMouseButtonDown(1)){
            SeeWater(mousePos);
        }

        if(Input.GetKeyDown("p")){
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            if(UnlockedLands.ContainsKey(CheckIfTileIsLand(gridPos))){
                selected_land = CheckIfTileIsLand(gridPos);
                PlantCrop(mousePos);
            }
            
            }
        if(Input.GetKeyDown("c")){
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            if(UnlockedLands.ContainsKey(CheckIfTileIsLand(gridPos))){
                selected_land = CheckIfTileIsLand(gridPos);
                CollectLand();
            }
            
        }
    }

    public void UpdateCycle(){
        current_cycle+=update_rate;
        ChangeCropSprite();
        if(current_cycle%climateManager.currentClimatecycle==0){
            climateManager.ClimateAlreadyExecuted = false;
        }
    }


    public void LoadDataFromMap(List<List<int>> parcelas){
        print("Loading data from map");
        foreach(var parcela in parcelas){
            print("Parcela: "+parcela[0]+" estado "+parcela[1]+" cantidad "+parcela[2]+" agua"+parcela[3]);
            LoadPredefinedMap(parcela[0],parcela[1],parcela[2],parcela[3]);
        }
    }
    public void LoadPredefinedMap(int land, int estado, int cantidad,int agua){
        if(!LandPosition.ContainsKey(land) || UnlockedLands[land]==false){
            return;
        }
        UnlockedLands[land] = true;
        CropsInLand[land] = cantidad;
        int[,] ranges = LandPosition[land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        if(cantidad>0){
            LandIsPlanted[land] = true;
        }
        int crop = Land_Crop_Assigned[land];
        plantedCrops[crop] = true;
        for(int i = x;i<x1+1;i++){
            for(int j=y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && cantidad>0 && !cropManager.cropCycleGrowth.ContainsKey(gridPosition)){
                    cropManager.cropCycleGrowth.Add(gridPosition, new Dictionary<string,int>(){
                        {"growth", estado},
                        {"cycle", current_cycle},
                        {"water",agua},
                        {"crop_type", crop}
                    });
                    cantidad--;
                    switch(crop){
                        case 1:
                            tilemap.SetTile(gridPosition, barley_grow_tiles[estado]);
                            break;
                        case 2:
                            tilemap.SetTile(gridPosition, corn_grow_tiles[estado]);
                            break;
                        case 3:
                            tilemap.SetTile(gridPosition, tomato_grow_tiles[estado]);
                            break;
                        case 4:
                            tilemap.SetTile(gridPosition, avocado_grow_tiles[estado]);
                            break;
                        case 6:
                            tilemap.SetTile(gridPosition, chilli_grow_tiles[estado]);
                            break;
                    }
                }
            }
        }

    }
    public void FastForward(){
        Time.timeScale = 5;
    }
    public void SlowDown(){
        Time.timeScale = 0.3f;
    }

    public void SetSelectedCrop(int crop){
        selected_crop = crop;
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
                            PlantLand(seeds[selected_crop-1]);
                            break;
                        case 2: 
                            plantedCrops[2] = true;
                            PlantLand(seeds[selected_crop-1]);
                            break;
                        case 3:
                            plantedCrops[3] = true;
                            PlantLand(seeds[selected_crop-1]);
                            break;
                        case 4:
                            plantedCrops[4] = true;
                            PlantLand(seeds[selected_crop-1]);
                            break;
                        case 5:
                            plantedCrops[5] = true;
                            PlantLand(seeds[selected_crop-1]);
                            break;
                        case 6: 
                            plantedCrops[6] = true;
                            PlantLand(seeds[selected_crop-1]);
                            break;   
                    }
                ui.hoz();
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
                if(cropManager.GetCropSeeds(selected_crop)>0 && tile && dataFromTiles.ContainsKey(tile) && 
                dataFromTiles[tile].crop_type==-selected_crop && CheckIfTileIsLand(gridPosition)!= -1 
                && CheckIfTileIsLand(gridPosition) == selected_land && UnlockedLands[CheckIfTileIsLand(gridPosition)]){
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
    public void PlantLand(TileBase seed){
        if(!LandPosition.ContainsKey(selected_land) || UnlockedLands[selected_land]==false){
            return;
        }
        int[,] ranges = LandPosition[selected_land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        LandIsPlanted[selected_land] = true;
        for(int i = x;i<x1+1;i++){
            for(int j=y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(cropManager.GetCropSeeds(selected_crop)>0 && tile && dataFromTiles.ContainsKey(tile) && 
                dataFromTiles[tile].crop_type==-selected_crop && CheckIfTileIsLand(gridPosition)!= -1 
                && CheckIfTileIsLand(gridPosition) == selected_land && UnlockedLands[CheckIfTileIsLand(gridPosition)]
                && !cropManager.cropCycleGrowth.ContainsKey(gridPosition)){
                    tilemap.SetTile(gridPosition, seed);
                    cropManager.UpdateCropSeeds(selected_crop, -1);
                    cropManager.cropCycleGrowth.Add(gridPosition, new Dictionary<string,int>(){
                        {"growth", 0},
                        {"cycle", current_cycle},
                        {"water",30},
                        {"crop_type", selected_crop}
                    });
                    CropsInLand[selected_land]++;
                }
            }
        }

    }

    public void FarmerAutomaticPlanting(int land, int crop){
        if(!LandPosition.ContainsKey(land) || UnlockedLands[land]==false){
            return;
        }
        plantedCrops[crop] = true;
        LandIsPlanted[land] = true;
        TileBase seed = seeds[crop-1];
        int[,] ranges = LandPosition[land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        for(int i = x;i<x1+1;i++){
            for(int j=y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(cropManager.GetCropSeeds(crop)>0 && tile && dataFromTiles.ContainsKey(tile) && 
                dataFromTiles[tile].crop_type==-crop && CheckIfTileIsLand(gridPosition)!= -1 
                && CheckIfTileIsLand(gridPosition) == land && UnlockedLands[CheckIfTileIsLand(gridPosition)]
                &&!cropManager.cropCycleGrowth.ContainsKey(gridPosition)){
                    tilemap.SetTile(gridPosition, seed);
                    cropManager.UpdateCropSeeds(crop, -1);
                    cropManager.cropCycleGrowth.Add(gridPosition, new Dictionary<string,int>(){
                        {"growth", 0},
                        {"cycle", current_cycle},
                        {"water",30},
                        {"crop_type", selected_crop}
                    });
                    CropsInLand[land]++;
                }
            }
        }

    }
    public void CollectLand(){
        if(!LandPosition.ContainsKey(selected_land) || UnlockedLands[selected_land]==false){
            return;
        }
        LandIsPlanted[selected_land] = false;
        int[,] ranges = LandPosition[selected_land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        for(int i = x;i<x1+1;i++){
            for(int j=y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0){
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["growth"]>=CropSpriteCounter[dataFromTiles[tile].crop_type]-1){
                        cropManager.cropCycleGrowth.Remove(gridPosition);
                        plantedCrops[dataFromTiles[tile].crop_type] = false;
                        tilemap.SetTile(gridPosition, soilFromCrop[-dataFromTiles[tile].crop_type]);
                        cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, dataFromTiles[tile].quantity);
                        CropsInLand[selected_land]--;
                    }
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
                    }
                }
            }
        }
    }

    public void FarmerAutomaticCollection(int land){
        if(!LandPosition.ContainsKey(land) || UnlockedLands[land]==false){
            return;
        }
        int[,] ranges = LandPosition[land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        for(int i = x;i<x1+1;i++){
            for(int j=y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0){
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["growth"]>=CropSpriteCounter[dataFromTiles[tile].crop_type]-1){
                        cropManager.cropCycleGrowth.Remove(gridPosition);
                        plantedCrops[dataFromTiles[tile].crop_type] = false;
                        tilemap.SetTile(gridPosition, soilFromCrop[-dataFromTiles[tile].crop_type]);
                        cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, dataFromTiles[tile].quantity);
                        CropsInLand[land]--;
                    }
                }
            }
        }
        if(CropsInLand[land]==0){
            LandIsPlanted[land] = false;
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

    public void WaterLand(Vector2 mousePos){
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        if(LandPosition.ContainsKey(CheckIfTileIsLand(gridPos))){
            WaterSpecificLand(CheckIfTileIsLand(gridPos));
        }

    }
    public void WaterSpecificLand(int land){
        if(!LandPosition.ContainsKey(land) || UnlockedLands[land]==false){
            return;
        }
        int[,] ranges = LandPosition[land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];

        for(int i = x;i<x1+1;i++){
            for(int j = y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0){
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
                plantedCrops[1] = false;
                plantedCrops[2] = false;
                plantedCrops[3] = false;   
                plantedCrops[4] = false;
                plantedCrops[5] = false;
                plantedCrops[6] = false;
                break;
            case 4:
                ClimateWaterUpdate();
                plantedCrops[1] = false;
                plantedCrops[2] = false;
                plantedCrops[3] = false;   
                plantedCrops[4] = false;
                plantedCrops[5] = false;
                plantedCrops[6] = false;
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
            if(CheckIfTileIsLand(gridPosition)!=-1){
                if(Disaster && CropsInLand[CheckIfTileIsLand(gridPosition)] != 0){
                    print("PAGAAAAAAAAAAAAANDO");
                    int price = financeManager.GetCropPrice(crop_type);
                    userController.UpdateCapital((int)(CropsInLand[CheckIfTileIsLand(gridPosition)]*(price)*financeManager.TasaInteres(1)));
                    uiControl.ActualizarDinero();
                }
                LandIsPlanted[CheckIfTileIsLand(gridPosition)] = false;
                CropsInLand[CheckIfTileIsLand(gridPosition)] = 0;
            }

        }
    }

    public void SeeWater(Vector2 mousePos){
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        TileBase tile = tilemap.GetTile(gridPos);
        if(tile && cropManager.cropCycleGrowth.ContainsKey(gridPos)){
            print("The crop at: "+gridPos+" has this water: "+cropManager.cropCycleGrowth[gridPos]["water"]);
        }
    }

    public int GetAverageWaterAtLand(int land){
        if(!LandPosition.ContainsKey(land) || UnlockedLands[land]==false){
            return 0;
        }
        int[,] ranges = LandPosition[land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        int average_water = 0;
        int count=0;
        for(int i = x;i<x1+1;i++){
            for(int j = y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && cropManager.cropCycleGrowth.ContainsKey(gridPosition) && dataFromTiles[tile].crop_type>0){
                    average_water+=cropManager.cropCycleGrowth[gridPosition]["water"];
                    count++;
                }
            }
        }
        if(count>0){
            return average_water/=count;
        }
        return 0;
        
    }

    

    public void UpdateVisualWater(int climate){
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type==0 && climate==0){
                    tilemap.SetTile(gridPosition, water_tiles[1]);
                }
                else if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type==0 && climate!=0){
                    tilemap.SetTile(gridPosition, water_tiles[0]);
                }
            }
        }
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
                    LandPosition.Add(numberOfLands,GetLand(i,j));
                    UnlockedLands.Add(numberOfLands-1, false);
                    Land_Crop_Assigned[numberOfLands-1] = -dataFromTiles[tile].crop_type;
                    LandIsPlanted[numberOfLands-1] = false;
                    CropsInLand[numberOfLands-1] = 0;
                }
            }
        }
    }
    public List<int> GetDifferentGrowthsInLand(int land){
        //Declare a list with the items 0,1,2
        List<int> crops = new List<int> {0, 0, 0};
        if(!LandPosition.ContainsKey(land) || UnlockedLands[land]==false){
            return crops;
        }
        int[,] ranges = LandPosition[land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        for(int i = x;i<x1+1;i++){
            for(int j = y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && cropManager.cropCycleGrowth.ContainsKey(gridPosition) && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0){
                    crops[cropManager.cropCycleGrowth[gridPosition]["growth"]]++;
                }
            }
        }
        return crops;
    }

    private int CheckIfTileIsLand(Vector3Int gridPosition){
        foreach (KeyValuePair<int, int[,]> entry in LandPosition){
            if(gridPosition.x>=entry.Value[0,0] && gridPosition.x<=entry.Value[1,0] && gridPosition.y>=entry.Value[0,1] && gridPosition.y<=entry.Value[1,1]){
                return entry.Key;
            }
        }
        return -1;
    }

    private void UpdateUnlockedLands(int[] unlocked){
        foreach(int land in unlocked){
            if(UnlockedLands.ContainsKey(land)){
                UnlockedLands[land] = true;
            }
            
        }
    }

    public void SetDisaster(int climate){
        if(climate == 0 || climate == 3 || climate ==4){
            print("ACTUALIZANDO DESASTRE");
            Disaster = true;
        }
        else{
            print("SIN DESASTRE");
            Disaster = false;
        }
    }

    public void UnlockLand(int land){
        UnlockedLands[land] = true;
    }

    public bool IsLandUnlocked(int land){
        return UnlockedLands[land];
    }

    public int GetCropAtLand(int land){
        return Land_Crop_Assigned[land];
    }

    public bool LandPlanted(int land){
        return LandIsPlanted[land];
    }

    public int GetCropsInLand(int land){
        return CropsInLand[land];
    }

    public void SetCycle(int cycle){
        current_cycle = cycle;
    }

    public List<List<int>> SaveDataFromMap(){
        List<List<int>> parcelas = new List<List<int>>();
        foreach (KeyValuePair<int, int[,]> entry in LandPosition){
            if(UnlockedLands[entry.Key]==false){
                continue;
            }
            List<int> crops = GetDifferentGrowthsInLand(entry.Key);
            for(int i = 0;i<2;i++){
                List<int> parcela = new List<int>();
                parcela.Add(entry.Key);
                parcela.Add(i);
                parcela.Add(crops[i]);
                parcela.Add(GetAverageWaterAtLand(entry.Key));
                parcelas.Add(parcela);
            }
        }
        return parcelas;
    }
}
