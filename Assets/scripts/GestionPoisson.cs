using UnityEngine;

public class GestionPoisson : MonoBehaviour
{
    public void DesactiverPoisson(){
        this.gameObject.SetActive(false);
        Invoke("ReafficherPoisson",2);
    }
 public void  ReafficherPoisson(){
    this.gameObject.SetActive(true);
     
 }   

}
