using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public float swingForce = 4f;
    public float speed = 1f;
    public Vector2 ropeHook;
    public bool isSwinging;
    public bool groundCheck;
    SpriteRenderer _graphic;
    Rigidbody2D _rb;
    Animator _anim;
    float _verticalMomentum;
    float _horizontalMomentum;
	public GameObject[] arms;

    void Awake()
    {
        _graphic = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
	{
		var aimDirection = GetPlayerForward();
		for (int i = 0; i < arms.Length; i++) arms[i].transform.right = aimDirection - arms[i].transform.position;

		_horizontalMomentum = Input.GetAxis("Horizontal");
		var halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
		groundCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);
	}

    void FixedUpdate()
    {
        if (_horizontalMomentum < 0f || _horizontalMomentum > 0f)
        {
            _graphic.flipX = _horizontalMomentum < 0f;
            if (isSwinging)
            {

                // Get normalized direction vector from player to the hook point
                var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

                // Inverse the direction to get a perpendicular direction
                Vector2 perpendicularDirection;
                if (_horizontalMomentum < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                    Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                    Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                _rb.AddForce(force, ForceMode2D.Force);
            }
            else
            {
                if (groundCheck)
                {
                    var groundForce = speed * 2f;
                    _rb.AddForce(new Vector2((_horizontalMomentum * groundForce - _rb.velocity.x) * groundForce, 0));
                    _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y);
                }
            }
        }
    }

    public void Die()
    {
        //ToDo lives
        SoundMenu.Instance.PlayClip(SoundMenu.Audios.DEATH);

        UIManager.Instance.Endgame();
        var cam = Camera.main.transform.parent = null;
        gameObject.SetActive(false);
    }

	public Vector3 GetPlayerForward()
	{
		var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
		var facingDirection = worldMousePosition - transform.position;
		var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
		if (aimAngle < 0f) aimAngle = Mathf.PI * 2 + aimAngle;

		var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
		var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

		return new Vector3(x, y, 0);
	}
}
