using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    GameObject[] weapons;
    public GameObject replaceMenu;
    public Runner player;
    public int ReplaceWeapon;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Runner>();
        replaceMenu = GameObject.Find("ReplaceMenu");
        SetPlayerWeapon(player.weaponNum);
    }
    private void Start()
    {
        replaceMenu.GetComponentInChildren<Canvas>().enabled = false;
    }
    void SetPlayerWeapon(int num)
    {
        if(player.nowWeapon != null)
        {
            Destroy(player.GetComponent<Transform>().GetChild(0).gameObject);
        }
        GameObject newWeapon = Instantiate(weapons[num], player.GetComponent<Transform>());
        newWeapon.transform.localPosition = new Vector3(0.2f, 0, 0);
        player.nowWeapon = newWeapon.GetComponent<weapon>();
        player.nowWeapon.nowRunner = player;
        player.weaponNum = num;
    }
    public void ChangeWeapon()
    {
        replaceMenu.GetComponentInChildren<Canvas>().enabled = false;
        SetPlayerWeapon(ReplaceWeapon);
    }
    public void CloseReplaceMenu()
    {
        replaceMenu.GetComponentInChildren<Canvas>().enabled = false;
    }
}
