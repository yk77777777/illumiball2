using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    //どのボールを吸い寄せるかをタグで指定
    public string targetTag;
    bool isHolding;

    //ボールが入っているかを返す
    //Getter
    public bool IsHolding()
    {
        return isHolding;
    }

    //isTriggerのコライダーに何かが侵入してきたときに動くメソッド
    //引数：侵入してきたcollider
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == targetTag)
        {
            isHolding = true;
        }
    }

    //isTriggerなコライダーから物が出て行ったときに動くメソッド
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == targetTag)
        {
            isHolding = false;
        }
    }
    //isTriggerなコライダーに物が接触している間中動くメソッド
    void OnTriggerStay(Collider other)
    {
        //コライダに触れているオブジェクトのRigidbodyコンポーネントを取得
        Rigidbody r = other.gameObject.GetComponent<Rigidbody>();

        //ボールがどの方向にあるかを計算
        Vector3 direction = 
        other.gameObject.transform.position 
        - transform.position; //いきなりtransformはこのスクリプトがアタッチされているオブジェクトのトランスフォームを指す
        direction.Normalize();

        //タグに応じてボールに力を加える
        if (other.gameObject.tag == targetTag)
        {
            //中心地点でボールを止めるため速度を減速させる
            r.velocity *= 0.9f;
            r.AddForce(direction * -20.0f, ForceMode.Acceleration);
        }
        else
        {
            r.AddForce(direction * 80.0f, ForceMode.Acceleration);
        }
    }
}
