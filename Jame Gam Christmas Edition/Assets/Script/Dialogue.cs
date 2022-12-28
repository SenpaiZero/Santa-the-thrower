using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public string[] dialogue;
    float textSpeed =  0.09f;
    public AudioSource typeSfx;

    private int index;
    void Start()
    {
        StartCoroutine(delayDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(txt.text == dialogue[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                txt.text = dialogue[index];
            }
        }
    }

    void doneDialoge()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setWalk(true);
        GameObject.FindGameObjectWithTag("cam").GetComponent<FollowCamera>().setMove(true);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ui>().startMusic();
    }

    IEnumerator delayDialogue()
    {
        yield return new WaitForSeconds(0.3f);
        txt.text = string.Empty;
        startDialogue();
    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in dialogue[index].ToCharArray())
        {
            typeSfx.Play();
            txt.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void nextLine()
    {
        if(index < dialogue.Length - 1)
        {
            index++;
            txt.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            doneDialoge();
            gameObject.SetActive(false);
        }
    }
}
