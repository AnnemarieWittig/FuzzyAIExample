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
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogueText = dialogueObject.Dialogue[i];
            Debug.Log(dialogueText);
            yield return typewriter.Run(dialogueText, dialogueUI);
            //if (i != (dialogueObject.Dialogue.Length - 1))
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return 0;
        }
        //closeDialogueBox();
    }

    private void closeDialogueBox()
    {
        dialogueUI.enabled = false;
        dialogueUI.text = string.Empty;
    }

    public bool getCoroutineRunning() => coroutineRunning;
    public void setCoroutineRunning(bool state) { coroutineRunning = state; }

}
