using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MANAGER : MonoBehaviour
{
    [SerializeField] GameObject first;
    [SerializeField] GameObject second;
    [SerializeField] GameObject third;

    GameObject anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("transition");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void firstPage()
    {
        first.SetActive(false);
        second.SetActive(true);
        third.SetActive(false);
    }

    public void secondPage()
    {
        first.SetActive(false);
        second.SetActive(false);
        third.SetActive(true);
    }

    public void thirdPage()
    {
        first.SetActive(true);
        second.SetActive(false);
        third.SetActive(false);
    }

    public void play()
    {
        anim.GetComponent<Transition>().outTransition(1);
    }
}
