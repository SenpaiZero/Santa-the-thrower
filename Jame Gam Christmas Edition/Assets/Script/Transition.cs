using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void outTransition(int index)
    {
        StartCoroutine(trans(index));
    }

    IEnumerator trans(int index)
    {
        anim.Play("Transition");

        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(index);
    }
}
