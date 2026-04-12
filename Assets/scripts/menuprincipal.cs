using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private string niveau1 = "Niveau1"; 

    void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadScene(niveau1);
        });
    }
}