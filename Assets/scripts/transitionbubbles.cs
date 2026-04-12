using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 2f;
    [SerializeField] private AudioClip bubbleSound;
    [SerializeField] private int bubbleCount = 20;

    [Header("Bubble Settings")]
    [SerializeField] private Sprite bubbleSprite;   // assign a circle sprite
    [SerializeField] private float bubbleMinSize = 0.2f;
    [SerializeField] private float bubbleMaxSize = 0.8f;
    [SerializeField] private float bubbleRiseSpeed = 3f;

    private AudioSource audioSource;
    private List<GameObject> activeBubbles = new List<GameObject>();
    private Canvas transitionCanvas;

    void Awake()
    {
        // Singleton so it persists between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        CreateCanvas();
    }

    private void CreateCanvas()
    {
        // Create a canvas that sits on top of everything
        GameObject canvasObj = new GameObject("TransitionCanvas");
        canvasObj.transform.SetParent(transform);
        transitionCanvas = canvasObj.AddComponent<Canvas>();
        transitionCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        transitionCanvas.sortingOrder = 999;
        canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
        canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        transitionCanvas.gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionRoutine(sceneName));
    }

    private IEnumerator TransitionRoutine(string sceneName)
    {
        transitionCanvas.gameObject.SetActive(true);

        // Play bubble sound
        if (bubbleSound != null)
            audioSource.PlayOneShot(bubbleSound);

        // Spawn bubbles
        SpawnBubbles();

        yield return new WaitForSeconds(transitionDuration);

        // Clean up bubbles
        foreach (GameObject bubble in activeBubbles)
            if (bubble != null) Destroy(bubble);
        activeBubbles.Clear();

        transitionCanvas.gameObject.SetActive(false);

        SceneManager.LoadScene(sceneName);
    }

    private void SpawnBubbles()
    {
        for (int i = 0; i < bubbleCount; i++)
        {
            StartCoroutine(SpawnBubbleWithDelay(i * (transitionDuration / bubbleCount)));
        }
    }

    private IEnumerator SpawnBubbleWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject bubble = new GameObject("Bubble");
        bubble.transform.SetParent(transitionCanvas.transform, false);
        activeBubbles.Add(bubble);

        // Visual
        UnityEngine.UI.Image img = bubble.AddComponent<UnityEngine.UI.Image>();
        if (bubbleSprite != null)
            img.sprite = bubbleSprite;

        // Random size
        float size = Random.Range(bubbleMinSize, bubbleMaxSize) * 100f;
        RectTransform rect = bubble.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(size, size);

        // Random horizontal start position
        float startX = Random.Range(-Screen.width / 2f, Screen.width / 2f);
        rect.anchoredPosition = new Vector2(startX, -Screen.height / 2f);

        // Animate bubble rising
        StartCoroutine(AnimateBubble(rect));
    }

    private IEnumerator AnimateBubble(RectTransform rect)
    {
        float targetY = Screen.height / 2f + 100f;
        Color startColor = new Color(0.5f, 0.8f, 1f, 0.7f); // light blue
        Color endColor = new Color(0.5f, 0.8f, 1f, 0f);      // fade out at top

        UnityEngine.UI.Image img = rect.GetComponent<UnityEngine.UI.Image>();

        while (rect != null && rect.anchoredPosition.y < targetY)
        {
            // Rise upward with slight horizontal wobble
            float wobble = Mathf.Sin(Time.time * 2f + rect.anchoredPosition.x) * 20f;
            rect.anchoredPosition += new Vector2(wobble * Time.deltaTime, bubbleRiseSpeed);

            // Fade out as bubble reaches top
            float progress = Mathf.InverseLerp(-Screen.height / 2f, targetY, rect.anchoredPosition.y);
            if (img != null)
                img.color = Color.Lerp(startColor, endColor, progress);

            yield return null;
        }
    }
}