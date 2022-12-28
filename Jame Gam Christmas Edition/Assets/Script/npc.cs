using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class npc : MonoBehaviour
{
    private float speed = 2f;
    float y, x;
    string color;
    bool isHit = false, isReset = true;
    bool changeDir = false;
    float size = 1f;
    static int scoreMul = 1;

    private Rigidbody2D rb;
    Vector2 npcDirection;
    GameObject cloneGift, check_, wrong_;
    Animator anim;
    SortingGroup sg;
    [SerializeField]AudioSource wrongSfx;
    [SerializeField] AudioSource correctSfx;

    [SerializeField] GameObject[] gifts;
    [SerializeField] Transform target;
    [SerializeField] GameObject check;
    [SerializeField] GameObject wrong;
    [SerializeField] GameObject angry;
    [SerializeField] GameObject[] powerups;
    [SerializeField] TextMeshProUGUI popup;
    [SerializeField] Animator animScore;
    // Start is called before the first frame update
    void Start()
    {

        sg = GetComponent<SortingGroup>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        setRandomDir();
        randomizeGift();

        size = Random.Range(0.7f, 1.3f);
        speed = Random.Range(1, 5);
        anim.speed = (speed-0.2f);
        gameObject.transform.localScale = new Vector3(size, size, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (changeDir == false) StartCoroutine(randomDir_wait());
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(npcDirection.x * speed, npcDirection.y * speed);
    }

    void setRandomDir() 
    {
        x = Random.Range(0, 2) * 2 - 1;
        y = Random.Range(0, 2) * 2 - 1;

        float directionX = x;
        float directionY = y;
        npcDirection = new Vector2(directionX, directionY).normalized;

        if (x == -1) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (x == 1) gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    IEnumerator randomDir() 
    {
        setRandomDir();
        yield return new WaitForSeconds(2);
    }

    IEnumerator randomDir_wait()
    {
        changeDir = true;
        setRandomDir();
        yield return new WaitForSeconds(2);
        changeDir = false;
    }

    private void setSpeed(float speed)
    {
        this.speed = speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        StartCoroutine("randomDir");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Behind")
        {
            Debug.Log("Touching");
            sg.sortingOrder = -2;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Behind")
        {
            Debug.Log("Touching");
            sg.sortingOrder = -2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sg.sortingOrder = 1;
    }
    public void hit(string color)
    {
        ui ui_ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ui>();
        if (isHit == false)
        {
            if (this.color.ToLower().Equals(color))
            {
                popup.color = Color.green;
                textPopup("+" + scoreMul);
                correctSfx.Play();
                //50% chance to drop
                int rand = Random.Range(1, 3);
                Debug.Log("Rand "+rand);
                if (rand == 2)
                {
                    Instantiate(powerups[Random.Range(0, 3)], transform.position, Quaternion.identity);
                }
                isReset = false;
                ui_.setScore(ui_.getScore() + scoreMul);
                isHit = true;
                check_ = Instantiate(check, target.transform.position, Quaternion.identity);
                check_.transform.parent = gameObject.transform;
                Destroy(cloneGift);
                resetNpc_();
            }
            else
            {
                popup.color = Color.red;
                textPopup("-1");
                screenShake(); 
                isReset = false;
                ui_.setScore(ui_.getScore() - 1);
                isHit = true;
                Destroy(check_);
                wrong_ = Instantiate(wrong, target.transform.position, Quaternion.identity);
                wrong_.transform.parent = gameObject.transform;
                Destroy(cloneGift);
                resetNpc_();
            }
        }
        else
        {
            popup.color = Color.red;
            textPopup("-1");
            screenShake();
            isReset = false;
            ui_.setScore(ui_.getScore() - 1);
            GameObject angry_ = Instantiate(angry, target.transform.position, Quaternion.identity);
            angry_.transform.parent = gameObject.transform;
            Destroy(check_);
            Destroy(wrong_);
        }

    }

    void textPopup(string txt)
    {
        popup.text = txt;
        animScore.Play("score +");
    }
    void screenShake()
    {
        wrongSfx.Play();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("Screenshake v2");
    }

    public void setScoreMult()
    {
        scoreMul++;
    }

    void resetNpc_()
    {

        if (isReset == false)
        {
            StartCoroutine(resetNpc());
        }
    }
    IEnumerator resetNpc()
    {
        isReset = true;
        yield return new WaitForSeconds(15);
        Destroy(check_);
        Destroy(wrong_);
        Destroy(cloneGift);
        randomizeGift();

        size = Random.Range(0.7f, 1.3f);
        speed = Random.Range(1, 5);
        anim.speed = (speed - 0.2f);
        gameObject.transform.localScale = new Vector3(size, size, 0);
        isHit = false;
    }

    void randomizeGift()
    {
        int randGift = Random.Range(0, gifts.Length);
        cloneGift = Instantiate(gifts[randGift], target.transform.position, Quaternion.identity);
        cloneGift.transform.parent = gameObject.transform;
        //blue
        //green
        //red
        if (randGift == 0) color = "blue";
        if (randGift == 1) color = "green";
        if (randGift == 2) color = "red";
    }
}
