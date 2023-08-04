using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ThrowCount
{
    public float reloadTime = 0;
    public int curCnt = 0;
    public int throwCnt = 1;
}

public class GrenadeThrower : MonoBehaviourPun
{
    ThrowCount cnt;

    [Header("Scene References")]
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private Rigidbody Grenade;
    [SerializeField]
    GameObject GrenadePrefab;
    [SerializeField]
    private LineRenderer LineRenderer;
    [SerializeField]
    private Transform ReleasePosition;
    [Header("Grenade Controls")]
    [SerializeField]
    [Range(1, 100)]
    private float ThrowStrength = 500f;
    [SerializeField]
    [Range(1, 10)]
    private float ExplosionDelay = 5f;
    [Header("Display Controls")]
    [SerializeField]
    [Range(10, 1000)]
    private int LinePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float TimeBetweenPoints = 0.1f;

    public AudioSource audioSource;

    private LayerMask GrenadeCollisionMask;

    private void Awake()
    {
        int grenadeLayer = Grenade.gameObject.layer;
        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(grenadeLayer, i))
            {
                GrenadeCollisionMask |= 1 << i; // magic
            }
        }
    }

    void Start()
    {
        cnt = new ThrowCount();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetButton("Fire2"))
            {
                ReleasePosition.forward = Camera.main.transform.forward;
                Camera.transform.position = ReleasePosition.position;
                DrawProjection();

                if (Input.GetButtonDown("Fire1") && cnt.curCnt != cnt.throwCnt)
                {
                    ReleaseGrenade();
                    ++cnt.curCnt;
                    audioSource.Play();
                }
            }
            else
            {
                LineRenderer.enabled = false;
            }

            if (cnt.curCnt == cnt.throwCnt)
            {
                cnt.reloadTime += Time.deltaTime;
                if (cnt.reloadTime > 2f)
                {
                    cnt.curCnt = 0;
                    cnt.reloadTime = 0;
                }
            }
        }
    }

    private void DrawProjection()
    {
        LineRenderer.enabled = true;
        LineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
        Vector3 startPosition = ReleasePosition.position;
        Vector3 startVelocity = ThrowStrength * Camera.transform.forward / Grenade.mass;
        int i = 0;
        LineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            LineRenderer.SetPosition(i, point);

            Vector3 lastPosition = LineRenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPosition,
                (point - lastPosition).normalized,
                out RaycastHit hit,
                (point - lastPosition).magnitude,
                GrenadeCollisionMask))
            {
                LineRenderer.SetPosition(i, hit.point);
                LineRenderer.positionCount = i + 1;
                return;
            }
        }
    }

    private void ReleaseGrenade()
    {
        GameObject InstantBomb = PhotonNetwork.Instantiate("Bomb_Choo", ReleasePosition.position, ReleasePosition.rotation);
        //Rigidbody rb = InstantBomb.GetComponent<Rigidbody>();
        //rb.velocity = ReleasePosition.forward * ThrowStrength;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "CountPlusItem")
        {
            cnt.throwCnt++;
        }
    }
}