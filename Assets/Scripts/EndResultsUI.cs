using UnityEngine;
using UnityEngine.UI;

public class EndResultsUI : MonoBehaviour
{
    public GameObject endResultsUI;
    public TMPro.TextMeshPro ScoreText;
    public TMPro.TextMeshPro MissedText;
    public ScoreManager instance;

    public void ShowResults()
    {
        endResultsUI.SetActive(true);
        ScoreText.text = "Score: " + instance.getScore();
        MissedText.text = "Missed Notes: " + instance.getMiss();
    }
}
