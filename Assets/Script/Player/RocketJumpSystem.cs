using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RocketJumpSystem : MonoBehaviour
{
    public GameObject rocketPrefab;
    public float fireForce = 5, cooldown = .65f, spawnDistance = 1.5f;
    private float _cooldown;
    private bool _isCoolingDown { get { return _cooldown != 0; } }
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (!rocketPrefab) rocketPrefab = (GameObject)Resources.Load("Prefabs/Rocket");
    }

    void Update()
    {
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f) aimAngle = Mathf.PI * 2 + aimAngle;
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

        HandleInput(aimDirection);
        if (_isCoolingDown)
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown < 0) _cooldown = 0;
        }
    }

    private void HandleInput(Vector2 aimDirection)
    {
        if (_isCoolingDown || !Input.GetMouseButtonDown(1)) return;

        var go = (GameObject)Instantiate(rocketPrefab, (Vector2)transform.position + aimDirection.normalized * spawnDistance, Quaternion.identity);
        go.transform.right = aimDirection;
        _rigidbody.AddForce(-aimDirection * fireForce, ForceMode2D.Impulse);
        _cooldown = cooldown;
        SoundMenu.Instance.PlayClip(SoundMenu.Audios.COCONUT);
    }
}
