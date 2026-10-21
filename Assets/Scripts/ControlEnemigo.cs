using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(Rigidbody2D))]

public class ControlEnemigo : MonoBehaviour
{
    [Header ("Posicion")]
    public float velocidad = 2f;
    public Vector2 posicionFinal;
    private Vector2 posicionInicial;
    private bool movimientoHaciaArriba = true;

    [SerializeField] private float toleracia = 1;


    private Rigidbody2D fisica;
    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicionInicial = fisica.position;
        
    }

    private void FixedUpdate()
    {
        Vector2 posicionOnObjetivo;
        if (velocidad <= 0)
        {
            return;
        }

        if(movimientoHaciaArriba){
            posicionOnObjetivo = posicionFinal;
        }
        else
        {
            posicionOnObjetivo = posicionInicial;
        }

        if (Vector2.Distance(fisica.position, posicionOnObjetivo) <= toleracia)
        {
            movimientoHaciaArriba = !movimientoHaciaArriba;

            posicionOnObjetivo = movimientoHaciaArriba ? posicionFinal : posicionInicial;

            posicionOnObjetivo = movimientoHaciaArriba ? posicionFinal : posicionInicial;
            Debug.Log("Posicion Objetivo" + posicionOnObjetivo);
        }

        fisica.MovePosition(Vector2.MoveTowards(fisica.position, posicionOnObjetivo, velocidad * Time.fixedDeltaTime));

    }
}
