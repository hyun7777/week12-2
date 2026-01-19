using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool canPlayerMove = true; // 플레이어의 움직임 제어

    public static bool isOpenInventory = false; // 인벤토리 활성화
    public static bool isOpenCragtManual = false; // 건축 메뉴 활성화

    public static bool isNight = false;
    public static bool isWater = false;

    private WeaponManager theWM;
    private bool flag = false;

    public static bool isPause = false; // 메뉴가 호출되면 true

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        theWM = FindAnyObjectByType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenInventory || isOpenCragtManual || isPause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canPlayerMove = false;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canPlayerMove = true;
        }

        if (isWater)
        {
            if (!flag)
            {
                StopAllCoroutines();
                StartCoroutine(theWM.WeaponInCoroutine());
                flag = true;
            }
        }
        else
        {
            if (flag)
            {
                flag = false;
                theWM.WeaponOut();
            }
        }
    }
}
