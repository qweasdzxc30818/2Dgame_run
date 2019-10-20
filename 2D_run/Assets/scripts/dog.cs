using UnityEngine;

public class dog : MonoBehaviour
{
    #region 欄位
    //region(可整理code)
    // private 私人(不顯示) public 公開(顯示)
    [Header("跳躍次數")]
    [Range(1,10)]
    public int jumpCount = 2;   //field欄位(變數)
    [Header("跳躍高度")]//(可加在同個中括號 但要逗號)
    public int jume = 1000;
    [Range(0, 10.5f)]
    public float moveSpeed = 1.5f;
    [Tooltip("判定是否在地上")]//tooltip(文字描述)
    public bool isGround; //布林 true or false
    public string Name = "Tuna";
    public Transform cha, cam;//可以儲存物件的控制
    #endregion
    //開始事件 遊戲開始時執行一次
    private void Start()
    {
        print("hello");
    }
    //更新事件 每一禎執行一次 約60fps
    private void Update()
    {
        print("hey");
        movecha();
        movecam();
    }

    /// <summary>
    ///角色移動方法 
    /// </summary>
    private void movecha()
    {
        cha.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// 攝影機移動方法
    /// </summary>
    private void movecam()
    {
        cam.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// 跳躍方法
    /// </summary>
    public void Jump()
    {
        print("跳躍");
    }

    /// <summary>
    /// 滑行方法
    /// </summary>
    public void Slide()
    {
        print("滑行");
    }
}