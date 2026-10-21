using UnityEngine;

public class ControlJugadorInputSystem : MonoBehaviour
{
    public float velocidad;
    private Rigidbody2D fisica;
    private float entradaX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>(); //Obtengo el componente de Rigibody para modificar posteriormente su linearVelocity
    }

    // Update is called once per frame
    // void update()
    //{
    //  vector2 position = transform.position;

    //position.x = position.x + 0.1f;

    //transform.position = position;
    //}
    private void FixedUpdate()
    {
        entradaX = Input.GetAxis("Horizontal"); //devuelve 1 si es drch y -1 si es izq

        if (fisica != null)
        {
            fisica.linearVelocity = new Vector2(entradaX * velocidad, fisica.linearVelocity.y);
        }
        else
        {
            Debug.LogError("El GameObject ");
        }
        fisica.linearVelocity = new Vector2(entradaX * velocidad, fisica.linearVelocity.y); //Aqui se le dice cual es su nueva posicion
    }


}
