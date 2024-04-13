using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public TreeManager treeManager;
    public GameObject empleados1;
    public GameObject empleados2;
    public GameObject empleados3;
    public GameObject tractor1;
    public GameObject tractor2;
    public GameObject tractor3;
    public GameObject tanque;

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
        for(int i = 0; i < 20; i++){
            medidor1[i].SetActive(treeManager.Mejoras[1]);    
            ChangeSprite(i);
        }
        medidores1.SetActive(treeManager.Mejoras[2]);
        medidores2.SetActive(treeManager.Mejoras[13]);
        medidores3.SetActive(treeManager.Mejoras[20]);
        empleados1.SetActive(treeManager.Mejoras[3]);
        empleados2.SetActive(treeManager.Mejoras[9]);
        empleados3.SetActive(treeManager.Mejoras[16]);
        tractor1.SetActive(treeManager.Mejoras[4]);
        tractor2.SetActive(treeManager.Mejoras[10]);
        tractor3.SetActive(treeManager.Mejoras[17]);
        tanque.SetActive(treeManager.Mejoras[5]);
    }
}
