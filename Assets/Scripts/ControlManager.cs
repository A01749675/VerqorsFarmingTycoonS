using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public GameObject herramienta;
    public GameObject regadera;
    public GameObject Maízhoz;
    public GameObject Trigohoz;
    public GameObject Tomatehoz;
    public GameObject Chilehoz;
    public GameObject Aguacatehoz;
    public GameObject Caféhoz;
    
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
    private void ShowTool(bool tool,Vector2 mousePos){
        if(tool){
            herramienta.SetActive(true);
            herramienta.transform.position = mousePos;
            switch(ui.typecrop){
                case 0:
                    Maízhoz.SetActive(false);
                    Trigohoz.SetActive(false);
                    Tomatehoz.SetActive(false);
                    Chilehoz.SetActive(false);
                    Aguacatehoz.SetActive(false);
                    Caféhoz.SetActive(false);
                    mapManager.SetSelectedCrop(0);
                    break;
                case 1:
                    Trigohoz.SetActive(true);
                    Maízhoz.SetActive(false);
                    Tomatehoz.SetActive(false);
                    Chilehoz.SetActive(false);
                    Aguacatehoz.SetActive(false);
                    Caféhoz.SetActive(false);
                    mapManager.SetSelectedCrop(1);
                    break;
                case 2:
                    Maízhoz.SetActive(true);
                    Trigohoz.SetActive(false);
                    Tomatehoz.SetActive(false);
                    Chilehoz.SetActive(false);
                    Aguacatehoz.SetActive(false);
                    Caféhoz.SetActive(false);
                    mapManager.SetSelectedCrop(2);
                    break;
                case 3:
                    Tomatehoz.SetActive(true);
                    Maízhoz.SetActive(false);
                    Trigohoz.SetActive(false);
                    Chilehoz.SetActive(false);
                    Aguacatehoz.SetActive(false);
                    Caféhoz.SetActive(false);
                    mapManager.SetSelectedCrop(3);
                    break;
                case 4: 
                    Chilehoz.SetActive(true);
                    Maízhoz.SetActive(false);
                    Trigohoz.SetActive(false);
                    Tomatehoz.SetActive(false);
                    Aguacatehoz.SetActive(false);
                    Caféhoz.SetActive(false);
                    mapManager.SetSelectedCrop(6);
                    break;
                case 5:
                    Aguacatehoz.SetActive(true);
                    Maízhoz.SetActive(false);
                    Trigohoz.SetActive(false);
                    Tomatehoz.SetActive(false);
                    Chilehoz.SetActive(false);
                    Caféhoz.SetActive(false);
                    mapManager.SetSelectedCrop(4);
                    break;
                case 6:
                    Caféhoz.SetActive(true);
                    Maízhoz.SetActive(false);
                    Trigohoz.SetActive(false);
                    Tomatehoz.SetActive(false);
                    Chilehoz.SetActive(false);
                    Aguacatehoz.SetActive(false);
                    mapManager.SetSelectedCrop(5);
                    break;
            }
        }
        else{
            herramienta.SetActive(false);
            Maízhoz.SetActive(false);
            Trigohoz.SetActive(false);
            Tomatehoz.SetActive(false);
            Chilehoz.SetActive(false);
            Aguacatehoz.SetActive(false);
            Caféhoz.SetActive(false);
        }
    }
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
