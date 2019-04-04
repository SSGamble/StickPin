using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    public Transform startPoint;
    public Transform spawnPoint;
    public Transform circle;

    public float speed = 20;

    private bool isFly = false; // 是否处于飞行状态
    private bool isReachStartPoint = false; // 是否到达目标位置
    private Vector3 targetCirclePos; // 针插在 circle 上的目标位置

	void Start () {
        startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        circle = GameObject.Find("Circle").transform;
        // 让针只运行到 circle 的表面
        targetCirclePos = circle.position;
        targetCirclePos.y = targetCirclePos.y - 2f;
    }
	
	void Update () {
		if(!isFly)
        {
            if(!isReachStartPoint) // 移动到目标位置
            {
                transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, startPoint.position) < 0.05f) // 若已到达目标位置，则不再做判断
                {
                    isReachStartPoint = true;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetCirclePos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetCirclePos) < 0.05f) // 若已到达目标位置，则不再做判断
            {
                transform.position = targetCirclePos; // 防止误差
                transform.parent = circle; // 跟随 circle 运动
                isFly = false;
            }
        }
    }

    /// <summary>
    /// 开始发射
    /// </summary>
    public void StartFly()
    {
        isFly = true;
        isReachStartPoint = true;
    }
}
