using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] public TMP_Text dialogueUI;
    [SerializeField] public TMP_Text Hint;
    private WritingEffect Typewriter;


    private void Start()
    {
        Typewriter = GetComponent<WritingEffect>();
    }

    public IEnumerator RunDialogue(DialogueObject dialogueObject)
    {
        while (PausePlaySwitcher.GameIsPaused)
        {
            yield return 0;
        }
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogueText = dialogueObject.Dialogue[i];
            yield return Typewriter.Run(dialogueText, dialogueUI);

            ActivateHint();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            DeactivteHint();

            yield return 0;
        }
    }

    private void ActivateHint()
    {
        Hint.enabled = true;
    }

    private void DeactivteHint()
    {
        Hint.enabled = false;
    }

}
