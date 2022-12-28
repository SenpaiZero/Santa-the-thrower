using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class cursor : MonoBehaviour
{
   // public Texture2D cursorTexture;
   // public CursorMode cursorMode = CursorMode.Auto;
   // public Vector2 hotSpot = Vector2.zero;
    public AudioSource clickSfx;
    public AudioSource noAmmo;
    public ui ui_;
    // Start is called before the first frame update
    void Start()
    {
       // UnityEngine.Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (ui_.getBullet() > 0)
                {
                    clickSfx.Play();
                }
                else
                {
                    noAmmo.Play();
                }
            }
            else
            {
                clickSfx.Play();
            }
        }
    }

}
