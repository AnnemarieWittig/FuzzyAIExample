using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] TMP_Text dialogueUI;
    [SerializeField] private DialogueObject testDialogue;
    private WritingEffect typewriter;
    private bool coroutineRunning;

    private void Start()
    {
        typewriter = GetComponent<WritingEffect>();
        //showDialogue(testDialogue);
    }

    public void showDialogue(DialogueObject dialogueObject)
    {
        dialogueUI.enabled = true;
        StartCoroutine(RunDialogue(dialogueObject));
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
            Debug.Log(dialogueText);
            yield return typewriter.Run(dialogueText, dialogueUI);
            //if (i != (dialogueObject.Dialogue.Length - 1))
            activateHint();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            deactivteHint();
            yield return 0;
        }
        //closeDialogueBox();
    }

    private void activateHint()
    {
        //TODO
    }

    private void deactivteHint()
    {
        //TODO
    }

    private void closeDialogueBox()
    {
        dialogueUI.enabled = false;
        dialogueUI.text = string.Empty;
    }

    public bool getCoroutineRunning() => coroutineRunning;
    public void setCoroutineRunning(bool state) { coroutineRunning = state; }

}
