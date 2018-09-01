using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeSystem : MonoBehaviour
{
    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    public float climbSpeed = 3f;
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    public Player playerMovement;
    public float ropeMaxCastDistance = 20f;

    bool _ropeAttached;
    Vector2 _playerPosition;
    List<Vector2> _ropePositions = new List<Vector2>();
    Rigidbody2D _ropeHingeAnchorRb;
    bool _distanceSet;
    bool _isColliding;
    Dictionary<Vector2, int> _wrapPointsLookup = new Dictionary<Vector2, int>();
    SpriteRenderer _ropeHingeAnchorSprite;
    Camera _cam;

    void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
        ropeJoint.enabled = false;
        _playerPosition = transform.position;
        _ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        _ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Figures out the closest Polygon collider vertex to a specified Raycast2D hit point in order to assist in 'rope wrapping'
    /// </summary>
    /// <param name="hit">The raycast2d hit</param>
    /// <param name="polyCollider">the reference polygon collider 2D</param>
    /// <returns></returns>
    private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
    {
        // Transform polygoncolliderpoints to world space (default is local)
        var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
            position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
            position => polyCollider.transform.TransformPoint(position));

        var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
        return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        var worldMousePosition = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        _playerPosition = transform.position;

        if (!_ropeAttached)
        {
            SetCrosshairPosition(aimAngle);
            playerMovement.isSwinging = false;
        }
        else
        {
            playerMovement.isSwinging = true;
            playerMovement.ropeHook = _ropePositions.Last();
            crosshairSprite.enabled = false;

            // Wrap rope around points of colliders if there are raycast collisions between player position and their closest current wrap around collider / angle point.
            if (_ropePositions.Count > 0)
            {
                var lastRopePoint = _ropePositions.Last();
                var playerToCurrentNextHit = Physics2D.Raycast(_playerPosition, (lastRopePoint - _playerPosition).normalized, Vector2.Distance(_playerPosition, lastRopePoint) - 0.1f, ropeLayerMask);
                if (playerToCurrentNextHit)
                {
                    var colliderWithVertices = playerToCurrentNextHit.collider as PolygonCollider2D;
                    if (colliderWithVertices != null)
                    {
                        var closestPointToHit = GetClosestColliderPointFromRaycastHit(playerToCurrentNextHit, colliderWithVertices);
                        if (_wrapPointsLookup.ContainsKey(closestPointToHit))
                        {
                            // Reset the rope if it wraps around an 'already wrapped' position.
                            ResetRope();
                            return;
                        }

                        _ropePositions.Add(closestPointToHit);
                        _wrapPointsLookup.Add(closestPointToHit, 0);
                        _distanceSet = false;
                    }
                }
            }
        }

        UpdateRopePositions();
        HandleRopeLength();
        HandleInput(aimDirection);
        HandleRopeUnwrap();
    }

    /// <summary>
    /// Handles input within the RopeSystem component
    /// </summary>
    /// <param name="aimDirection">The current direction for aiming based on mouse position</param>
    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetMouseButton(0))
        {
            if (_ropeAttached) return;
            ropeRenderer.enabled = true;

            var hit = Physics2D.Raycast(_playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);
            if (hit.collider != null)
            {
                _ropeAttached = true;
                if (!_ropePositions.Contains(hit.point))
                {
                    // Jump slightly to distance the player a little from the ground after grappling to something.
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    _ropePositions.Add(hit.point);
                    _wrapPointsLookup.Add(hit.point, 0);
                    ropeJoint.distance = Vector2.Distance(_playerPosition, hit.point);
                    ropeJoint.enabled = true;
                    _ropeHingeAnchorSprite.enabled = true;
                }
            }
            else
            {
                ropeRenderer.enabled = false;
                _ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ResetRope();
        }
    }

    /// <summary>
    /// Resets the rope in terms of gameplay, visual, and supporting variable values.
    /// </summary>
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        _ropeAttached = false;
        playerMovement.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        _ropePositions.Clear();
        _wrapPointsLookup.Clear();
        _ropeHingeAnchorSprite.enabled = false;
    }

    /// <summary>
    /// Move the aiming crosshair based on aim angle
    /// </summary>
    /// <param name="aimAngle">The mouse aiming angle</param>
    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }

    /// <summary>
    /// Retracts or extends the 'rope'
    /// </summary>
    private void HandleRopeLength()
    {
        if (Input.GetAxis("Vertical") >= 1f && _ropeAttached && !_isColliding)
        {
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (Input.GetAxis("Vertical") < 0f && _ropeAttached)
        {
            ropeJoint.distance += Time.deltaTime * climbSpeed;
        }
    }

    /// <summary>
    /// Handles updating of the rope hinge and anchor points based on objects the rope can wrap around. These must be PolygonCollider2D physics objects.
    /// </summary>
    private void UpdateRopePositions()
    {
        if (_ropeAttached)
        {
            ropeRenderer.positionCount = _ropePositions.Count + 1;

            for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
            {
                if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
                {
                    ropeRenderer.SetPosition(i, _ropePositions[i]);

                    // Set the rope anchor to the 2nd to last rope position (where the current hinge/anchor should be) or if only 1 rope position then set that one to anchor point
                    if (i == _ropePositions.Count - 1 || _ropePositions.Count == 1)
                    {
                        if (_ropePositions.Count == 1)
                        {
                            var ropePosition = _ropePositions[_ropePositions.Count - 1];
                            _ropeHingeAnchorRb.transform.position = ropePosition;
                            if (!_distanceSet)
                            {
                                ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                                _distanceSet = true;
                            }
                        }
                        else
                        {
                            var ropePosition = _ropePositions[_ropePositions.Count - 1];
                            _ropeHingeAnchorRb.transform.position = ropePosition;
                            if (!_distanceSet)
                            {
                                ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                                _distanceSet = true;
                            }
                        }
                    }
                    else if (i - 1 == _ropePositions.IndexOf(_ropePositions.Last()))
                    {
                        // if the line renderer position we're on is meant for the current anchor/hinge point...
                        var ropePosition = _ropePositions.Last();
                        _ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!_distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            _distanceSet = true;
                        }
                    }
                }
                else
                {
                    // Player position
                    ropeRenderer.SetPosition(i, transform.position);
                }
            }
        }
    }

    private void HandleRopeUnwrap()
    {
        if (_ropePositions.Count <= 1)
        {
            return;
        }

        // Hinge = next point up from the player position
        // Anchor = next point up from the Hinge
        // Hinge Angle = Angle between anchor and hinge
        // Player Angle = Angle between anchor and player

        // 1
        var anchorIndex = _ropePositions.Count - 2;
        // 2
        var hingeIndex = _ropePositions.Count - 1;
        // 3
        var anchorPosition = _ropePositions[anchorIndex];
        // 4
        var hingePosition = _ropePositions[hingeIndex];
        // 5
        var hingeDir = hingePosition - anchorPosition;
        // 6
        var hingeAngle = Vector2.Angle(anchorPosition, hingeDir);
        // 7
        var playerDir = _playerPosition - anchorPosition;
        // 8
        var playerAngle = Vector2.Angle(anchorPosition, playerDir);

        if (!_wrapPointsLookup.ContainsKey(hingePosition))
        {
            Debug.LogError("We were not tracking hingePosition (" + hingePosition + ") in the look up dictionary.");
            return;
        }

        if (playerAngle < hingeAngle)
        {
            // 1
            if (_wrapPointsLookup[hingePosition] == 1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }

            // 2
            _wrapPointsLookup[hingePosition] = -1;
        }
        else
        {
            // 3
            if (_wrapPointsLookup[hingePosition] == -1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }

            // 4
            _wrapPointsLookup[hingePosition] = 1;
        }
    }

    private void UnwrapRopePosition(int anchorIndex, int hingeIndex)
    {
        // 1
        var newAnchorPosition = _ropePositions[anchorIndex];
        _wrapPointsLookup.Remove(_ropePositions[hingeIndex]);
        _ropePositions.RemoveAt(hingeIndex);

        // 2
        _ropeHingeAnchorRb.transform.position = newAnchorPosition;
        _distanceSet = false;

        // Set new rope distance joint distance for anchor position if not yet set.
        if (_distanceSet)
        {
            return;
        }
        ropeJoint.distance = Vector2.Distance(transform.position, newAnchorPosition);
        _distanceSet = true;
    }

    void OnTriggerStay2D(Collider2D colliderStay)
    {
        _isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        _isColliding = false;
    }
}
