using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Lava : MonoBehaviour
{
    public float speed = 1;
    float _speed;
    public float speedIncrementDelay = 1f;
    public float increment = .1f;
    public float startDelay = 2f;
    public Camera cam;

    void Start()
    {
		if (!cam) cam = Camera.main;
        Invoke("StartMoving", startDelay);
    }

    void StartMoving()
    {
        _speed = speed;
    }

    IEnumerator AddSpeed()
    {
        var instruction = new WaitForSeconds(speedIncrementDelay);

        while (true)
        {
            yield return instruction;
            _speed += increment;
        }
    }

    void Update()
    {
        transform.position += Vector3.right * _speed * Time.deltaTime;
        ConchaDeTuMadreFrustrum();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<Player>().Die();
        }
    }

    void ConchaDeTuMadreFrustrum()
    {
        // boundsTarget is the center of the camera's frustum, in world coordinates:
        Vector3 camPosition = cam.transform.position;
        Vector3 normCamForward = Vector3.Normalize(cam.transform.forward);
        float boundsDistance = (cam.farClipPlane - cam.nearClipPlane) / 2 + cam.nearClipPlane;
        Vector3 boundsTarget = camPosition + (normCamForward * boundsDistance);

        // The game object's transform will be applied to the mesh's bounds for frustum culling checking.
        // We need to "undo" this transform by making the boundsTarget relative to the game object's transform:
        Vector3 realtiveBoundsTarget = this.transform.InverseTransformPoint(boundsTarget);

        // Set the bounds of the mesh to be a 1x1x1 cube (actually doesn't matter what the size is)
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.bounds = new Bounds(realtiveBoundsTarget, Vector3.one);
    }
}
