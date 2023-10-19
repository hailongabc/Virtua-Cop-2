using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GunType
{
    pistol, akm, yellowGun
}
public class GunInGame : MonoBehaviour
{
    public GunType guntype;
    public float CurrentAmmo;
    public float AmmoLeft;
    public Transform PointBullet;
    public bool IsReadyFire = false;
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ParticleShoot;
    [SerializeField]
    private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private ParticleSystem ImpactParticleSystemEnemy;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private LayerMask Mask;
    [SerializeField]
    private LayerMask MaskEnemy;
    [SerializeField]
    private float ShootDelay = 0.5f;
    private float LastShootTime;
    public Gun DataGun;
    public bool isOutOfBullet = false;
    public bool isReload = false;
    private float value = 0;
    private void Awake()
    {
        CurrentAmmo = DataGun.TotalBullet;
        AmmoLeft = DataGun.MaxBullet;
    }


    public void Init(float CurrentAmmo, float AmmoLeft)
    {
        this.CurrentAmmo = CurrentAmmo;
        this.AmmoLeft = AmmoLeft;
        PlayerUI.ins.txtBullet.gameObject.SetActive(true);
        PlayerUI.ins.txtBullet.text = CurrentAmmo + "/" + AmmoLeft;
    }
    public void DecreaseBullet()
    {
        CurrentAmmo--;
        PlayerUI.ins.txtBullet.text = CurrentAmmo + "/" + AmmoLeft;
    }

    public bool CheckBullet()
    {
        if (CurrentAmmo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public IEnumerator ReloadBullet()
    {
        StartCoroutine(ChangeValueSlider());
        PlayerUI.ins.Reloading.gameObject.SetActive(true);
        //PlayerUI.ins.Reloading.value = 
        isOutOfBullet = true;
        isReload = true;
        yield return new WaitForSeconds(DataGun.TimeReload);
        PlayerUI.ins.Reloading.gameObject.SetActive(false);
        while (CurrentAmmo < DataGun.TotalBullet)
        {
            if (AmmoLeft > 0)
            {
                CurrentAmmo++;
                AmmoLeft--;
            }
            else
            {
                break;
            }
        }
        PlayerUI.ins.txtBullet.text = CurrentAmmo + "/" + AmmoLeft;
        isOutOfBullet = false;
        isReload = false;
    }
    public IEnumerator ChangeValueSlider()
    {
        float elapsedTime = 0;

        while (elapsedTime < DataGun.TimeReload)
        {
            value = Mathf.Lerp(0, 1, elapsedTime / DataGun.TimeReload); // Sử dụng hàm Mathf.Lerp để thay đổi giá trị dần dần
            elapsedTime += Time.deltaTime;
            PlayerUI.ins.Reloading.value = value;
            yield return null;
        }

        value = 1; // Đảm bảo giá trị là 1 sau khi kết thúc
    }
    public IEnumerator Shoot()
    {
        if (!IsReadyFire)
        {
            Vector3 direction = GetDirection();
            GameObject bullet = Instantiate(GameManager.ins.bullet.gameObject, PointBullet.position, GameManager.ins.PlayerCam.transform.rotation);
            bullet.GetComponent<Bullet>().damage = DataGun.damage;
            IsReadyFire = true;
            ParticleShoot.Play();
            DecreaseBullet();
            /* if (Physics.Raycast(t_cam.position, t_cam.forward, out hit, 1000f, canBeShot))
             {
                 GameObject NewHole = Instantiate(BulleHolePf, (hit.point + hit.normal), Quaternion.identity);

                 NewHole.transform.LookAt(hit.point + hit.normal);

                 Destroy(NewHole, 10f);
             }
     */
            //giat len
            transform.Rotate(-DataGun.Recoil, 0, 0);

            //giat ve sau
            transform.position -= transform.forward * DataGun.KickBack;
            //delay
            yield return new WaitForSeconds(DataGun.FireRate);
            IsReadyFire = false;
        }
        //RaycastHit hit = new RaycastHit();
    }

    public void Shoot2()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
            ParticleShoot.Play();

            Vector3 direction = GetDirection();

            if (Physics.Raycast(PointBullet.position, direction, out RaycastHit hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, PointBullet.position, GameManager.ins.PlayerCam.transform.rotation);
                DecreaseBullet();
                StartCoroutine(SpawnTrail(trail, hit));
                transform.Rotate(-DataGun.Recoil, 0, 0);
                transform.position -= transform.forward * DataGun.KickBack;
                LastShootTime = Time.time;
            }

            if (Physics.Raycast(PointBullet.position, PointBullet.forward, out RaycastHit hit1, float.MaxValue, MaskEnemy))
            {
                TrailRenderer trail = Instantiate(BulletTrail, PointBullet.position, Quaternion.identity);
                DecreaseBullet();

                StartCoroutine(SpawnTrailEnemy(trail, hit1));
                transform.Rotate(-DataGun.Recoil, 0, 0);
                transform.position -= transform.forward * DataGun.KickBack;
                LastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
                );
            direction.Normalize();
        }
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
           Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;

        }
        Trail.transform.position = Hit.point;
        Instantiate(ImpactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }

    private IEnumerator SpawnTrailEnemy(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;

        }
        Trail.transform.position = Hit.point;
        Instantiate(ImpactParticleSystemEnemy, Hit.point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }

}
