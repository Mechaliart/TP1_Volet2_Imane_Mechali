using UnityEngine;

public class Bait : MonoBehaviour
{
    //variables pour la physuique de l'hamecon
    [SerializeField] private float gravity = -1.5f;
    [SerializeField] private fildepeche rope;
    [SerializeField] private float springStrength = 50f;  // la vitesse à laquelle l'hameçon revient à sa position initiale
    [SerializeField] private float damping = 0.85f;       // le facteur de réduction de la vitesse pour simuler l'amortissement du mouvement
public affichagetextealeatoire scriptTexte;
    private Vector2 velocity;
    private Vector2 initialPosition;
    private bool isDragging = false;

    Rigidbody2D rigidbait;

    void Start()
    { 
        rigidbait = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // save starting position
    }

    void Update()
    { //code pour faire revenir l'hamecon à sa position initiale après l'avoir lâché (simulation du ressort)
        if (isDragging)
        {
            velocity = Vector2.zero;
            return;
        }

        
        Vector2 directionHome = initialPosition - (Vector2)transform.position;
        velocity += directionHome * springStrength * Time.deltaTime;
        velocity *= damping; 

        transform.position += (Vector3)(velocity * Time.deltaTime);

        
        if (rope != null)
        {
            Vector2 ropeEnd = rope.GetSecondToLastSegmentPos();
            float distance = Vector2.Distance(ropeEnd, (Vector2)transform.position);

            if (distance > rope.ropeSegLen)
            {
                velocity.y = 0f;
                transform.position = (Vector3)ropeEnd;
            }
        }
    }

   
    public void StartDrag() { 
        isDragging = true; 
    rigidbait.bodyType = RigidbodyType2D.Static;

    }
    public void StopDrag() { 
        isDragging = false; 
      rigidbait.bodyType = RigidbodyType2D.Dynamic;
    }


      //si l'hamecon touche un poisson avec chaque couleur
        //si le poisson correspond a la couleur du texte, on redemarre la couleur du texte

public   void OnTriggerEnter2D(Collider2D collision) {
   
    

   if (collision.gameObject.tag ==  scriptTexte.couleurChoisie) {
      //code pour le poisson rouge
       Debug.Log("bon poisson");
       //rétroaction positive : redemarrer la couleur du texte
       collision.gameObject.GetComponent<GestionPoisson>().DesactiverPoisson();
       scriptTexte.Redmarrer();
       
   }else{
    Debug.Log("mauvais poisson");
    //POUR VOLET ******3 : jouer le son (après une certaine durée on renomme la couleur affichée (rappeler à maxime que au-dessus, on mets un if pour une correspondance entre la couleur du texte et le tag du poisson, mais il y aura une correspondance de son (pour les instructions de la rétroaction) pour CHAQUE COULEUR))
    
   }
}  

}