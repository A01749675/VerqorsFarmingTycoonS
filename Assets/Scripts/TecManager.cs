using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Controla las mejoras que se compran y actualiza el MapManager para que se activen las ventajas de las mejoras.
Autores:  Santiago Chevez Trejo, Carlos Iker Fuentes Reyes, 
          Alma Teresa Carpio Revilla, Mariana Marzyani Hernandez Jurado, 
          y Alan Rodrigo Vega Reza */
public class TecManager : MonoBehaviour
{
    public MapManager mapManager;
    //Declarar un vector de GameObjects para los medidores
    public GameObject[] medidor1;
    public GameObject medidores1;
    public GameObject medidores2;
    public GameObject medidores3;
    public Sprite  medidor1_5;
    public Sprite  medidor2_5;
    public Sprite  medidor3_5;
    public Sprite  medidor4_5;
    public Sprite  medidor5_5;
    //Referencia al TreeManager
    public TreeManager treeManager;
    //Referencia a los objetos que se van a modificar
    public GameObject empleados1;
    public GameObject empleados2;
    public GameObject empleados3;
    public GameObject tractor1;
    public GameObject tractor2;
    public GameObject tractor3;
    public GameObject tanque;
    public GameObject aspersores1;
    public GameObject aspersores2;
    public GameObject prodtrigo;
    public GameObject prodmaiz;
    public GameObject prodtomate;
    public GameObject prodchile;
     public GameObject prodaguacate;
    public GameObject prodfrijol;
    public GameObject basura1;
    public GameObject basura2;
    public GameObject basura3;
   

    public FinanceManager financeManager;
    //Actualiza el estado de los medidores dependiendo del nivel del agua
    private void ChangeSprite(int parcela){
        int water = mapManager.GetAverageWaterAtLand(parcela);
        if(water < 20){
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor1_5;
        }else if(water < 40){
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor2_5;
        }else if(water < 60){
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor3_5;
        }else if(water < 80){
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor4_5;
        }else{
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor5_5;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        //Si se compro algo del arbol y se actualizo cambia el estado de los objetos
        if(treeManager.update){
            for(int i = 0; i < 20; i++){
                medidor1[i].SetActive(treeManager.mejoras[1]);    
                ChangeSprite(i);
            }
            medidores1.SetActive(treeManager.mejoras[2]);
            medidores2.SetActive(treeManager.mejoras[13]);
            medidores3.SetActive(treeManager.mejoras[20]);
            basura1.SetActive(!treeManager.mejoras[2]);
            basura2.SetActive(!treeManager.mejoras[13]);
            basura3.SetActive(!treeManager.mejoras[20]);
            empleados1.SetActive(treeManager.mejoras[3]);
            empleados2.SetActive(treeManager.mejoras[9]);
            empleados3.SetActive(treeManager.mejoras[16]);
            tractor1.SetActive(treeManager.mejoras[4]);
            tractor2.SetActive(treeManager.mejoras[10]);
            tractor3.SetActive(treeManager.mejoras[17]);
            tanque.SetActive(treeManager.mejoras[5]);
            aspersores1.SetActive(treeManager.mejoras[7]);
            aspersores2.SetActive(treeManager.mejoras[14]);
            if(treeManager.mejoras[6] && !(treeManager.mejoras[12] || treeManager.mejoras[19])){
                financeManager.UpdateSeguro(6);
            }
            else if(treeManager.mejoras[12] && treeManager.mejoras[6] && !treeManager.mejoras[19]){
                financeManager.UpdateSeguro(12);
            }
            else if(treeManager.mejoras[19] && treeManager.mejoras[6] && treeManager.mejoras[12]){
                financeManager.UpdateSeguro(19);
            }
            if(treeManager.mejoras[8] && treeManager.mejoras[15] && treeManager.mejoras[21]){
                financeManager.dinero = 1.2f;
            }
            if(treeManager.mejoras[2]){
                mapManager.UpdateUnlockedLands(new int[]{11,12,16,17});
            }
            if(treeManager.mejoras[13]){
                mapManager.UpdateUnlockedLands(new int[]{0, 2, 4, 6, 9,13,18});
            }
            if(treeManager.mejoras[20]){
                mapManager.UpdateUnlockedLands(new int[]{1,3,5,7,8,10,14,15,19});
            }
            treeManager.update = false;
            if(treeManager.mejoras[5] && !treeManager.mejoras[11] && !treeManager.mejoras[18]){
                tanque.GetComponent<TankManager>().SetTankLevel(1);
            }
            else if(treeManager.mejoras[11] && !treeManager.mejoras[18]){
                tanque.GetComponent<TankManager>().SetTankLevel(2);
            }
            else if(treeManager.mejoras[18]){
                tanque.GetComponent<TankManager>().SetTankLevel(3);
            }
            
        }
        //Se llama a la función de cambiar el sprite de los medidores
        for(int i = 0; i < 20; i++){
                ChangeSprite(i);
            }
        //Se checa la producción de los cultivos	
        if(!treeManager.mejoras[2] & !treeManager.mejoras[13] &!treeManager.mejoras[20]){
                prodtrigo.GetComponent<TextMeshProUGUI>().text = "1 parcela";
                prodmaiz.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodchile.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodtomate.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodfrijol.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodaguacate.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
            } else if (treeManager.mejoras[2] & !treeManager.mejoras[13] & !treeManager.mejoras[20]){
                prodtrigo.GetComponent<TextMeshProUGUI>().text = "1 parcela";
                prodmaiz.GetComponent<TextMeshProUGUI>().text = "1 parcela";
                prodchile.GetComponent<TextMeshProUGUI>().text = "1 parcela";
                prodtomate.GetComponent<TextMeshProUGUI>().text = "1 parcelas";
                prodfrijol.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodaguacate.GetComponent<TextMeshProUGUI>().text = "1 parcelas";
            } else if (treeManager.mejoras[2] & treeManager.mejoras[13] & !treeManager.mejoras[20]){
                prodtrigo.GetComponent<TextMeshProUGUI>().text = "2 parcelas";
                prodmaiz.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
                prodchile.GetComponent<TextMeshProUGUI>().text = "2 parcelas";
                prodtomate.GetComponent<TextMeshProUGUI>().text = "2 parcelas";
                prodfrijol.GetComponent<TextMeshProUGUI>().text = "1 parcelas";
                prodaguacate.GetComponent<TextMeshProUGUI>().text = "2 parcelas";
            } else{
                prodtrigo.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
                prodmaiz.GetComponent<TextMeshProUGUI>().text = "4 parcelas";
                prodchile.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
                prodtomate.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
                prodfrijol.GetComponent<TextMeshProUGUI>().text = "4 parcelas";
                prodaguacate.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
            }
       
    }
}
