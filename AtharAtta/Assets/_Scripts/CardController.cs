using DG.Tweening;
using RTLTMPro;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public RTLTextMeshPro nameText;
    public RTLTextMeshPro messageText;
    public GameObject deleteButton;

    private StickyCardsController stickyController;

    void Awake()
    {
        if (deleteButton != null)
            deleteButton.SetActive(false);
    }

    public void Setup(string name, string message)
    {
        if (nameText != null)
            nameText.text = name;

        if (messageText != null)
            messageText.text = message;
    }

    public void Init(StickyCardsController owner)
    {
        stickyController = owner;
    }

    public void SetEditMode(bool enabled)
    {
        if (deleteButton != null)
            deleteButton.SetActive(enabled);
    }

    public void DeleteSelf()
    {
        transform.DOScale(Vector3.zero, 0.25f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                if (stickyController != null)
                    stickyController.RemoveCard(gameObject);
            });
    }
}
