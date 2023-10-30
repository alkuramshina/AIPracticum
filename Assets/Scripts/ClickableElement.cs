using System;
using UnityEngine;

public class ClickableElement : MonoBehaviour
{
    public event EventHandler OnClickEventHandler;

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit) && hit.collider.TryGetComponent(out ClickableElement element))
            {
                element.OnClickEventHandler?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}