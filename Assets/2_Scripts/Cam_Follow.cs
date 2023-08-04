using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Cam_Follow : MonoBehaviourPun
{
    //public Vector3 offset;
    //public Transform cameraArm;

    public Transform objectFollow;
    public float followSpeed = 10f;
    public float sensitivety = 100f;
    public float clampAngle = 70f;

    public float rotX;
    public float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;



    public bool toggleCameraRotation;


    private void Start()
    {
        if (!photonView.IsMine)
        {
            this.gameObject.SetActive(false);
        }

        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivety * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivety * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, minDistance - clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectFollow.position, followSpeed * Time.deltaTime);
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if(Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized
            * finalDistance, Time.deltaTime * smoothness);
    }
}
