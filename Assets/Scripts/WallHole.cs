using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHole : MonoBehaviour, IInteractable
{
    public Transform @out;
    public Transform @in;

    public GameObject mouseView;

    [SerializeField] WallHole Connection;

    private MouseController _mouse;
    
    public void Interact(MouseController t)
    {
        _mouse = t;
        if (_mouse.IsInWallHole)
        {
            Debug.Log("press O to exit");
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.LogWarning("sali!");
                mouseView.gameObject.SetActive(false);
                _mouse.ExitWallHole(@out);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                GoToOtherHole();

            }
        }
        else
        {
            Debug.Log("press I to enter");
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.LogWarning("Entre!");

                _mouse.EnterWallHole(@in);
                mouseView.gameObject.SetActive(true);

            }
        }
    }

    public void GoToOtherHole() {
        Debug.LogWarning("pase");
        mouseView.gameObject.SetActive(false);
        Connection.mouseView.SetActive(true);
        _mouse.transform.position = Connection.@in.position;

    }
}
