using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InventorySlider : NetworkBehaviour
{
    public GameObject panelMenu;

    public void ShowHideMenu()
    {
        if(panelMenu != null)
        {
            Animator animator = panelMenu.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("Display");
                animator.SetBool("Display", !isOpen);

            }
        }
    }
}
