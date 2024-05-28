using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class KillEventPanel : MonoBehaviour
{
    [SerializeField] private Image _killerIcon;
    [SerializeField] private Image _gunIcon;
    [SerializeField] private Image _victimIcon;

    private Sprite MachineGun, Shotgun, SMG, SniperRifle, Fist;
    private Sprite Warrok, Vanguard, Ely, Pumpkinhulk, Mutant;

    public int PositionPoint { get; set; }
    public bool IsFreeToRelease { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsReleased { get; private set; }

    private void Awake()
    {
        MachineGun = Resources.Load<Sprite>("MachineGunIcon");
        Shotgun = Resources.Load<Sprite>("ShotgunIcon");
        SMG = Resources.Load<Sprite>("SMGIcon");
        SniperRifle = Resources.Load<Sprite>("SniperRifleIcon");
        Fist = Resources.Load<Sprite>("FistIcon");

        Warrok = Resources.Load<Sprite>("WarrokIcon");
        Vanguard = Resources.Load<Sprite>("VanguardIcon");
        Ely = Resources.Load<Sprite>("ElyIcon");
        Pumpkinhulk = Resources.Load<Sprite>("PumpkinhulkIcon");
        Mutant = Resources.Load<Sprite>("MutantIcon");
    }
    private void Start()
    {
        IsFreeToRelease = true;
    }
    public IEnumerator ShowPanel(RectTransform spawnPosition, RectTransform targetPosition)
    {
        float time = 0;
        IsFreeToRelease = false;
        while (transform.localPosition.x != targetPosition.localPosition.x)
        {
            transform.localPosition = Vector3.Lerp(spawnPosition.localPosition, targetPosition.localPosition, time);
            time += Time.deltaTime;
            yield return null;
        }
        IsReleased = true;
        yield return new WaitForSeconds(3);
        StartCoroutine(HidePanel(spawnPosition));
    }
    public IEnumerator MovePanel(RectTransform targetPosition)
    {
        yield return new WaitUntil(() => !IsMoving);
        float time = 0;
        IsMoving = true;
        while (transform.localPosition.y != targetPosition.localPosition.y)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition.localPosition, time);
            time += Time.deltaTime;
            yield return null;
        }
        IsMoving = false;
    }
    public IEnumerator HidePanel(RectTransform spawnPosition)
    {
        yield return new WaitUntil(() => !IsMoving);
        IsReleased = false;
        float time = 0;
        Vector3 hideDirection = new Vector3(spawnPosition.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        while (transform.localPosition.x != spawnPosition.localPosition.x)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, hideDirection, time);
            time += Time.deltaTime;
            yield return null;
        }
        IsFreeToRelease = true;
        PositionPoint = 0;
    }
    public void SetImages(WeaponType weaponIcon, Sprite victimIcon)
    {
        switch (weaponIcon)
        {
            case WeaponType.Shotgun:
                _killerIcon.sprite = Warrok;
                _gunIcon.sprite = Shotgun;
                break;
            case WeaponType.MachineGun:
                _killerIcon.sprite = Pumpkinhulk;
                _gunIcon.sprite = MachineGun;
                break;
            case WeaponType.SniperRifle:
                _killerIcon.sprite = Vanguard;
                _gunIcon.sprite = SniperRifle;
                break;
            case WeaponType.SMG:
                _killerIcon.sprite = Ely;
                _gunIcon.sprite = SMG;
                break;
            case WeaponType.Fist:
                _killerIcon.sprite = Mutant;
                _gunIcon.sprite = Fist;
                break;
            default:
                break;
        }
        _victimIcon.sprite = victimIcon;
    }
}
