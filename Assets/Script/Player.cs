﻿using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public GameObject parctefek;
    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }


    void Update()
    {
        if (!photonView.IsMine) return;

        PlayerMoveController();
    }

    private void PlayerMoveController()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            Vector3 dir = hit.point - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir * Time.deltaTime * 2f);
            Debug.DrawLine(transform.position, hit.point);

        }
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 20f;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * 20f;
        transform.Translate(x, 0, y);


        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray2, out hit))

                Debug.Log(hit.transform.gameObject.tag);

            PhotonNetwork.Instantiate("FireVFX", transform.position, Quaternion.Euler(-90f,transform.eulerAngles.y,0f));

        }
    }


}
