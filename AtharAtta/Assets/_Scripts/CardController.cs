using UnityEngine;
using TMPro;
using RTLTMPro;

public class CardController : MonoBehaviour
{
    public RTLTextMeshPro nameText;
    public RTLTextMeshPro messageText;

    public void Setup(string personName, string message)
    {
        nameText.text = personName;
        messageText.text = message;
    }
}
