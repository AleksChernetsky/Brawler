using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : HealthBar
{
    //[SerializeField] private Image ammoBar;
    private WeaponMain _weaponMain;
    //private Animator _anim;

    private void Start()
    {
        //_anim = GetComponent<Animator>();
        _weaponMain = GetComponent<WeaponMain>();
        _weaponMain.OnShootEvent += ChangeAmmoBar;
    }
    private void ChangeAmmoBar()
    {
        _progressBar.fillAmount = _weaponMain.CurrentAmmo;
        _anim.SetTrigger("TakeHit");
    }
    public override void DestroyBar() => base.DestroyBar();

    private void OnDestroy()
    {        
        _weaponMain.OnShootEvent -= ChangeAmmoBar;
        _vitalitySystem.OnDeath -= DestroyBar;
    }
}