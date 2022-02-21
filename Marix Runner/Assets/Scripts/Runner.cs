using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Runner : MonoBehaviour
{

    [SerializeField]
    public weapon nowWeapon;
    [SerializeField]
    public int weaponNum;
    Vector2Int[] position;
    int positionCnt;
    [SerializeField]
    public int blood,attack;
    [SerializeField]
    public int teamNum;
    public int rx, ry, num;
    
    [SerializeField]
    public BlockController blockController;
    [SerializeField]
    public WeaponController weaponController;
    [SerializeField]
    int moveRange;

    GameObject playMenu;
    public int MoveRange{ 
        get{
            return moveRange; 
        }
    }
    [SerializeField]
    public Renderer My_Renderer;
    private void Awake()
    {
        blockController = GameObject.Find("BlockController").GetComponent<BlockController>();
        My_Renderer = GetComponent<Renderer>();
        position = new Vector2Int[100000];
        positionCnt = 0;
        playMenu = GameObject.Find("PlayMenu");
        weaponController = GameObject.Find("WeaponController").GetComponent<WeaponController>();
    }
    virtual public void Resprown(float x,float y,int i)
    {
        if (My_Renderer.enabled == false)
        {
            transform.position = new Vector2(x,y);
            num = i;
            rx = blockController.RunnerPosition[num].x;
            ry = blockController.RunnerPosition[num].y;
            My_Renderer.enabled = true;
        }
    }
    public void useFloorToShowMoveRange()
    {
        playMenu.GetComponent<Canvas>().enabled = false;
        blockController.floors[rx, ry].GetComponent<FloorController>().showMoveRange();
    }
    public void useWeaponToShowMoveRange()
    {
        playMenu.GetComponent<Canvas>().enabled = false;
        nowWeapon.showAttackRange();
    }
    public void showMoveRange()
    {
        findMoveRange();
        for (int i = 0; i < positionCnt; i++)
        {
            TrunFloorToYellow(position[i].x, position[i].y);
        }
    }
    public void hideMoveRange()
    {
        for(int i = 0; i < positionCnt; i++)
        {
            TrunFloorToNormal(position[i].x, position[i].y);
        }
    }
    public void showMoveRange2()
    {
        for(int i = -moveRange+rx; i <= moveRange+rx; i++)
        {
            for (int j = -(moveRange - Mathf.Abs(i - rx)) + ry; j <= (moveRange - Mathf.Abs(i - rx)) + ry; j++) 
            {
                if (i >= 0 && i <= blockController.mapx && j >= 0 && j <= blockController.mapy)
                {
                    if (blockController.map[i, j] == -1) continue;
                    if(blockController.floors[i,j].GetComponent<FloorController>().IsOccupied == true)
                    {
                        if(blockController.floors[i, j].GetComponent<FloorController>().runner.teamNum != teamNum)
                        {
                            TrunFloorToRed(i, j);
                        }
                    }
                    else
                    {
                        TrunFloorToYellow(i, j);
                    }
                }
            }
        }
        
    }
    public void hideMoveRange2()
    {
        for (int i = -moveRange + rx; i <= moveRange + rx; i++)
        {
            for (int j = -(moveRange  - Mathf.Abs(i-rx)) + ry; j <= (moveRange  - Mathf.Abs(i-rx)) + ry; j++)
            {
                if (i >= 0 && i <= blockController.mapx && j >= 0 && j <= blockController.mapy)
                {
                    if (blockController.map[i, j] == -1) continue;
                    TrunFloorToNormal(i, j);
                }
            }
        }
    }
    public void TrunFloorToRed(int i, int j)
    {
        blockController.floors[i, j].GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        blockController.floors[i, j].GetComponent<FloorController>().IsInTheRange = true;
        blockController.floors[i, j].GetComponent<FloorController>().ChosenRunner = this;
    }
    public void TrunFloorToYellow(int i,int j)
    {
        blockController.floors[i, j].GetComponent<SpriteRenderer>().color = new Color(242, 255, 0, 255);
        blockController.floors[i, j].GetComponent<FloorController>().IsInTheRange = true;
        blockController.floors[i, j].GetComponent<FloorController>().ChosenRunner = this;
    }
    public void TrunFloorToNormal(int i,int j)
    {
        blockController.floors[i, j].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        blockController.floors[i, j].GetComponent<FloorController>().IsInTheRange = false;
        blockController.floors[i, j].GetComponent<FloorController>().ChosenRunner = null;
    }
    public void getDamege(int damage)
    {
        blood -= damage;
        if(blood <= 0)
        {
            die();
        }
    }
    void findMoveRange()
    {
        positionCnt = 0;
        Queue<Vector3Int> findposition = new Queue<Vector3Int>();
        int[,] IsFindposition = new int[20,20];
        findposition.Enqueue(new Vector3Int(rx, ry, 0));
        Vector3Int[] changes = new Vector3Int[4];
        changes[0] = new Vector3Int(0, 1, 1);
        changes[1] = new Vector3Int(1, 0, 1);
        changes[2] = new Vector3Int(-1, 0, 1);
        changes[3] = new Vector3Int(0, -1, 1);
        while (findposition.Count != 0)
        {
            Vector3Int nowposition = findposition.Dequeue();
            position[positionCnt++] = new Vector2Int(nowposition.x, nowposition.y);
            for (int i = 0; i < 4; i++)
            {
                Vector3Int newposition = nowposition + changes[i];
                if (newposition.z <= moveRange && newposition.x >= 0 && newposition.x <= blockController.mapx && newposition.y >= 0 && newposition.y <= blockController.mapy) 
                {
                    if (blockController.map[newposition.x, newposition.y] == -1 || IsFindposition[newposition.x, newposition.y] == 1) continue;
                    if (blockController.floors[newposition.x, newposition.y].GetComponent<FloorController>().IsOccupied == false)
                    {
                        findposition.Enqueue(newposition);
                        IsFindposition[newposition.x, newposition.y] = 1;
                    }
                }
            }
        }
    }
    public void move(int x,int y)
    {
        hideMoveRange();
        float sx = blockController.sx;
        float sy = blockController.sy;
        blockController.floors[rx, ry].GetComponent<FloorController>().ReturnRunner();
        transform.position = new Vector2(sx + x, sy + y);
        rx = x;
        ry = y;
        blockController.RunnerPosition[num].x = rx;
        blockController.RunnerPosition[num].y = ry;
        blockController.floors[rx, ry].GetComponent<FloorController>().SetRunner(this);
    }
    public void die()
    {
        My_Renderer.enabled = false;
        blockController.floors[rx, ry].GetComponent<FloorController>().ReturnRunner();
    }
}
