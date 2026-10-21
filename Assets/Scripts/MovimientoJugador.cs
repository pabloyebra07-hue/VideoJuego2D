using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent (typeof(Rigidbody2D))]
public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocity;

    [SerializeField]private InputActionReference actionMover; //private para que no lo puedan cambiar con cualquier script y serializeField para que aparezca en unityHub

    [Header("Salto")]
    [SerializeField] private InputActionReference actionSaltar;
    private Rigidbody2D fisica;
    private Vector2 entradaX;

    private bool saltoPendiente;

    public float fuerzaImpulso = 5f; //fuerza al saltar
    [SerializeField] private LayerMask capaSuelo;
    private bool estaTocandoSuelo;
    public float longitudRayCast = 1.7f;

    private void Awake() // se ejecute aunque no este activo y se ejecuta antes que Start
    {   //detengo el rigidbody del player para modificar despues su linearvelocity
        fisica = GetComponent<Rigidbody2D>(); //rigidbody esta almacenado en fisica
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        entradaX = actionMover.action.ReadValue<Vector2>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRayCast, capaSuelo); //hit tiene que colisionar con objetos de la capa suelo

        estaTocandoSuelo = hit.collider != null;

        Color colorRayo = estaTocandoSuelo ? Color.green : Color.red;

        Debug.DrawRay(transform.position, Vector2.down * longitudRayCast, colorRayo);

        if (actionSaltar.action.WasPressedThisFrame() && estaTocandoSuelo)
        {
            saltoPendiente = true;
        }
    }

    private void OnEnable() // se ejecuta cada vez que acctivo el jugador
    {
        if((actionMover==null) || actionMover.action == null)
        {
            Debug.LogError("Asigna Mapa de accion Jugador/Mover en Accion Mover");
            enabled = false;
            return;
        }
        actionMover.action.Enable();

        if ((actionSaltar == null) || actionSaltar.action == null)
        {
            Debug.LogError("Asigna Mapa de accion Jugador/Saltar en Accion Saltar");
            enabled = false;
            return;
        }
        actionSaltar.action.Enable();
    }

    private void OnDisable()
    {
        if(actionMover!= null && actionMover.action != null)
        {
            actionMover.action.Disable();
        }

        if (actionSaltar != null && actionSaltar.action != null)
        {
            actionSaltar.action.Disable();
        }
    }
    private void FixedUpdate() //mover a travez de la fisica
    {
        fisica.linearVelocity = new Vector2(entradaX.x * velocity, fisica.linearVelocity.y);

        if (saltoPendiente)
        {
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse); //Force salto continuo, impulse un impulso 
        }

        saltoPendiente = false;
    }
}
