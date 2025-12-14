using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class StickyCardsController : MonoBehaviour
{
    [Header("References")]
    public RectTransform cardParent;
    public GameObject cardPrefab;

    [Header("Animation")]
    public float moveDuration = 0.6f;
    public Ease moveEase = Ease.OutBack;
    public float initialScale = 0.1f; // small when spawning

    [Header("Positions")]
    public List<Transform> availablePositions = new List<Transform>(); // assign in inspector

    private List<GameObject> cards = new List<GameObject>();
    private List<Transform> freePositions = new List<Transform>();

    void Start()
    {
        // Copy positions to free list
        freePositions = new List<Transform>(availablePositions);
    }

    // Call this for each submitted card
    public void AddNewCard(string name, string message)
    {
        if (freePositions.Count == 0)
        {
            Debug.LogWarning("No free positions left!");
            return;
        }

        // Pick random free position
        int randIndex = Random.Range(0, freePositions.Count);
        Transform targetPos = freePositions[randIndex];
        freePositions.RemoveAt(randIndex);

        // Instantiate card
        GameObject card = Instantiate(cardPrefab, cardParent);
        RectTransform rt = card.GetComponent<RectTransform>();
        CardController controller = card.GetComponent<CardController>();
        controller.Setup(name, message);
        controller.Init(this);

        // Start small/offscreen
        rt.localScale = Vector3.one * initialScale;
        rt.anchoredPosition = new Vector2(0, -500);

        cards.Add(card);

        // Animate to target position
        rt.DOMove(targetPos.position, moveDuration).SetEase(moveEase);
        rt.DOScale(new Vector3(0.1f,0.1f,0.1f), moveDuration).SetEase(moveEase);
    }

    // Remove card
    public void RemoveCard(GameObject card)
    {
        if (cards.Contains(card))
            cards.Remove(card);

        // Return position back to free list
        freePositions.Add(card.transform);

        Destroy(card);
    }

    // Clear all cards
    public void ClearAll()
    {
        foreach (var c in cards)
            Destroy(c);

        cards.Clear();
        freePositions = new List<Transform>(availablePositions);
    }
}
