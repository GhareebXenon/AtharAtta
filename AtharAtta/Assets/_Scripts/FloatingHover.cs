using UnityEngine;
using DG.Tweening;

public class FloatingHover : MonoBehaviour
{
    [SerializeField] float amplitude = 8f;
    [SerializeField] float speed = 1.5f;

    Vector3 startPos;

    void Awake()
    {
        startPos = transform.localPosition;
    }

    void OnEnable()
    {
        transform.DOKill();

        transform.DOLocalMoveY(startPos.y + amplitude, speed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void OnDisable()
    {
        transform.DOKill();
        transform.localPosition = startPos;
    }
}
