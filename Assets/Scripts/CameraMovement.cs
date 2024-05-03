using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] 
    private Camera cam;

    [SerializeField]
    private SpriteRenderer mapRenderer;

    public float speed = 5.0f;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    private Vector3 dragOrigin;

    private void Awake(){
        //Limites del mapa con un sprite que se queda en el fondo del código.
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x/2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x/2f;
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y/2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y/2f;

    }
    private void Update(){
        //llamada a la función PanCamera
        PanCamera();

    //Detección de si el usuario presiona alguna de las flechas o las teclas WASD para mover la cámara.
    if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
	{
		//movimiento de la cámara
        transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
        //llamada a la función ClampCamera
        cam.transform.position = ClampCamera(cam.transform.position);
	}
	if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
	{
		transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
        cam.transform.position = ClampCamera(cam.transform.position);
	}
	if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
	{
		transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
        cam.transform.position = ClampCamera(cam.transform.position);
	}
	if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
	{
		transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
        cam.transform.position = ClampCamera(cam.transform.position);
	}
    }

    //Función que permite mover la cámara haciendo click y arrastrando el mouse.
    private void PanCamera(){
        if (Input.GetMouseButtonDown(0)){
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0)){
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    
    //Funcion que checa la posición de los límites de la cámara respecto a los límites del mapa y aplica la función Clamp para evitar que se siga moviendo la cámara en caso de llegar a los límites.
    private Vector3 ClampCamera(Vector3 targetPosition){
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

}
