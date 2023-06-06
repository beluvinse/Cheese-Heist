using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHole : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _out;
    [SerializeField] Transform _in;

    [SerializeField] GameObject mouseView;

    private MouseController _mouse;
    
    public void Interact(MouseController t)
    {
        _mouse = t;
        if (_mouse.IsInWallHole)
        {
            Debug.Log("press O to exit");
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("sali!");
                mouseView.gameObject.SetActive(false);
                _mouse.ExitWallHole(_out);
            }
        }
        else
        {
            Debug.Log("press I to enter");
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Entre!");
                mouseView.gameObject.SetActive(true);

                _mouse.EnterWallHole(_in);

            }
        }
    }
}
