using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;

    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce;

    [SerializeField] private PhotonView _photonView; // Serialized field to store the PhotonView reference

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>(); // Get the PhotonView component on this GameObject
    }

    private void Start()
    {
        if (_photonView.IsMine)
        {
            PlayerCamera.SetActive(true);
        }

        if (PlayerNameText != null)
        {
            PlayerNameText.text = _photonView.Owner.NickName;
        }
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * MoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
        {
            sr.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            sr.flipX = false;
        }
    }
}
