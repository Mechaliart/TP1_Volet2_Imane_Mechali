using UnityEngine;

public class Bait : MonoBehaviour
{
    //variables pour la physuique de l'hamecon
    [SerializeField] private float gravity = -1.5f;
    [SerializeField] private fildepeche rope;
    [SerializeField] private float springStrength = 50f;  // how fast it snaps back
    [SerializeField] private float damping = 0.85f;       // reduces bouncing

    private Vector2 velocity;
    private Vector2 initialPosition;
    private bool isDragging = false;

    void Start()
    {
        initialPosition = transform.position; // save starting position
    }

    void Update()
    {
        if (isDragging)
        {
            velocity = Vector2.zero; // no physics while dragging
            return;
        }

        // Spring back to initial position
        Vector2 directionHome = initialPosition - (Vector2)transform.position;
        velocity += directionHome * springStrength * Time.deltaTime;
        velocity *= damping; // dampen to avoid infinite bouncing

        transform.position += (Vector3)(velocity * Time.deltaTime);

        // Stop falling if rope is fully stretched
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

    // Call these from your Glisser.cs
    public void StartDrag() { isDragging = true; }
    public void StopDrag() { isDragging = false; }


      //si l'hamecon touche un poisson avec chaque couleur
        //si le poisson correspond a la couleur du texte, on redemarre la couleur du texte

    public   void OnTriggerEnter2D(Collider2D collision) {
   
   if (collision.gameObject.tag == "rouge") {
      //code pour le poisson rouge
       Debug.Log("rouge");
       //rétroaction positive : redemarrer la couleur du texte
   
   }else if (collision.gameObject.tag == "bleu") {
        //code pour le poisson bleu
         Debug.Log("bleu !");
   
   }else if (collision.gameObject.tag == "jaune" ) {
       // code pour le poisson orange
       Debug.Log("jaune");
   
   }else if (collision.gameObject.tag == "vert" ) {
       // code pour le poisson vert
       Debug.Log("vert");
   }
}  
    

}