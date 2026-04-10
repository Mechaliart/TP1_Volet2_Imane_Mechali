using UnityEngine;
using UnityEngine.InputSystem;

public class jeu : MonoBehaviour
{
    //Variables globales
    [Header("Variables de jeu")]
    
    [Header("Actions objet")]

    public InputAction actiondeplacerhamecon;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Rigidbody rigidbody = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    //Making other functions
}
