using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap; //tilemap (map) en el que se encuentran los tiles de las parcelas, los cultivos y el agua
    
    //Referencia a los tipos de suelo dependiendo del cultivo
    public TileBase soil;
    public TileBase soil2;
    public TileBase soil3;
    public TileBase soil4;
    public TileBase soil5;
    public TileBase soil6;

    //Varialbe para identificar que cultivo se va a plantar
    private int selected_crop = 0;
    //Variable para indetificar en que parcela se va a plantar
    private int selected_land = -1;
    //Variable que actúa como el contador de tiempo del juego
    private int current_cycle = 1;
    //Constante para determinar cada cuanto tiempo se actualiza los tiles de cultivo
    private int crop_cycle_constant = 60;
    //Variable para determinar cada cuanto tiempo se actualiza el ciclo
    private int update_rate = 1;


    [SerializeField]
    private List<TileData> tileDatas; //Lista del ScriptableObject TileData para identificar tiles especiales con comportamientos
    [SerializeField]
    private List<TileBase> seeds; //Lista de los tiles de las semillas de los cultivos
    private Dictionary<TileBase, TileData> dataFromTiles; //Dicionario que relaciona cada tile presente con su tildata
    private Dictionary<int,TileBase> soilFromCrop; //Dicionario que relaciona cada cultivo con su tipo de suelo
    private Dictionary<int,bool> plantedCrops; //Dicionario que indica si un cultivo está plantado
    private Dictionary<int,int> cropSpriteCounter;  //Dicionario que indica el número de sprites de crecimiento de cada cultivo
    private Dictionary<int,int[,]> landPosition; //Dicionario que indica la posición de cada parcela (sus rangos)
    private Dictionary<int,bool>  unlockedLands; //Dicionario que indica si una parcela está desbloqueada
    private Dictionary<int,int> landCropAssigned; //Dicionario que indica el cultivo asignado a cada parcela
    private Dictionary<int,bool> landIsPlanted; //Dicionario que indica si una parcela está plantada
    private Dictionary<int,int> cropsInLand; //Dicionario que indica la cantidad de cultivos en una parcela
    private int numberOfLands = 0; //Variable que indica el número de parcelas

    private AudioSource audioSourcePlant; 
    private AudioSource audioSourceTractor;

    [SerializeField]
    public List<TileBase> chilli_grow_tiles; //Lista de los sprites de crecimiento del chile
    public List<TileBase> barley_grow_tiles; //Lista de los sprites de crecimiento de la "trigo"
    public List<TileBase> corn_grow_tiles; //Lista de los sprites de crecimiento del maíz
    public List<TileBase> tomato_grow_tiles; //Lista de los sprites de crecimiento del tomate
    public List<TileBase> avocado_grow_tiles; //Lista de los sprites de crecimiento del aguacate
    public List<TileBase> coffee_grow_tiles; //Lista de los sprites de crecimiento del "frijoles"

    public List<TileBase> water_tiles; //Lista de los tiles de agua
    


    public CropManager cropManager; //Referencia al CropManager
    public UiControl ui; //Referencia al UiControl
    public ClimateManager climateManager; //Referencia al ClimateManager

    public FinanceManager financeManager; //Referencia al FinanceManager
    public UserController userController; //Referencia al UserController
    public UiControl uiControl; //Referencia al UiControl

    public GameObject herramienta; //Referencia al GameObject de la herramienta
    public GameObject regadera; //Referencia al GameObject de la regadera

    private int dryrate = 5; //Variable que indica la tasa de sequía


    private bool Disaster = false; //Variable que indica si hay un desastre


    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        //Se recorre la lista de TileData para relacionar cada tile con su TileData
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
            {-4,soil4},
            {-5,soil5},
            {-6,soil6}
        };
        cropSpriteCounter = new Dictionary<int, int>(){
            {1,barley_grow_tiles.Count},
            {2,corn_grow_tiles.Count},
            {3,tomato_grow_tiles.Count},
            {4,avocado_grow_tiles.Count},
            {5,coffee_grow_tiles.Count},
            {6,chilli_grow_tiles.Count}
        };
        landPosition = new Dictionary<int, int[,]>();
        unlockedLands =new Dictionary<int, bool>();
        landCropAssigned = new Dictionary<int, int>();
        landIsPlanted = new Dictionary<int, bool>();
        cropsInLand = new Dictionary<int, int>();
        FindLand(); //Se busca las parcelas en el mapa
        UpdateUnlockedLands(new int[]{8}); //Se desbloquea la parcela 8
        InvokeRepeating("UpdateCycle", 0, 1f);
        audioSourcePlant = GameObject.Find("Plantar").GetComponent<AudioSource>();
        audioSourceTractor = GameObject.Find("Tractor").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Método debug para obtener información de un tile
        if(Input.GetMouseButtonDown(2))
        {
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            TileBase clickedTile = tilemap.GetTile(gridPos);
            if(clickedTile && dataFromTiles.ContainsKey(clickedTile)){
                print("Selected crop "+dataFromTiles[clickedTile].crop_type +" at "+gridPos);
            }
            print("Tile is at the land " + CheckIfTileIsLand(gridPos));
            if(unlockedLands.ContainsKey(CheckIfTileIsLand(gridPos))){
                print("The Land is unlocked? "+unlockedLands[CheckIfTileIsLand(gridPos)]);
                print("Average water " + GetAverageWaterAtLand(CheckIfTileIsLand(gridPos)));
                print("The crop is: "+landCropAssigned[CheckIfTileIsLand(gridPos)]);
                print("Crops at land " + cropsInLand[CheckIfTileIsLand(gridPos)]);
            }
            
        }
        //Método para plantar cultivos
        if(Input.GetMouseButtonDown(0)){
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            if(unlockedLands.ContainsKey(CheckIfTileIsLand(gridPos))){
                print(selected_crop);
                selected_land = CheckIfTileIsLand(gridPos);
                PlantCrop(mousePos);
            }
            
        }
        //Método para regar recolectar cultivos
        if(Input.GetMouseButtonDown(1)){
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            if(unlockedLands.ContainsKey(CheckIfTileIsLand(gridPos))){
                selected_land = CheckIfTileIsLand(gridPos);
                CollectLand();
            }
            
        }
    }
    //Método que actualiza el ciclo del juego
    public void UpdateCycle(){
        current_cycle+=update_rate;
        ChangeCropSprite();
        if(current_cycle%climateManager.currentClimatecycle==0){
            climateManager.climateAlreadyExecuted = false;
        }
    }
    //Método que obtiene el ciclo actual
    public int GetUpdateRate(){
        return update_rate;
    }

    //Método que toma los datos del mapa para cada parcela y los carga al inicio del juego 
    public void LoadDataFromMap(List<List<int>> parcelas){
        //print("Loading data from map");
        foreach(var parcela in parcelas){
            //print("Parcela: "+parcela[0]+" estado "+parcela[1]+" cantidad "+parcela[2]+" agua"+parcela[3]);
            LoadPredefinedMap(parcela[0],parcela[1],parcela[2],parcela[3]); //Para cada dato mandado, se cargan los valores al mapa
        }
    }
    //Método que carga los datos de una parcela al inicio del juego
    public void LoadPredefinedMap(int land, int estado, int cantidad,int agua){
        //Checa si está desbloqueada la parcela
        if(!landPosition.ContainsKey(land)){
            return;
        }
        unlockedLands[land] = true;
        cropsInLand[land] = cantidad;
        int[,] ranges = landPosition[land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        if(cantidad>0){
            landIsPlanted[land] = true;
        }
        int crop = landCropAssigned[land];
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
    //Método que guarda los datos de cada parcela al final del juego
    public void FastForward(){
        Time.timeScale = 5;
    }
    //Método que regresa la velocidad del juego a la normalidad
    public void SlowDown(){
        Time.timeScale = 0.3f;
    }
    //Método que establece el cultivo seleccionado
    public void SetSelectedCrop(int crop){
        selected_crop = crop;
    }
    //Método que regresa el ciclo actual
    public int GetCurrentCycle(){
        return current_cycle;
    }
    //Método para plantar cultivos
    public void PlantCrop(Vector2 worldPosition){
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        if(tileDatas==null){
            return;
        }
        TileBase tile = tilemap.GetTile(gridPosition);
        if(tile && dataFromTiles.ContainsKey(tile)){
            //print("Key found");
            if(dataFromTiles[tile].crop_type==-selected_crop){ //checa que el tile seleccionado sea del tipo del crop seleccionado
                //print("Selecting crop");
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
                uiControl.Hoz();
                audioSourcePlant.Play();
            }
            //print("Planted");
            selected_crop=-1000;

        }
        
    }
    //Método que actualiza el sprite de crecimiento de los cultivos cada cierto tiempo 
    public void ChangeCropSprite(){
        if(plantedCrops[1] || plantedCrops[2] || plantedCrops[3] || plantedCrops[4] || plantedCrops[5] || plantedCrops[6]){
            int i;
            //recorre todo el mapa y actualiza los sprites de crecimiento de los cultivos
            for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
                for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                    Vector3Int gridPosition = new Vector3Int(i, j, 0);
                    TileBase tile = tilemap.GetTile(gridPosition);
                    if(tileDatas==null){
                        return;
                    }
                    //Si el tile es un tile de cultivo, se actualiza su sprite de crecimiento
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && (current_cycle-cropManager.cropCycleGrowth[gridPosition]["cycle"])%crop_cycle_constant==0){
                        if(tile && dataFromTiles.ContainsKey(tile)){  
                            //se actualiza el sprite dependiendo del tipo de cultivo
                            switch(dataFromTiles[tile].crop_type){
                                case 1:
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,1);
                                    tilemap.SetTile(gridPosition, barley_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    break;
                                case 2:
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,2);
                                    tilemap.SetTile(gridPosition, corn_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    break;
                                case 3:
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,3);
                                    tilemap.SetTile(gridPosition, tomato_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    break;
                                case 4:
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,4);
                                    tilemap.SetTile(gridPosition, avocado_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    break;
                                case 5:
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,5);
                                    tilemap.SetTile(gridPosition, coffee_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    break;
                                case 6:
                                    cropManager.cropCycleGrowth[gridPosition]["growth"] = UpdateCropSpriteCycle(gridPosition,6);
                                    tilemap.SetTile(gridPosition, chilli_grow_tiles[cropManager.cropCycleGrowth[gridPosition]["growth"]]);
                                    break;
                                }
                        }
                    }
                    //Actualiza la cantidad de agua en cada tile de cultivo
                    if(tile && dataFromTiles.ContainsKey(tile) && cropManager.cropCycleGrowth.ContainsKey(gridPosition) && (current_cycle-cropManager.cropCycleGrowth[gridPosition]["cycle"])%35==0){
                        UpdateTileWater(gridPosition,dataFromTiles[tile].crop_type);
                    }

                }
            }
        }
    }
    //Método que actualiza el valor de crecimiento del tile de cultivo
    private int UpdateCropSpriteCycle(Vector3Int gridPosition,int cropType){
        int cycle = cropManager.cropCycleGrowth[gridPosition]["growth"];
        switch(cropType){
            case 1:
                if(cycle==barley_grow_tiles.Count-1){
                    return cycle;
                }
                cycle=(cycle+1)%barley_grow_tiles.Count;
                break;
            case 2:
                if(cycle==corn_grow_tiles.Count-1){
                    return cycle;
                }
                cycle=(cycle+1)%corn_grow_tiles.Count;
                break;
            case 3:
            if(cycle==tomato_grow_tiles.Count-1){
                    return cycle;
                }
                cycle=(cycle+1)%tomato_grow_tiles.Count;
                break;
            case 4:
                if(cycle==avocado_grow_tiles.Count-1){
                    return cycle;
                }
                cycle=(cycle+1)%avocado_grow_tiles.Count;
                break;
            case 5:
                if(cycle==coffee_grow_tiles.Count-1){
                        return cycle;
                }   
                cycle=(cycle+1)%coffee_grow_tiles.Count;
                break;
            case 6:
                if(cycle==chilli_grow_tiles.Count-1){
                    return cycle;
                }
                cycle=(cycle+1)%chilli_grow_tiles.Count;
                break;
        }
        return cycle;
    }

    //Método que planta un cultivo en una parcela en particular
    public void PlantLand(TileBase seed){
        if(!landPosition.ContainsKey(selected_land) || unlockedLands[selected_land]==false){
            return;
        }
        int[,] ranges = landPosition[selected_land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        landIsPlanted[selected_land] = true;
        //Para los rangos de la parcela, se recorre cada tile y se planta la semilla
        for(int i = x;i<x1+1;i++){
            for(int j=y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(cropManager.GetCropSeeds(selected_crop)>0 && tile && dataFromTiles.ContainsKey(tile) && 
                dataFromTiles[tile].crop_type==-selected_crop && CheckIfTileIsLand(gridPosition)!= -1 
                && CheckIfTileIsLand(gridPosition) == selected_land
                && !cropManager.cropCycleGrowth.ContainsKey(gridPosition)){
                    tilemap.SetTile(gridPosition, seed);
                    cropManager.UpdateCropSeeds(selected_crop, -1);
                    cropManager.cropCycleGrowth.Add(gridPosition, new Dictionary<string,int>(){
                        {"growth", 0},
                        {"cycle", current_cycle},
                        {"water",30},
                        {"crop_type", selected_crop}
                    });
                    cropsInLand[selected_land]++;
                }
            }
        }

    }

    //Método llamado por el EmployeeManager para plantar cultivos 
    public void FarmerAutomaticPlanting(int land, int crop){
        if(!landPosition.ContainsKey(land) || unlockedLands[land]==false){
            return;
        }
        plantedCrops[crop] = true;
        landIsPlanted[land] = true;
        TileBase seed = seeds[crop-1];
        int[,] ranges = landPosition[land];
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
                && CheckIfTileIsLand(gridPosition) == land && unlockedLands[CheckIfTileIsLand(gridPosition)]
                &&!cropManager.cropCycleGrowth.ContainsKey(gridPosition)){
                    tilemap.SetTile(gridPosition, seed);
                    cropManager.UpdateCropSeeds(crop, -1);
                    cropManager.cropCycleGrowth.Add(gridPosition, new Dictionary<string,int>(){
                        {"growth", 0},
                        {"cycle", current_cycle},
                        {"water",30},
                        {"crop_type", selected_crop}
                    });
                    cropsInLand[land]++;
                }
            }
        }
        audioSourcePlant.Play();

    }
    //Método para recolectar cultivos en una parcela en particular
    public void CollectLand(){
        if(!landPosition.ContainsKey(selected_land) || unlockedLands[selected_land]==false){
            return;
        }
        landIsPlanted[selected_land] = false;
        int[,] ranges = landPosition[selected_land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        
        for(int i = x;i<x1+1;i++){
            for(int j=y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0){
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["growth"]>=cropSpriteCounter[dataFromTiles[tile].crop_type]-1){
                        cropManager.cropCycleGrowth.Remove(gridPosition);
                        plantedCrops[dataFromTiles[tile].crop_type] = false;
                        tilemap.SetTile(gridPosition, soilFromCrop[-dataFromTiles[tile].crop_type]);
                        cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, dataFromTiles[tile].quantity);
                        cropsInLand[selected_land]--;
                    }
                }
            }
        }
    }

    //Método llamado por el TruckManager para recolectar cultivos
    public void FarmerAutomaticCollection(int land){
        if(!landPosition.ContainsKey(land) || unlockedLands[land]==false){
            return;
        }
        int[,] ranges = landPosition[land];
        int x = ranges[0,0];
        int y = ranges[0,1];
        int x1 = ranges[1,0];
        int y1 = ranges[1,1];
        for(int i = x;i<x1+1;i++){
            for(int j=y;j<y1+1;j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0){
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["growth"]>=cropSpriteCounter[dataFromTiles[tile].crop_type]-1){
                        cropManager.cropCycleGrowth.Remove(gridPosition);
                        plantedCrops[dataFromTiles[tile].crop_type] = false;
                        tilemap.SetTile(gridPosition, soilFromCrop[-dataFromTiles[tile].crop_type]);
                        cropManager.UpdateCropQuantity(dataFromTiles[tile].crop_type, dataFromTiles[tile].quantity);
                        cropsInLand[land]--;
                        audioSourceTractor.Play();
                    }
                }
            }
        }
        
        if(cropsInLand[land]==0){
            landIsPlanted[land] = false;
        }
    }

    //Método que actualiza el agua de una parcela en particular, y es llamado por el controlmanager
    public void WaterLand(Vector2 mousePos){
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        if(landPosition.ContainsKey(CheckIfTileIsLand(gridPos))){
            WaterSpecificLand(CheckIfTileIsLand(gridPos));
        }

    }
    //Método que actualiza el agua de una parcela en particular
    public void WaterSpecificLand(int land){
        if(!landPosition.ContainsKey(land) || unlockedLands[land]==false){
            return;
        }
        int[,] ranges = landPosition[land];
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
    //Método que recorre todos los tiles y cambia el nivel de agua en cada una dependiendo del clima
    public void ClimateWaterUpdate(){
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && dataFromTiles[tile].crop_type>0){
                    if(cropManager.cropCycleGrowth.ContainsKey(gridPosition) && cropManager.cropCycleGrowth[gridPosition]["water"]<100){
                        cropManager.cropCycleGrowth[gridPosition]["water"] = climateManager.GetCurrentClimate()["water"];
                        UpdateTileWater(gridPosition,dataFromTiles[tile].crop_type);
                    }
                }
            }
        }
    }
    //Método que actualiza la tasa de sequía dependiendo del clima
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
                dryrate = -100;
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
    //Método que actualiza el agua en cada tile de cultivo y si el agua es menor a 10 o mayor a 110, se elimina el cultivo
    public void UpdateTileWater(Vector3Int gridPosition,int crop_type){
        
        if(cropManager.cropCycleGrowth.ContainsKey(gridPosition)){
            cropManager.cropCycleGrowth[gridPosition]["water"]-= dryrate;
        }
        if(cropManager.cropCycleGrowth[gridPosition]["water"]<10 || cropManager.cropCycleGrowth[gridPosition]["water"]>110){
            tilemap.SetTile(gridPosition, soilFromCrop[-crop_type]);
            cropManager.cropCycleGrowth.Remove(gridPosition);
            if(CheckIfTileIsLand(gridPosition)!=-1){
                if(Disaster && cropsInLand[CheckIfTileIsLand(gridPosition)] != 0){
                    int price = financeManager.GetCropPrice(crop_type);
                    userController.UpdateCapital((int)(cropsInLand[CheckIfTileIsLand(gridPosition)]*(price)*financeManager.GetSeguro(userController.GetParameter("financiamiento"))));
                    uiControl.ActualizarDinero();
                }
                landIsPlanted[CheckIfTileIsLand(gridPosition)] = false;
                cropsInLand[CheckIfTileIsLand(gridPosition)] = 0;
            }
        }
    }

    //Método que calcula el promedio del nivel de agua en una parcela en particular
    public int GetAverageWaterAtLand(int land){
        if(!landPosition.ContainsKey(land) || unlockedLands[land]==false){
            return 0;
        }
        int[,] ranges = landPosition[land];
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

    
    //Método que actualiza el mapa dependiendo de si hay sequía o no
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

    //Método que revisa si un tile es suelo
    private bool IsSoil(int crop_type){
        if(crop_type <= -1 && crop_type>=-6){
            return true;
        }
        return false;
    }
    //Método que calcula los rangos de una parcela
    private int[,] GetLand(int x, int y){
        bool xTrue = true;
        bool yTrue = true;
        int x1 = x;
        int y1 = y;
            TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
            //va checando si el tile revisado es suelo y cuando termina resta 1 y pasa a revisar la y
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
    //Método que busca las parcelas en el mapa y las guarda con sus datos en los diferentes diccionarios
    private void FindLand(){
        int i = 0;
        for(i = -2*tilemap.size.x; i<2*tilemap.size.x; i++){
            for(int j = -2*tilemap.size.y; j<2*tilemap.size.y; j++){
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(gridPosition);
                if(tile && dataFromTiles.ContainsKey(tile) && IsSoil(dataFromTiles[tile].crop_type) && CheckIfTileIsLand(gridPosition)==-1){
                    landPosition.Add(numberOfLands,GetLand(i,j));
                    unlockedLands.Add(numberOfLands-1, false);
                    landCropAssigned[numberOfLands-1] = -dataFromTiles[tile].crop_type;
                    landIsPlanted[numberOfLands-1] = false;
                    cropsInLand[numberOfLands-1] = 0;
                }
            }
        }
    }
    //Método que regresa un contador de crecimiento para los cultivos en cada parcela
    public List<int> GetDifferentGrowthsInLand(int land){
        //Declare a list with the items 0,1,2
        List<int> crops = new List<int> {0, 0, 0};
        if(!landPosition.ContainsKey(land) || unlockedLands[land]==false){
            return crops;
        }
        int[,] ranges = landPosition[land];
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

    //Método que revisa checa tile es una parcela
    private int CheckIfTileIsLand(Vector3Int gridPosition){
        foreach (KeyValuePair<int, int[,]> entry in landPosition){
            if(gridPosition.x>=entry.Value[0,0] && gridPosition.x<=entry.Value[1,0] && gridPosition.y>=entry.Value[0,1] && gridPosition.y<=entry.Value[1,1]){
                return entry.Key;
            }
        }
        return -1;
    }
    //Método que actualiza las parcelas desbloqueadas
    public void UpdateUnlockedLands(int[] unlocked){
        foreach(int land in unlocked){
            if(unlockedLands.ContainsKey(land)){
                unlockedLands[land] = true;
            }
            
        }
    }
    //Método que establece si hay un desastre
    public void SetDisaster(int climate){
        if(climate == 0 || climate == 3 || climate ==4){
            //print("ACTUALIZANDO DESASTRE");
            Disaster = true;
        }
        else{
            //print("SIN DESASTRE");
            Disaster = false;
        }
    }
    //Método que desbloque una tierra
    public void UnlockLand(int land){
        unlockedLands[land] = true;
    }
    //Método que regresa si una tierra está desbloqueada
    public bool IsLandUnlocked(int land){
        return unlockedLands[land];
    }
    //Método que regresa el cultivo de la parcela
    public int GetCropAtLand(int land){
        return landCropAssigned[land];
    }

    //Método que regresa si una parcela está plantada
    public bool LandPlanted(int land){
        return landIsPlanted[land];
    }
    //Método que cuenta cuantoss cultivos hay en la parcela 
    public int GetCropsInLand(int land){
        return cropsInLand[land];
    }
    //Método que actualiza el ciclo actual
    public void SetCycle(int cycle){
        current_cycle = cycle;
    }
    //Método que almacena la información de las parcelas en una lista de listas
    public List<List<int>> SaveDataFromMap(){
        List<List<int>> parcelas = new List<List<int>>();
        foreach (KeyValuePair<int, int[,]> entry in landPosition){
            if(unlockedLands[entry.Key]==false){
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
