using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] Transform gift;
    [SerializeField] SpriteRenderer[] sprite;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SortingGroup sg;
    [SerializeField] SpriteRenderer[] roof;
    [SerializeField] GameObject[] giftGun;

    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float bulletSpeed;

    public static int gunC = 0;
    bool isInside = false;
    bool isWalk = false;
    Vector2 playerDirection;
    AudioSource pickupSfx;

    ui ui_;
    // Start is called before the first frame update
    void Start()
    {
        ui_ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ui>();
        pickupSfx= gameObject.GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalk == true)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");

            playerDirection = new Vector2(directionX, directionY).normalized;
            if (directionX == -1) sprite[0].flipX = true;
            if (directionX == 1) sprite[0].flipX = false;
            if (directionX != 0 || directionY != 0)
            {
                animations("Walk");
            }
            else
            {
                animations("Player");
            }
            giftRotate();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                gunC = 0;
                giftGun[0].SetActive(true);
                giftGun[1].SetActive(false);
                giftGun[2].SetActive(false);
                giftGun[0].GetComponent<Gun>().setIndex(gunC);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                gunC = 1;
                giftGun[0].SetActive(false);
                giftGun[1].SetActive(true);
                giftGun[2].SetActive(false);
                giftGun[1].GetComponent<Gun>().setIndex(gunC);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                gunC = 2;
                giftGun[0].SetActive(false);
                giftGun[1].SetActive(false);
                giftGun[2].SetActive(true);
                giftGun[2].GetComponent<Gun>().setIndex(gunC);
            }
        }
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
        if (isInside == true)
        {
            if(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize > 4)
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize -= 1 * Time.deltaTime;

            GameObject.FindGameObjectWithTag("GameController").GetComponent<ParticleSystem>().emissionRate = 0;
        }
        else
            {
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize < 5)
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize += 1 * Time.deltaTime;
                    GameObject.Find("Game Manager").GetComponent<ParticleSystem>().emissionRate = 50;
        }
    }


    void giftRotate() 
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(giftGun[gunC].transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        giftGun[gunC].transform.rotation = Quaternion.AngleAxis(angle - 45f, Vector3.forward);
        giftGun[gunC].transform.GetChild(0).GetComponent<Transform>().transform.rotation = Quaternion.AngleAxis(angle - 45f, Vector3.forward);
    }

    void animations(string anim) 
    {
        playerAnim.Play(anim);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Behind")
        {
            Debug.Log("Touching");
            sg.sortingOrder = -1;
        }
        if(other.gameObject.tag == "Long House")
        {
            //long house
            roof[1].GetComponent<Animator>().Play("Roofs");
            isInside = true;
        }
        if (other.gameObject.tag == "Short House")
        {
            //long house
            roof[0].GetComponent<Animator>().Play("Roofs");
            isInside = true;
        }
        if(other.gameObject.tag == "xmas")
        {
            ui_.setBullet(ui_.getBulletCap());
        }

        if(other.gameObject.tag == "Bullet")
        {
            pickupSfx.Play();
            int bullets = ui_.getBullet();
            ui_.setBullet(bullets + 1);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "speed")
        {
            pickupSfx.Play();
            giftGun[0].GetComponent<Gun>().setBulletDistance();
            giftGun[1].GetComponent<Gun>().setBulletDistance();
            giftGun[2].GetComponent<Gun>().setBulletDistance();

            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "scoreMult")
        {
            pickupSfx.Play();
            npc Npc = GameObject.FindGameObjectWithTag("NPC").GetComponent<npc>();
            Npc.setScoreMult();
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "timer")
        {
            pickupSfx.Play();
            ui_.setTimer();
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sg.sortingOrder = 1;
        if (collision.gameObject.tag == "Long House")
        {
            //long house
            roof[1].GetComponent<Animator>().Play("Exit Roof");
            isInside = false;
        }
        if (collision.gameObject.tag == "Short House")
        {
            //long house
            roof[0].GetComponent<Animator>().Play("Exit Roof");
            isInside = false;
        }
    }

    public void setWalk(bool isWalk)
    {
        this.isWalk = isWalk;
    }

    public bool getWalk()
    {
        return isWalk;
    }

}
