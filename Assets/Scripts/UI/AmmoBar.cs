using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : HealthBar
{
    private WeaponMain _weaponMain;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _weaponMain = GetComponentInParent<WeaponMain>();
        _vitalitySystem = GetComponentInParent<VitalitySystem>();

        _weaponMain.OnShoot += ChangeAmmoBar;
        _weaponMain.OnReload += ChangeAmmoBar;

        _vitalitySystem.OnDeath += DestroyBar;
    }
    private void ChangeAmmoBar()
    {
        _progressBar.fillAmount = _weaponMain.CurrentAmmo;
        _anim.SetTrigger("Reload");
    }
    public override void DestroyBar()
    {
        if (gameObject != null)
        {
            base.DestroyBar();
        }
    }

    private void OnDestroy()
    {
        _weaponMain.OnShoot -= ChangeAmmoBar;
        _weaponMain.OnReload -= ChangeAmmoBar;
    }
}