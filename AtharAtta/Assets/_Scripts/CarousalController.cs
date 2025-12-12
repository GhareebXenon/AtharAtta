using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarousalController : MonoBehaviour
{
    public RectTransform cardParent;   // UI Container (Horizontal group or manual layout)
    public GameObject cardPrefab;      // Prefab with UI elements

    public float slideSpeed = 5f;
    public float idleTimeBeforeLoop = 30f;
    public float cardWidth = 1920f;    // Width of each card in 2D UI

    private List<GameObject> cards = new List<GameObject>();
    private int currentIndex = 0;
    private float idleTimer = 0f;
    private bool isLooping = false;

    void Update()
    {
        idleTimer += Time.deltaTime;

        if (!isLooping && idleTimer >= idleTimeBeforeLoop)
        {
            StartCoroutine(LoopCarousel());
        }
    }

    public void AddNewCard(string name, string message)
    {
        GameObject newCard = Instantiate(cardPrefab, cardParent);

        RectTransform rt = newCard.GetComponent<RectTransform>();

        // Position card horizontally
        float xPos = cards.Count * cardWidth;
        rt.anchoredPosition = new Vector2(xPos, -300);   // Start BELOW the screen

        newCard.GetComponent<CardController>().Setup(name, message);

        cards.Add(newCard);

        // Expand the parent width
        cardParent.sizeDelta = new Vector2(cards.Count * cardWidth, cardParent.sizeDelta.y);

        // ---- DOTween Animation ----

        // 1) Start invisible
        CanvasGroup cg = newCard.AddComponent<CanvasGroup>();
        cg.alpha = 0;

        // 2) Fade-in
        cg.DOFade(1f, 0.4f);

        // 3) Bounce Up Animation
        rt.DOAnchorPosY(0, 0.6f)
            .SetEase(Ease.OutBack)      // bounce effect
            .SetUpdate(true);           // smoother animation


        // ---- Update carousel ----
        StopAllCoroutines();
        isLooping = false;
        idleTimer = 0f;

        currentIndex = cards.Count - 1;
        MoveToCardInstant(currentIndex);
    }


    void MoveToCardInstant(int index)
    {
        Vector2 pos = cardParent.anchoredPosition;
        pos.x = -index * cardWidth;
        cardParent.anchoredPosition = pos;
    }

    IEnumerator LoopCarousel()
    {
        if (cards.Count == 0) yield break;

        isLooping = true;

        while (true)
        {
            currentIndex = (currentIndex + 1) % cards.Count;
            yield return StartCoroutine(SlideToIndex(currentIndex));
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator SlideToIndex(int index)
    {
        float targetX = -index * cardWidth;

        Vector2 startPos = cardParent.anchoredPosition;
        Vector2 targetPos = new Vector2(targetX, startPos.y);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * slideSpeed;
            cardParent.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }
    }
}
