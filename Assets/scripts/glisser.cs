using UnityEngine;
using UnityEngine.EventSystems;

public class Glisser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
  public void AuDebutGlisser(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        Vector3 nouvellePosition = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        nouvellePosition.z = 0;
        transform.position = nouvellePosition;
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
       
    }
}
