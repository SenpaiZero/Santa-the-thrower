using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float bulletSpeed = 3f;
    private float distance = 0.75f;
    [SerializeField] GameObject[] bullets;
    [SerializeField] Transform[] target;
    Player player;
    int bulletIndex = 0;
    bool isClick = false, isShoot = false;
    ui ui_;
    void Start()
    {

        ui_ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ui>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getWalk() == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (isShoot == false)
                {
                    if (ui_.getBullet() > 0)
                    {
                        StartCoroutine(shootCooldown());
                        isClick = true;
                        ui_.setBullet(ui_.getBullet() - 1);
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (isClick == true)
        {
            GameObject bulletClone = Instantiate(bullets[bulletIndex], target[bulletIndex].transform.position, target[bulletIndex].transform.rotation);
            Rigidbody2D rb = bulletClone.GetComponent<Rigidbody2D>();


            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.AddForce((dir * bulletSpeed) * Time.deltaTime, ForceMode2D.Impulse);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, bulletSpeed);
            Debug.Log(dir);
            StartCoroutine(setGravity(rb));
            isClick = false;
        }
        
    }
    IEnumerator setGravity(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(distance);
        if (rb != null)
        {
            rb.gravityScale = 1;
            yield return new WaitForSeconds(0.2f);
            if (rb != null) rb.gravityScale = 0;
            if (rb != null) rb.velocity = Vector3.zero;
            if (rb != null) rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    IEnumerator shootCooldown()
    {
        isShoot = true;
        yield return new WaitForSeconds(0.5f);
        isShoot = false;
    }
    public void setIndex(int index)
    {
        bulletIndex = index;
    }

    public void setBulletDistance()
    {
        distance += 0.2f;
    }

    public float getBulletDistance()
    {
        return distance;
    }
}
