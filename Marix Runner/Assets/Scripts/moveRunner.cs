using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRunner : MonoBehaviour
{
    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标

    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标

    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标
    bool Enabled_Move = true;
    private void Update()
    {
        if (Input.GetMouseButton(0)&&Enabled_Move)
        {
            screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            mousePositionOnScreen = Input.mousePosition;
            mousePositionOnScreen.z = screenPosition.z;
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            transform.position = mousePositionInWorld;
        }
        else
        {
            Enabled_Move = false;
        }
    }
}
