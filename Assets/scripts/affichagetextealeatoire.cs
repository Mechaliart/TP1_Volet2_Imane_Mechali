using UnityEngine;
using TMPro;
using System.Collections;


public class affichagetextealeatoire : MonoBehaviour
{

    //=====variables globales

    //UI
    public TMP_Text texteCouleur; //mettre le texte dans l'inspecteur pour pouvoir le modifier

    //affichage du texte dans une liste qu'on peut changer dans l'inspecteur
    public string[] textes;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //generer nombre aleatoire entre 0 et la longueur du tableau de textes (assuigner le texte aleatoire a la variable texteCouleur)
        int textealeatoire = Random.Range(0, textes.Length); 
        texteCouleur.text = textes[textealeatoire]; //afficher le texte aleatoire  
     
     if(textes.Length == 0){ //la length 0 (le premier element du tableau) correspond a la couleur rouge
           
            Debug.Log("Le texte est rouge");
        }
        else if(textes.Length == 1){
           
            Debug.Log("Le texte est vert");
        }
        else if(textes.Length == 2){
           
            Debug.Log("Le texte est bleu");
        }else if(textes.Length == 3){
          
            Debug.Log("Le texte est jaune");
        }
    }

    // Update is called once per frame
    void Update()
    {
      
     
        
    }

    void Redmarrer(){
        //après avoir choisi le bon poisson correspondant à la couleur du texte, on redemarre la couleur du texte
    }
}
