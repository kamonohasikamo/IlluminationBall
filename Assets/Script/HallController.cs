using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//============================================================
// HallControllerクラス
//============================================================
public class HallController : MonoBehaviour {
	private bool fallInHall = false;

	public string activeTag;

	//------------------------------------------
	// ボールが入っているかどうかを返す関数
	//------------------------------------------
	public bool getIsFallInHall() {
		return fallInHall;
	}

	//------------------------------------------
	// コリジョンイベント処理
	//------------------------------------------
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == activeTag) {
			fallInHall = true;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == activeTag) {
			fallInHall = false;
		}
	}

	//------------------------------------------
	// ホールにボールが近づいて来た時の処理
	//------------------------------------------
	void OnTriggerStay(Collider other) {
		//コライダに触れているオブジェクトのRigidbodyコンポーネントを取得
		Rigidbody colliderTouchingObject = other.gameObject.GetComponent<Rigidbody>();

		//ボールがどの方向にあるかを計算
		Vector3 directionBallToCollider = transform.position - other.gameObject.transform.position;
		directionBallToCollider.Normalize();

		//タグに応じて力を加える
		if (other.gameObject.tag == activeTag) {
			// 中心地点でボールを止めるために速度を減衰させる
			colliderTouchingObject.velocity *= 0.9f;

			colliderTouchingObject.AddForce(directionBallToCollider * colliderTouchingObject.mass * 20.0f);
		} else {
			colliderTouchingObject.AddForce(- directionBallToCollider * colliderTouchingObject.mass * 80.0f);
		}
	}

}
