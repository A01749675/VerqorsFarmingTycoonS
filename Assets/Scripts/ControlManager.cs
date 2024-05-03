using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public GameObject herramienta;
    public GameObject regadera;
    public GameObject maizHoz;
    public GameObject trigoHoz;
    public GameObject tomateHoz;
    public GameObject chileHoz;
    public GameObject aguacateHoz;
    public GameObject cafeHoz;
    
    public UiControl ui;
    public MapManager mapManager;
    private int contadorAgua = 0;

    private bool startedWatering = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShowTool(ui.flagHerramienta,mousePos);
        ShowRegadera(ui.flagRegadera,mousePos);  
    }
    //Método que muestra la hoz con el respectivo cultivo seleccionado
    private void ShowTool(bool tool,Vector2 mousePos){
        if(tool){
            herramienta.SetActive(true);
            herramienta.transform.position = mousePos;
            switch(ui.typecrop){
                case 0:
                    maizHoz.SetActive(false);
                    trigoHoz.SetActive(false);
                    tomateHoz.SetActive(false);
                    chileHoz.SetActive(false);
                    aguacateHoz.SetActive(false);
                    cafeHoz.SetActive(false);
                    mapManager.SetSelectedCrop(0);
                    break;
                case 1:
                    trigoHoz.SetActive(true);
                    maizHoz.SetActive(false);
                    tomateHoz.SetActive(false);
                    chileHoz.SetActive(false);
                    aguacateHoz.SetActive(false);
                    cafeHoz.SetActive(false);
                    mapManager.SetSelectedCrop(1);
                    break;
                case 2:
                    maizHoz.SetActive(true);
                    trigoHoz.SetActive(false);
                    tomateHoz.SetActive(false);
                    chileHoz.SetActive(false);
                    aguacateHoz.SetActive(false);
                    cafeHoz.SetActive(false);
                    mapManager.SetSelectedCrop(2);
                    break;
                case 3:
                    tomateHoz.SetActive(true);
                    maizHoz.SetActive(false);
                    trigoHoz.SetActive(false);
                    chileHoz.SetActive(false);
                    aguacateHoz.SetActive(false);
                    cafeHoz.SetActive(false);
                    mapManager.SetSelectedCrop(3);
                    break;
                case 4: 
                    chileHoz.SetActive(true);
                    maizHoz.SetActive(false);
                    trigoHoz.SetActive(false);
                    tomateHoz.SetActive(false);
                    aguacateHoz.SetActive(false);
                    cafeHoz.SetActive(false);
                    mapManager.SetSelectedCrop(6);
                    break;
                case 5:
                    aguacateHoz.SetActive(true);
                    maizHoz.SetActive(false);
                    trigoHoz.SetActive(false);
                    tomateHoz.SetActive(false);
                    chileHoz.SetActive(false);
                    cafeHoz.SetActive(false);
                    mapManager.SetSelectedCrop(4);
                    break;
                case 6:
                    cafeHoz.SetActive(true);
                    maizHoz.SetActive(false);
                    trigoHoz.SetActive(false);
                    tomateHoz.SetActive(false);
                    chileHoz.SetActive(false);
                    aguacateHoz.SetActive(false);
                    mapManager.SetSelectedCrop(5);
                    break;
            }
        }
        else{
            herramienta.SetActive(false);
            maizHoz.SetActive(false);
            trigoHoz.SetActive(false);
            tomateHoz.SetActive(false);
            chileHoz.SetActive(false);
            aguacateHoz.SetActive(false);
            cafeHoz.SetActive(false);
        }
    }
    //Método que muestra la regadera y riega el cultivo seleccionado (llamando al map manager)
    private void ShowRegadera(bool tool,Vector2 mousePos){
        if(tool){
            regadera.SetActive(true);
            regadera.transform.position = mousePos;
            if (Input.GetMouseButtonDown(0)){
                if(!startedWatering){
                    startedWatering = true;
                    contadorAgua = mapManager.GetCurrentCycle();
                    
                }
                mapManager.WaterLand(mousePos);
            }
            if(mapManager.GetCurrentCycle()-contadorAgua<1){
                regadera.GetComponent<Animator>().SetBool("Regando",true);
            }
            else{
                regadera.GetComponent<Animator>().SetBool("Regando",false);
                if(mapManager.GetCurrentCycle()-contadorAgua>2 && contadorAgua!=0){
                    ui.flagRegadera = false;
                    Cursor.visible = true;
                }
            }
        }
        else{
            contadorAgua=0;
            regadera.SetActive(false);
            startedWatering = false;
        }
    }
}
