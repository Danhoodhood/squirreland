using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    public Animator startDialog;
    public DialogueManager dm;

    public void OnTriggerEnter(Collider2D other)
    {
        startDialog.SetBool("StartDialog", true);
    }

    public void OnTriggerExit(Collider2D other)
    {
        startDialog.SetBool("StartDialog", false);
        dm.EndDialogue();
    }
}
