using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    private static readonly int Ability1Hash = Animator.StringToHash("ability1");
    private static readonly int ReloadHash = Animator.StringToHash("reload");
    private static readonly int ShootHash = Animator.StringToHash("shoot");

    //fix the shootDir{
    //[Header("Input")]
    [Header("Bullet settings")]
    public float ShootForce;
    public float UpwardForce, AbilityForce;
    [Header("Gun Stats")]
    public float TimeBetweenShooting, Spread, ReloadTime, TimeBetweenBullets;
    public int MagSize, BulletsPerTap;
    public int BulletsLeft { get; set; }
    public int BulletsAvalible;
    int BulletsShot;
    bool ReadyToShoot;
    public bool Reloading { get; set; }
    public bool AutoReload = true;

    [Header("Ability Stats")]
    public float TimeBetweenAbilities;
    public float saveCoolDown;
    public int Ability1Bullets;
    bool  ReadyToActivate;
    public bool SaveCoolDownActive { get; set; }

    [Header("Referance Objects")]
    public Transform Cam;
    public Transform AttackPoint;
    public TextMeshProUGUI CoolDownCounter;

    [Header("Debuging")]
    public bool AllowInvoke = true;
    public bool AllowInvokeAbility = true;
    public bool animationActive;
    public bool animationActiveAbility;
    public Animator anim;

    [Header("Graphics")]
    public GameObject MuzzleFlash;
    public TextMeshProUGUI AmmoDisplay;

    public bool Test;
    public void Awake()
    {
        BulletsLeft = MagSize;
        ReadyToShoot = true;
        ReadyToActivate = true;
    }
    void Start()
    {
        //anim = GameObject.FindGameObjectWithTag("right").GetComponent<Animator>();
        saveCoolDown = TimeBetweenAbilities;
    }

    void Update()
    {
        if(Test)
        {
            CallShoot();
            Shoot(ShootForce);
            Test = false;
        }

        if(CoolDownCounter != null)
        CoolDownCounter.SetText(String.Format("{0:0.00}", saveCoolDown));
        if (AutoReload)
            if (ReadyToShoot  && !Reloading && BulletsLeft <= 0) Reload();
        if (AmmoDisplay != null)
            AmmoDisplay.SetText(BulletsLeft / BulletsPerTap + " / " + BulletsAvalible / BulletsPerTap);
        if (SaveCoolDownActive)
            saveCoolDown -= Time.deltaTime;
    }
    public void CallShoot()
    {
        if (ReadyToShoot && !Reloading && BulletsLeft > 0)
        {
            BulletsShot = 0;
            ReadyToShoot = false;
            Shoot(ShootForce);
            if (AllowInvoke)
            {
                //Invoke("ResetShot", 3f); calls function after 3 seconds
                Invoke(nameof(ResetShot), TimeBetweenShooting);
                AllowInvoke = false;
            }
            if (BulletsShot < BulletsPerTap && BulletsLeft > 0)
                Invoke(nameof(Shoot), TimeBetweenBullets);
            if (anim != null && !animationActive)
            {
                anim.SetTrigger(ShootHash);
                animationActive = true;
            }
        }
    }
    public void ShotgunShot()
    {
        if (ReadyToActivate && !Reloading && BulletsLeft > Ability1Bullets)
        {
            BulletsShot = 0;
            ReadyToActivate = false;
            Shoot(AbilityForce);
            if (anim != null && !animationActiveAbility)
            {
                anim.SetTrigger(Ability1Hash);
                animationActiveAbility = true;
            }
            if (AllowInvokeAbility)
            {
                if (saveCoolDown == TimeBetweenAbilities)
                {
                    //Invoke("ResetShot", 3f); calls function after 3 seconds
                    Invoke(nameof(ResetAbility), TimeBetweenAbilities);
                    AllowInvokeAbility = false;
                    SaveCoolDownActive = true;
                }
            }
            if (BulletsShot < Ability1Bullets && BulletsLeft > 0)
                Invoke(nameof(ShotgunShot), TimeBetweenBullets);
        }
    }

    public GameObject currentBullet;
    public void Shoot(float force)
    {
        Ray ray = new(Cam.position, AttackPoint.forward);

        Vector3 targetPoint;
        if (Physics.Raycast(Cam.position, AttackPoint.forward , out RaycastHit hit)) targetPoint = hit.point;
        else targetPoint = ray.GetPoint(75);
        Vector3 directionWithoutSpread = targetPoint - AttackPoint.position;
        float x = Random.Range(-Spread, Spread);
        float y = Random.Range(-Spread, Spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
        currentBullet = ObjectPooling.SharedInstance.GetPooledObject();
        if (currentBullet != null)
        {
            //currentBullet.SetActive(true);
            currentBullet.GetComponent<Rigidbody>().position = AttackPoint.position;
            currentBullet.GetComponent<Rigidbody>().rotation = AttackPoint.rotation;
            //currentBullet.transform.rotation = AttackPoint.rotation;
            currentBullet.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            currentBullet.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            currentBullet.GetComponent<MeshRenderer>().enabled = true;
            currentBullet.GetComponent<SphereCollider>().enabled = true;
            currentBullet.transform.forward = directionWithSpread.normalized;
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * force, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(Cam.transform.up * UpwardForce, ForceMode.Impulse);
        }
        if (MuzzleFlash != null)
            Instantiate(MuzzleFlash, AttackPoint.position, Quaternion.identity);
        BulletsLeft--;
        BulletsShot++;
        //currentBullet = null;
    }
    private void ResetAbility()
    {
        SaveCoolDownActive = false;
        saveCoolDown = TimeBetweenAbilities;
        ReadyToActivate = true;
        AllowInvokeAbility = true;
        animationActiveAbility = false;
    }
    private void ResetShot()
    {
        ReadyToShoot = true;
        AllowInvoke = true;
        animationActive = false;
    }
    private void Reload()
    {
        if(anim != null)
        anim.SetTrigger(ReloadHash);
        Reloading = true;
        if (Reloading) Invoke(nameof(ReloadFinished), ReloadTime);
    }
    private void ReloadFinished()
    {
        int mag = MagSize;
        int bulletsLeft = BulletsLeft;
        int bulletsAvalible = BulletsAvalible;
        int AmmoToBeReloaded = bulletsLeft - mag;
        int e = bulletsAvalible - -AmmoToBeReloaded;
        if (BulletsAvalible >= MagSize)
        {
            BulletsLeft = MagSize;
            BulletsAvalible -= -AmmoToBeReloaded;
        }
        else
        {
            //get the bullets left then only add an amout that will make it equal to at max MagSize
            BulletsLeft += -AmmoToBeReloaded;
            BulletsAvalible -= -AmmoToBeReloaded;
            if (e < 0)
            {
                BulletsAvalible += -e;
                BulletsLeft += e;
            }
        }
        Reloading = false;
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 targetPoint;
        Ray ray = new Ray(Cam.position, AttackPoint.forward);
        if (Physics.Raycast(Cam.position, AttackPoint.forward, out RaycastHit hit)) targetPoint = hit.point;
        else targetPoint = ray.GetPoint(75);
        Gizmos.DrawLine(AttackPoint.position, targetPoint);
    }
}
