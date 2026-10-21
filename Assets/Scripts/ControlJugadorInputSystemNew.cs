using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugadorInputSystemNew : MonoBehaviour
{
    public InputAction moveAction;
    public float velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable(); //Hay q poner esto para q el personaje se mueva

        QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = 30; //lo ejecuta a 10 fps
    }

    // Update is called once per frame
    void Update() // depende del equipo q tenga el usr, pq lo mira cada segundo
    {
        Vector2 move = moveAction.ReadValue<Vector2>(); //lee el valor

        Vector2 position = (Vector2)transform.position + move * velocity*Time.deltaTime; //(vector2) es un casting pq position no tiene z, deltaTime esta diciendo mueveme el personaje por segundo

        transform.position = position; 
    }
}
