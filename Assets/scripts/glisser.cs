using UnityEngine;
using UnityEngine.EventSystems;

public class Glisser : MonoBehaviour
{   
    AudioSource audioSource;
    public AudioClip GrabSound;
    public AudioClip DropSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
  public void AuDebutGlisser(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        Vector3 nouvellePosition = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        nouvellePosition.z = 0;
        transform.position = nouvellePosition;
        audioSource.PlayOneShot(GrabSound); //jouer le son lors de la prise d'objet
        
    }
    public void AuGlisser(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        Vector3 nouvellePosition = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        nouvellePosition.z = 0;
        transform.position = nouvellePosition;
    }
        
    public void AuFinGlisser(BaseEventData eventData)
    {
        audioSource.PlayOneShot(DropSound); //jouer le son lorsque l'objet est lâché
    }
}
