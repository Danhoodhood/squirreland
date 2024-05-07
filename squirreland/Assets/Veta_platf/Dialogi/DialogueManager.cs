using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text dialogueText;
    public Text nameText;

    public Animator startDialog;

    private Queue<string> sentences;

    public void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        startDialog.SetBool("StartDialog", false);
        sentences.Clear();

       //     foreach(string sentence in dialogue.sentences)
      //      {
             //   sentences.Enqueue(sentence);
       //     }

          //  DisplayNextSentences();
    }

    public void DisplayNextSentences()
    {
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter  in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {

    }

}
