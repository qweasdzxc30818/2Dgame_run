using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text Lodingtext;
    public Image LodingImage;
    /// <summary>
    /// 充新開始
    /// </summary>
    /// <param name="scene"></param>
    public void RESTART(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    /// <summary>
    /// 離開
    /// </summary>
    public void EXIT()
    {
        Application.Quit();
    }
    /// <summary>
    /// 開始載入
    /// </summary>
    public void StarLoding()
    {
        StartCoroutine(Loding());
    }

    public IEnumerator Loding()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("關卡1");
        ao.allowSceneActivation = false;
        while (ao.isDone == false)
        {
            Lodingtext.text = ao.progress + "/100";
            yield return new WaitForSeconds(.1f);

        }

    }
}
