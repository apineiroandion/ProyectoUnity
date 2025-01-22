using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera3 : MonoBehaviour
{   
    public GameObject player;
    private Vector3 offset;
    public float rotationSpeed = 10f;  // Velocidad de rotación

    // Start is called before the first frame update
    void Start()
    {
        // Calcular la distancia entre la cámara y el jugador
        offset = transform.position - player.transform.position;
    }

    // LateUpdate se llama después de que se hayan procesado todos los objetos en la escena
    void LateUpdate()
    {   
        // Rotar la cámara alrededor del jugador
        transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);

        // Asegurarse de que la cámara siempre mire al jugador
        transform.LookAt(player.transform);
    }
}

