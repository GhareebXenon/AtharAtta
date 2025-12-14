//using UnityEngine;

//public class ArabicWordSpawner : MonoBehaviour
//{
//    public StickyCardsController stickyController;

//    void Start()
//    {
//        string word = "أثر العطاء";

//        // Generate layout
//        stickyController.GenerateArabicWordLayout(word);

//        // Add letters
//        foreach (char c in word)
//        {
//            if (c == ' ') continue; // skip spaces
//            stickyController.AddNewCard(c.ToString());
//        }
//    }
//}