using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickControl : Controller, IDragHandler, IEndDragHandler
{
    Vector3 _moveDir;
    Vector3 initialPosition;
    [SerializeField] float maxMagnitude = 75;

    void Start()
    {
        initialPosition = transform.position;
    }

    public override Vector3 GetMovementInput()
    {
        Vector3 moreDirModified = new Vector3(_moveDir.x, 0, _moveDir.y);
        moreDirModified = moreDirModified / maxMagnitude;
        return moreDirModified;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _moveDir = Vector3.ClampMagnitude((Vector3)eventData.position - initialPosition, maxMagnitude);

        transform.position = initialPosition + _moveDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initialPosition;
        _moveDir = Vector3.zero;
    }
}
