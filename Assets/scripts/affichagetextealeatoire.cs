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
        // Debug.Log("Le texte aleatoire est : " + texteCouleur.text); //afficher le texte aleatoire dans la console pour verifier que ca marche 
     
     if(texteCouleur.text == "Rouge"){
        Debug.Log("Le texte est rouge");
        texteCouleur.color = Color.red; //changer la couleur du texte en rouge
     }
     else if(texteCouleur.text == "Bleu"){
        Debug.Log("Le texte est bleu");
        texteCouleur.color = Color.blue; //changer la couleur du texte en bleu
     }
     else if(texteCouleur.text == "Vert"){
        Debug.Log("Le texte est vert");
        texteCouleur.color = Color.green; //changer la couleur du texte en vert
     }
     else if(texteCouleur.text == "Jaune"){
        Debug.Log("Le texte est jaune");
        texteCouleur.color = Color.yellow; //changer la couleur du texte en jaune
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
