using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarousalController : MonoBehaviour
{
    [Header("References")]
    public RectTransform cardParent;
    public GameObject cardPrefab;
    public GameObject exitButton;

    [Header("Carousel Settings")]
    public float cardWidth = 600f;
    public float idleTimeBeforeLoop = 30f;

    [Header("Animation")]
    public float repositionDuration = 0.4f;
    public float centerScale = 1.1f;

    private List<GameObject> cards = new List<GameObject>();
    private int currentIndex = 0;
    private float idleTimer = 0f;
    private bool isLooping = false;
    private bool editMode = false;

    [Header("Center Offset")]
    public float centerXOffset = 0f;

    void Update()
    {
        idleTimer += Time.deltaTime;

        if (!isLooping && idleTimer >= idleTimeBeforeLoop && cards.Count > 1)
        {
            StartCoroutine(AutoLoop());
        }
    }


    public void AddNewCard(string name, string message)
    {
        GameObject newCard = Instantiate(cardPrefab, cardParent);
        RectTransform rt = newCard.GetComponent<RectTransform>();

        //newCard.GetComponent<CardController>().Init(this);
        //newCard.GetComponent<CardController>().Setup(name, message);

        // Start below screen
        rt.anchoredPosition = new Vector2(0, -300);
        rt.localScale = Vector3.one; 

        cards.Add(newCard);

       
        currentIndex = cards.Count - 1;

        StopAllCoroutines();
        isLooping = false;
        idleTimer = 0f;

        // Fade + bounce in
        CanvasGroup cg = newCard.AddComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.DOFade(1f, 0.4f);
        rt.DOAnchorPosY(0, 0.6f).SetEase(Ease.OutBack);

        UpdateCarouselVisuals(true);
    }


    public void RemoveCard(GameObject card)
    {
        int index = cards.IndexOf(card);
        if (index < 0) return;

        cards.Remove(card);
        Destroy(card);

        if (cards.Count == 0) return;

        currentIndex = WrapIndex(currentIndex);
        UpdateCarouselVisuals();
    }

    
    public void ToggleEditMode()
    {
        editMode = !editMode;

        foreach (GameObject card in cards)
        {
            card.GetComponent<CardController>().SetEditMode(editMode);
        }

        exitButton.SetActive(editMode);
    }

    IEnumerator AutoLoop()
    {
        isLooping = true;

        while (true)
        {
            yield return new WaitForSeconds(3f);
            currentIndex = WrapIndex(currentIndex + 1);
            UpdateCarouselVisuals();
        }
    }


    void UpdateCarouselVisuals(bool instant = false)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            RectTransform rt = cards[i].GetComponent<RectTransform>();
            int offset = i - currentIndex;

            // Wrap offsets to form a circle
            if (offset > cards.Count / 2) offset -= cards.Count;
            if (offset < -cards.Count / 2) offset += cards.Count;

            float targetX = centerXOffset + offset * cardWidth;
            float targetScale = (offset == 0) ? centerScale : 1f; // Center = 1.1, others = 1

            rt.DOKill();

            if (instant)
            {
                rt.anchoredPosition = new Vector2(targetX, 0);
                rt.localScale = Vector3.one * targetScale;
            }
            else
            {
                rt.DOAnchorPosX(targetX, repositionDuration).SetEase(Ease.OutCubic);
                rt.DOScale(targetScale, repositionDuration).SetEase(Ease.OutCubic);
            }

           
            rt.SetSiblingIndex(offset == 0 ? cards.Count : 0);
        }
    }

    int WrapIndex(int index)
    {
        if (cards.Count == 0) return 0;
        return (index % cards.Count + cards.Count) % cards.Count;
    }
}
