using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private GameObject clickViewerPrefab;
    [SerializeField] private Color color;
    [SerializeField] private RectTransform canvas;
    
    private void Update()
    {
        if (IsClick())
        {
            var obj = Instantiate(clickViewerPrefab, GetClickPos(), Quaternion.identity);
            obj.transform.parent = canvas;
            obj.GetComponent<ClickViewer>().SetColor(color);
        }
    }

    private bool IsClick()
    {
        return Input.GetMouseButtonDown(0);
    }

    private Vector3 GetClickPos()
    {
        return Input.mousePosition;
    }
}
