using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRunner : MonoBehaviour
{
    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标
    [SerializeField]
    GameObject runner;
    void OnMouseDown()
    {
        GameObject point = GameObject.Instantiate(runner, transform.position, transform.rotation) as GameObject;
    }
}
