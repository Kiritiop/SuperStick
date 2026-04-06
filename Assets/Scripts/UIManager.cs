using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // Add this line

    public TMP_Text messageText;

    void Awake() // Add this
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowMessage(string message, float duration)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        Invoke(nameof(HideMessage), duration);
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}