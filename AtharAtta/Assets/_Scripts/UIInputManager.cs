using RTLTMPro;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    public RTLTMP_InputField nameInput;
    public RTLTMP_InputField messageInput;

    public StickyCardsController stickyController;
    public PanelAnimation panelJuice;

    public void SubmitCard()
    {
        string n = nameInput.text.Trim();
        string m = messageInput.text.Trim();

        if (string.IsNullOrEmpty(n) || string.IsNullOrEmpty(m))
            return;

        panelJuice.PlaySubmitFeedback();
        stickyController.AddNewCard(n, m);

        nameInput.text = "";
        messageInput.text = "";
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void ClearAll()
    {
        stickyController.ClearAll();
    }
}
