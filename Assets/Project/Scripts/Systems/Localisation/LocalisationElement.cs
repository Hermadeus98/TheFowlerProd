using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationElement : MonoBehaviour
{

    private TMPro.TextMeshProUGUI displayedText;

    [TextArea()]
    [SerializeField] private string englishText, frenchText;

    private void Awake()
    {
        displayedText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        LocalisationManager.Register(this);
    }

    private void OnDisable()
    {
        LocalisationManager.UnRegister(this);
    }

    public void Refresh()
    {
        switch (LocalisationManager.language)
        {
            case Language.ENGLISH:
                if(displayedText != null)
                {
                    displayedText.text = englishText;
                }

                break;
            case Language.FRENCH:
                if (displayedText != null)
                {
                    displayedText.text = frenchText;
                }
                break;
        }
    }
}
