using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーのアクションを管理するクラス</summary>
    public class PlayerActionView : MonoBehaviour
    {
        [SerializeField, Tooltip("地面のレイヤー")] LayerMask _groundLayer;
        [SerializeField, Tooltip("接地判定をする距離")] float _groundCheckDistance = -0.6f;
        Rigidbody2D _rb2d;
        Animator _animator;
        InputManager _inputManager;
        GameFlowManager _gameFlowManager;
        RaycastHit2D _groundHit;
        Vector3 _move;
        Vector3 _rayStart, _rayEnd;

        //動きの処理をかけ
    }
}
