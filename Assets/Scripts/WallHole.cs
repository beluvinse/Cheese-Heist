using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHole : MonoBehaviour, IInteractable
{
    public Transform @out;
    public Transform @in;

    public GameObject mouseView;
    public GameObject Canvas;
    public GameObject buttonIn;
    public GameObject buttonOut;
    public GameObject buttonConnection;

    [SerializeField] WallHole Connection;

    private MouseController _mouse;

    private void OnTriggerExit(Collider other)
    {
        if(other == _mouse.GetComponent<Collider>()) { Canvas.SetActive(false); }
    }

    public void Interact(MouseController t)
    {
        _mouse = t;
        Canvas.SetActive(true);
        if (_mouse.IsInWallHole)
        {
            buttonOut.SetActive(true);
            buttonConnection.SetActive(true);
            buttonIn.SetActive(false);
        }
        else
        {
            buttonConnection.SetActive(false);
            buttonOut.SetActive(false);
            buttonIn.SetActive(true);
        }
        /*if (_mouse.IsInWallHole)
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
        }*/
    }

    public void GetIn()
    {
        _mouse.EnterWallHole(@in);
        mouseView.gameObject.SetActive(true);
    }

    public void GetOut()
    {
        mouseView.gameObject.SetActive(false);
        _mouse.ExitWallHole(@out);
    }

    public void GoToOtherHole() {
        Debug.LogWarning("pase");
        mouseView.gameObject.SetActive(false);
        Connection.mouseView.SetActive(true);
        _mouse.transform.position = Connection.@in.position;

    }
}
