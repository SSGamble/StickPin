using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// circle 旋转
/// </summary>
public class RotateSelf : MonoBehaviour {

    public float speedRotate = 50;
	
	void Update () {
        transform.Rotate(new Vector3(0, 0, -speedRotate * Time.deltaTime)); // 绕 z 轴旋转。- 表顺时针旋转
	}
}
