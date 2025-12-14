using DG.Tweening;
using RTLTMPro;
using TMPro;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public RTLTextMeshPro nameText;
    public RTLTextMeshPro messageText;
    public GameObject deleteButton;   
    private CarousalController carousel;

    void Awake()
    {
        deleteButton.SetActive(false);
    }
    public void Setup(string personName, string message)
    {
        nameText.text = personName;
        messageText.text = message;
    }
    public void Init(CarousalController owner)
    {
        carousel = owner;
    }

    public void SetEditMode(bool enabled)
    {
        deleteButton.SetActive(enabled);
    }

    public void DeleteSelf()
    {
        
        transform.DOScale(Vector3.zero, 0.25f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                carousel.RemoveCard(gameObject);
            });
    }
}
