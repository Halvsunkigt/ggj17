using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject[] menuItems;
    public GameObject knob;
    bool ready = true;
    int index = 0;

    CameraShake cameraShake;

	// Use this for initialization
	void Start () {
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetAxisRaw("Horizontal_Player1") == 1 || Input.GetAxisRaw("Vertical_Player1") == 1) && ready)
        {
            //move menu right
            index++;
            DisableMenu();
            try
            {
                menuItems[index].SetActive(true);
            }
            catch (System.Exception)
            {
                index = 0;
                menuItems[index].SetActive(true);
            }
        }

        if ((Input.GetAxisRaw("Horizontal_Player1") == -1 || Input.GetAxisRaw("Vertical_Player1") == -1 ) && ready)
        {
            //move menu right
            index--;
            DisableMenu();
            try
            {
                menuItems[index].SetActive(true);

            }
            catch (System.Exception)
            {
                index = menuItems.Length - 1;
                menuItems[index].SetActive(true);
            }
        }

        if ( Mathf.Abs(Input.GetAxisRaw("Horizontal_Player1") + Input.GetAxisRaw("Vertical_Player1")) < 0.1f)
        {
            ready = true;
        }

        knob.transform.eulerAngles = new Vector3(-120 + index * 20, 90, 180);
	}

    void DisableMenu()
    {
        ready = false;
        cameraShake.ShakeCamera(0.65f, 0.005f);
        foreach (var item in menuItems)
        {
            item.SetActive(false);
        }
    }
}
