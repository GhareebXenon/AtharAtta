using RTLTMPro;
using TMPro;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    public RTLTMP_InputField nameInput;
    public RTLTMP_InputField messageInput;

    public CarousalController carousel;

    public void SubmitCard()
    {
        string n = nameInput.text.Trim();
        string m = messageInput.text.Trim();

        if (string.IsNullOrEmpty(n) || string.IsNullOrEmpty(m))
            return;

        carousel.AddNewCard(n, m);

        nameInput.text = "";
        messageInput.text = "";
    }
}
