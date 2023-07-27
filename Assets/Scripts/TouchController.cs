using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Material materialRed;
    // Start is called before the first frame update

    //Raycast
    RaycastHit hit;

    public int damage;
    public float timeBetweenShooting, reloadTime;
    public int magazineSize, bulletPerTap;
    int bulletsLeft, bulletsShot;

    public bool allowButtonHold;

    bool shooting, readyToShoot, reloading;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;

    //Reference
    public Transform attachPoint;

    void Start()
    {
        bulletsLeft = 10;
    }

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        MyInput();
    }
    void DrawRay()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                hit.transform.GetChild(1).GetComponent<Renderer>().material = materialRed;
            }
        }
    }

    void MyInput()
    {
        if (allowButtonHold)
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("hold 1 time");
            }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("hold");
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && bulletsLeft < magazineSize && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Debug.Log("vo day chua");
            bulletsShot = bulletPerTap;
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            hit.transform.GetChild(1).GetComponent<Renderer>().material = materialRed;
        }


        Instantiate(muzzleFlash, attachPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        //DrawRay();
        Invoke("ResetShot", timeBetweenShooting);
        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
