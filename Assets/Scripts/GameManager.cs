using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Transform startPoint; // 起始点坐标
    public Transform spawnPoint; // 生成点坐标

    private Pin currentPin; // 当前针
    public GameObject pin; // 针，预制体
    public Text scoreText; // UI ，分数
    private Camera mainCamera; // 主摄像机

    private int score = 0; // 分数
    private int speedAnimation = 10; // 结束动画，渐变速度

    private bool isGameOver = false; // 游戏是否结束

	void Start () {
        startPoint = GameObject.Find("StartPoint").transform; // 获取起始点位置
        mainCamera = Camera.main; // 获取主摄像机

        GetPin(); // 实例化针
    }

    private void Update()
    {
        if (isGameOver) return;
        if (Input.GetMouseButtonDown(0)) // 鼠标左键，开始发射
        {
            currentPin.StartFly();
            // 分数增加
            score++;
            scoreText.text = score.ToString();
            GetPin(); // 实例化下一个针
        }
    }

    /// <summary>
    /// 生成 针，将生成的针赋值给 当前针
    /// </summary>
    void GetPin()
    {
        currentPin = GameObject.Instantiate(pin, spawnPoint.position, pin.transform.rotation).GetComponent<Pin>();
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
        if (isGameOver) return;
        GameObject.Find("Circle").GetComponent<RotateSelf>().enabled = false; // 禁用旋转
        isGameOver = true;
        StartCoroutine(GameOverAnimation()); // 开启协程
    }

    /// <summary>
    /// 协程，游戏结束的动画
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOverAnimation()
    {
        while (true)
        {
            // 更改摄像机的颜色
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, speedAnimation * Time.deltaTime);
            // 更改摄像机的视野
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, speedAnimation * Time.deltaTime);
            // 动画结束后，跳出循环
            if (Mathf.Abs(mainCamera.orthographicSize - 4) < 0.01f)
            {
                break;
            }
            yield return 0;
        }
        yield return new WaitForSeconds(0.3f); // 等待 0.3 秒
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 重新加载场景
    }
}
