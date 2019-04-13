using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//========================================================
// 重力系を扱うクラス
//========================================================
public class GravityController : MonoBehaviour {
	private const float GRAVITY = 9.81f;
	public float gravityScale = 1.0f;	// 重力のスケールパラメータ

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		Vector3 vector = new Vector3();

		// Unity上でのデバックと実機で使い分け
		if (Application.isEditor) {
			// キー入力を検知し、ベクトルを設定
			vector.x = Input.GetAxis("Horizontal");
			vector.z = Input.GetAxis("Vertical");

			if (Input.GetKey("z")) {
				vector.y = 1.0f;
			} else {
				vector.y = -1.0f;
			}
		} else {
			// 加速度センサーの入力をUnity空間の軸にマッピングする
			// Unityと加速度センサーでは、yとz軸が逆転している
			vector.x = Input.acceleration.x;
			vector.z = Input.acceleration.y;
			vector.y = Input.acceleration.z;
		}
		// シーンの重力を入力ベクトルの方向に合わせて変化させる
		Physics.gravity = GRAVITY * vector.normalized * gravityScale;
	}
}
