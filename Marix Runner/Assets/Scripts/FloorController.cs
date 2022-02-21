using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloorController : MonoBehaviour
{
    public bool IsOccupied;
    public bool IsChosen;
    public bool IsInTheRange;
    public bool IsInTheWeaponRange;
    public Runner runner;
    public Runner ChosenRunner;
    public weapon ChosenWeapon;
    public BlockController blockController;
    public int x, y;
    [SerializeField]
    public GameObject playMenu;
    void Awake()
    {
        runner = null;
        blockController = GameObject.Find("BlockController").GetComponent<BlockController>();
        playMenu = GameObject.Find("PlayMenu");
        IsOccupied = false;
        IsChosen = false;
        IsInTheRange = false;
        IsInTheWeaponRange = false;
    }
    public void TrunToNextRound()
    {

    }
    public void SetFloor(int setx,int sety)
    {
        x = setx;
        y = sety;
    }
    public void SetRunner(Runner new_runner)
    {
        IsOccupied = true;
        runner = new_runner;
    }
    public void ReturnRunner()
    {
        IsChosen = false;
        IsOccupied = false;
        runner = null;
    }
    private void OnMouseDown()
    {
        if (IsInTheRange == true)
        {
            showMoveRange();
        }
        else if (IsChosen == true && IsOccupied == true)
        {
            ChosenWeapon.showAttackRange();
        }
        else if(IsInTheWeaponRange == true)
        {
            if(IsOccupied == true && runner.teamNum != ChosenWeapon.nowRunner.teamNum)
            {
                ChosenWeapon.Attack(runner);
                Button[] buttons = playMenu.GetComponentsInChildren<Button>();
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].name == "攻击")
                    {
                        buttons[i].interactable = false;
                    }
                    if (buttons[i].name == "移动")
                    {
                        buttons[i].interactable = false;
                    }
                }
                ChosenWeapon.showAttackRange();
            }
        }
        else
        {
            if (IsOccupied == true && runner.teamNum == blockController.moveTeam)
            {
                if (playMenu.GetComponent<Canvas>().enabled  == false)
                {
                    playMenu.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    playMenu.GetComponent<Canvas>().enabled = false;
                }
            }
        }
    }
    public void showMoveRange()
    {
        if(IsOccupied == true)
        {
            if(IsChosen == false)
            {
                runner.showMoveRange();
                IsChosen = true;
            }
            else if(IsChosen == true)
            {
                runner.hideMoveRange();
                IsChosen = false;
                playMenu.GetComponent<Canvas>().enabled = true;
            }
        }
        else
        {
            if (IsInTheRange == true)
            {
                ChosenRunner.move(x, y);
                Button[] buttons = playMenu.GetComponentsInChildren<Button>();
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].name == "移动")
                    {
                        buttons[i].interactable = false;
                        break;
                    }
                }
                playMenu.GetComponent<Canvas>().enabled = true;
            }
        }
    }
}
