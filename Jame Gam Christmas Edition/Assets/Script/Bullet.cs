using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    [SerializeField] string color;
    [SerializeField] BoxCollider2D bc;
    [SerializeField] BoxCollider2D bcC;
    SortingGroup sg;
    bool isHit = false;
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        transform.rotation = Quaternion.EulerRotation(0,0,0);
        sg = GetComponent<SortingGroup>(); 
        sg.sortingOrder= 1;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            if (isHit == false)
            {
                isHit = true;
                Debug.Log("Hit NPC");
                collision.gameObject.GetComponent<npc>().hit(color);
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Behind")
        {
            if(sg != null)
            sg.sortingOrder = -1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(sg != null)
        sg.sortingOrder = 1;
    }
}
