﻿using UnityEngine;

public class dog : MonoBehaviour
{
    #region 欄位
    //region(可整理code)
    // private 私人(不顯示) public 公開(顯示)
    [Header("跳躍次數")]
    [Range(1,10)]
    public int jumpCount = 2;   //field欄位(變數)
    [Header("跳躍高度")]//(可加在同個中括號 但要逗號)
    [Range(0,600)]
    public int jump;
    [Range(0, 10.5f)]
    public float moveSpeed = 1.5f;
    [Tooltip("判定是否在地上")]//tooltip(文字描述)
    public bool isGround; //布林 true or false
    public string Name = "Tuna";
    public Transform cha, cam;//可以儲存物件的控制
    public Animator ani;
    public CapsuleCollider2D cc2d;
    public Rigidbody2D Rgi2D;
    #endregion
    #region 事件
    //開始事件 遊戲開始時執行一次
    private void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
        ani = GetComponent<Animator>();
        cc2d = GetComponent<CapsuleCollider2D>();
        Rgi2D = GetComponent<Rigidbody2D>();
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
    /// 判定是否在地上
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "地板")
        {
            isGround = true;
        }
    }
    #endregion
    #region 方法
    /// <summary>
    ///角色移動方法 
    /// </summary>
    private void movecha()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
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
        if (isGround == true)
        {
            print("跳躍");
            ani.SetBool("跳躍開關", true);
            Rgi2D.AddForce(new Vector2(0,jump));
            isGround = false;
        }
    }

    /// <summary>
    /// 滑行方法與動畫器
    /// </summary>
    public void Slide()
    {
        print("滑行");
        ani.SetBool("滑行開關", true);
        cc2d.offset = new Vector2(0.02f, -0.78f);
        cc2d.size = new Vector2(0.97f, 1.09f);
    }

    /// <summary>
    /// 重設動畫器布林值
    /// </summary>
    public void ResetAnimator()
    {
        ani.SetBool("跳躍開關", false);
        ani.SetBool("滑行開關", false);
        cc2d.offset = new Vector2(-0.06f, -0.09f);
        cc2d.size = new Vector2(1.5f, 2.4f);
    }
    #endregion
}