using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueAnimator : MonoBehaviour
{
    public Animator startDialog;
    public GameObject start;
    public DialogueManager dm;

    public void OnTriggerEnter(Collider2D other)
    {
        startDialog.SetBool("StartDialog", true);
        start.SetActive(true);
    }

    public void OnTriggerExit(Collider2D other)
    {
        startDialog.SetBool("StartDialog", false);
        start.SetActive(false);
        dm.EndDialogue();
    }
}
