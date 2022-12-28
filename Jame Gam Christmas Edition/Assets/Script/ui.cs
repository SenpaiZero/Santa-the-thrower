using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ui : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] TextMeshProUGUI score_text;
    [SerializeField] TextMeshProUGUI gifts_text;
    [SerializeField] GameObject canvas;
    [SerializeField] Animator anim;
    [SerializeField] Animator warnAnim;

    AudioSource music;

    float time = 100;
    int score;
    int gifts, giftCap;
    bool isTime = false, isDone = false;
    void Start()
    {
        gifts = 10;
        setBulletCap(10);
        setScore(0);
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        time_text.text = "TIMER:\n" + string.Format("{0:00}:{1:00}", minutes, seconds);

        music = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (time <= 0 && isDone == false)
        {
            isDone = true;
            canvas.SetActive(true);
            music.volume = 0.1f;
            music.pitch = 0.6f;
            anim.Play("Game over");
            GameObject.FindGameObjectWithTag("gameover").GetComponent<GameOver>().setHighScore(score);

            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setWalk(false);
        }
        if (isTime == false && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getWalk() == true) StartCoroutine(timer());

    }

    public void startMusic()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
    }

    IEnumerator timer()
    {
        isTime= true;
        yield return new WaitForSeconds(1);
        if(time > 0) time -= 1;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        time_text.text = "TIMER:\n" + string.Format("{0:00}:{1:00}", minutes, seconds);
        isTime = false;
    }

    public void setTimer()
    {
        time += 3;
    }

    public void setBullet(int bullet)
    {
        gifts = bullet;
        updateGift();
    }

    public int getBullet()
    {
        return gifts;
    }

    public void updateGift()
    {
        gifts_text.text = "GIFT: " + getBullet() + "/10";
        if (getBullet() <= 0)
        {
            warnAnim.Play("no ammo");
        }
        else
        {
            warnAnim.Play("New State");
        }
    }

    public void setBulletCap(int bullet)
    {
        giftCap = bullet;
    }

    public int getBulletCap()
    {
        return giftCap;
    }

    public void setScore(int score)
    {
        this.score = score;
        score_text.text = "SCORE: " + getScore();
    }

    public int getScore()
    {
        if(score <= 0)
        {
            return 0;
        }
        return score;
    }
}
