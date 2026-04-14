using UnityEngine;

public class GestionPoisson : MonoBehaviour
{
    //ce script permet de faire disparaitre le poisson pour 2 secondes après l'avoir attrapé, puis de le faire réapparaître pour pouvoir le réattraper à nouveau (code utilisé dans le script de l'hameçon)
    public void DesactiverPoisson(){
        this.gameObject.SetActive(false);
        Invoke("ReafficherPoisson",2);
    }
 public void  ReafficherPoisson(){
    this.gameObject.SetActive(true);
     
 }   

}
