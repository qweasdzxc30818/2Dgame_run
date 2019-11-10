using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections;

public class dog : MonoBehaviour
{
    #region 欄位
    //region(可整理code)
    // private 私人(不顯示) public 公開(顯示)
    [Header("跳躍次數")]
    [Range(1,10)]
    public int jumpCount = 2;   //field欄位(變數)
    [Header("跳躍高度")]//(可加在同個中括號 但要逗號)
    [Range(0,1500)]
    public int jump;
    [Range(0, 10.5f)]
    public float moveSpeed = 1.5f;
    [Tooltip("判定是否在地上")]//tooltip(文字描述)
    public bool isGround; //布林 true or false
    public string Name = "Tuna";
    public AudioClip Jump_1, Slide_1;//音源片段
    private Transform  cam;//可以儲存物件的控制
    private Animator ani;//儲存動畫器
    private CapsuleCollider2D cc2d;//碰撞器
    private Rigidbody2D Rgi2D;//剛體
    private new AudioSource audio;//音源
    private SpriteRenderer sr;//物體顯隱
    [Header("血量")]
    public float health;
    private float maxHp;
    public Image hp;
    public float dam;//障礙物傷害
    [Header("拼接地圖")]
    public Tilemap tileprop;
    [Header("道具")]
    public int textCherry;
    public int textDiamond;
    public Text Diamond;
    public Text Cherry;
    [Header("遺失血量大小")]
    public float lose = 1;
    public Text finalDiamond, finalCherry, finaltime, finaltotal;
    public int scorediamond, scorecherry,scoretime, scoretotal;
    public GameObject Final;

    #endregion

    #region 事件
    //開始事件 遊戲開始時執行一次
    private void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
        ani = GetComponent<Animator>();
        cc2d = GetComponent<CapsuleCollider2D>();
        audio = GetComponent<AudioSource>();
        Rgi2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        maxHp = health;
    }

    //更新事件 每一禎執行一次 約60fps
    private void Update()
    {
        movecha();
        movecam();
        loseHP();
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
        if (collision.gameObject.name == "道具")
        {
            eat(collision);
        }
    }

    /// <summary>
    /// 觸碰障礙物時執行
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "障礙物")
        {
            health = health - dam;
            Damage();
            hp.fillAmount = health / maxHp;
            sr.enabled = false;
            Invoke("Show", .3f);
            Dead();
        }
        if(collision.tag == "鑽石")
        {
            eatDi(collision);
        }
        if(collision.name == "死亡區域")
        {
            health = 0;
            Dead();
        }
    }
    #endregion

    #region 方法
    /// <summary>
    /// 鑽石消除方法
    /// </summary>
    /// <param name="collision"></param>
    private void eatDi(Collider2D collision)
    {
        Destroy(collision.gameObject);
        textDiamond++;
        Diamond.text = textDiamond.ToString();
    }
    /// <summary>
    /// 櫻桃消除方法
    /// </summary>
    /// <param name="collision"></param>
    private void eat(Collision2D collision)
    {
        Debug.Log("吃到道具");
        Vector3 hitpoint = Vector3.zero;//碰撞點
        hitpoint =  collision.contacts[0].point;
        Vector3 por = Vector3.zero;
        Vector3 normal = collision.contacts[0].normal;//法線取得
        por.x = hitpoint.x - normal.x * 0.01f;
        por.y = hitpoint.y - normal.y * 0.01f;
        tileprop.SetTile(tileprop.WorldToCell(por), null);//消除道具
        textCherry++;
        Cherry.text = textCherry.ToString();
 
    }
    private void loseHP()
    {
        health -= Time.deltaTime * lose;
        hp.fillAmount = health / maxHp;
        Dead();
    }
    private void Show()
    {
        sr.enabled = true;
    }
    /// <summary>
    /// 角色受傷
    /// </summary>
    private void Damage()
    {
        Debug.Log("Hurt!");
    }
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
            if (health == 0) return;
            print("跳躍");
            ani.SetBool("跳躍開關", true);
            Rgi2D.AddForce(new Vector2(0,jump));
            audio.PlayOneShot(Jump_1, 0.7f);
            isGround = false;
        }
    }

    /// <summary>
    /// 滑行方法與動畫器
    /// </summary>
    public void Slide()
    {
        if (health == 0) return;
        print("滑行");
        ani.SetBool("滑行開關", true);
        cc2d.offset = new Vector2(0.02f, -0.78f);
        cc2d.size = new Vector2(0.97f, 1.09f);
        audio.PlayOneShot(Slide_1, 0.7f);
    }

    /// <summary>
    /// 重設動畫器布林值
    /// </summary>
    public void ResetAnimator()
    {
        ani.SetBool("跳躍開關", false);
        ani.SetBool("滑行開關", false);
        cc2d.offset = new Vector2(0.2f, -0.13f);
        cc2d.size = new Vector2(1.2f, 2.45f);
    }
    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        if (health <= 0)
        {
            moveSpeed = 0;
            ani.SetBool("死亡開關", true);
            Finalscene();

        }
    }
    /// <summary>
    ///結算畫面 
    /// </summary>
    private void Finalscene()
    {
        if (Final.activeInHierarchy==false)
        {
            Final.SetActive(true);
            StartCoroutine(Finalcount(textDiamond,scorediamond,100,finalDiamond));
            StartCoroutine(Finalcount(textCherry,scorecherry, 50 ,finalCherry, textDiamond* 0.2f));
           // StartCoroutine(FinalTime());
           // StartCoroutine(FinalScore());
        }
    }
    /// <summary>
    /// 結算計分
    /// </summary>
    /// <param name="count"></param>
    /// <param name="score"></param>
    /// <param name="add"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private IEnumerator Finalcount(int count, int score , int add , Text text,float wait = 0)
    {
        yield return new WaitForSeconds(wait);
        while (count > 0)
        {
            count--;
            score += add;
            text.text = score.ToString();
            yield return new WaitForSeconds(0.3f);
        }

    }
    /*
    /// <summary>
    /// 鑽石跳分
    /// </summary>
    /// <returns></returns>
    private IEnumerator FinalDiamond()
    {
        
        while (textDiamond > 0)
        {
            textDiamond--;
            scorediamond += 100;
            finalDiamond.text = scorediamond.ToString();
            yield return new WaitForSeconds(0.3f);
        }

    }
    /// <summary>
    /// 櫻桃跳分
    /// </summary>
    /// <returns></returns>
    private IEnumerator FinalCherry()
    {
        
        while (textCherry > 0)
        {
            textCherry--;
            scorecherry += 100;
            finalCherry.text = scorecherry.ToString();
            yield return new WaitForSeconds(0.3f);
        }

    }
    */
    /*
    /// <summary>
    ///時間跳分 
    /// </summary>
    /// <returns></returns>
    private IEnumerator FinalTime()
    {
        

    }
    /// <summary>
    /// 總分
    /// </summary>
    /// <returns></returns>
    private IEnumerator FinalScore()
    {
        
        

    }
    */
    #endregion
}