using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button boutonPoisson;
    [SerializeField] private string niveau1 = "Niveau1"; 

    void Start()
    {
        boutonPoisson.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadScene(niveau1);
        });
    }
}