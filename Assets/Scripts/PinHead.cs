using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 针头
/// </summary>
public class PinHead : MonoBehaviour {

    /// <summary>
    /// 触发检测
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PinHead") // 如果针头之间发生了接触
        {
            // 调用 GameManager 物体上 GameManager 脚本中的 GameOver 方法
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }
}
