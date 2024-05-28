using System.Collections;

using TMPro;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private JoysticksHandler _joysticksHandler;

    [Header("Objects")]
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _combatJoystick;
    [SerializeField] private GameObject _combatTutorial;
    [SerializeField] private GameObject _killTutorial;
    [SerializeField] private GameObject _tutorialCompleteState;

    [Header("Animations")]
    [SerializeField] private Animator _movementAnim;
    [SerializeField] private Animator _combatAnim;
    [SerializeField] private Animator _releaseOnEdgeAnim;
    [SerializeField] private Animator _releaseInCenterAnim;

    private bool _isReleasedOnEdge = false;
    private bool _isReleasedInCenter = false;

    private void Start()
    {
        _joysticksHandler.OnJoystickUpToShoot += ReleaseJoystickToShoot;
        _joysticksHandler.OnJoystickUpToRelease += ReleaseJoystickToReset;
        StartCoroutine(GameTutorial());

        GlobalEventsManager.OnPauseState?.Invoke();
    }
    private IEnumerator GameTutorial()
    {
        yield return new WaitUntil(() => _joysticksHandler.MoveDirection.magnitude > 0);
        StartCoroutine(ObjectScaler(_movementAnim.gameObject));
        _combatJoystick.SetActive(true);
        _combatAnim.SetTrigger("StartAnim");

        yield return new WaitUntil(() => _joysticksHandler.RotateDirection.magnitude > 0);
        StartCoroutine(ObjectScaler(_combatAnim.gameObject));
        _combatTutorial.SetActive(true);
        _releaseOnEdgeAnim.SetTrigger("StartAnim");

        yield return new WaitUntil(() => _isReleasedOnEdge);
        StartCoroutine(ObjectScaler(_releaseOnEdgeAnim.gameObject));
        _isReleasedInCenter = false;
        _releaseInCenterAnim.SetTrigger("StartAnim");

        yield return new WaitUntil(() => _isReleasedInCenter);
        StartCoroutine(ObjectScaler(_releaseInCenterAnim.gameObject));

        GlobalEventsManager.OnBattleState?.Invoke();
        _target.SetActive(true);
        _killTutorial.SetActive(true);
        yield return new WaitForSeconds(3);
        _killTutorial.SetActive(false);

        _target.gameObject.TryGetComponent(out VitalitySystem vitalitySystem);
        yield return new WaitUntil(() => vitalitySystem.CurrentHealth <= 0);
        _tutorialCompleteState.SetActive(true);
        GlobalEventsManager.OnPauseState?.Invoke();
    }

    private void ReleaseJoystickToShoot() => _isReleasedOnEdge = true;
    private void ReleaseJoystickToReset() => _isReleasedInCenter = true;
    private IEnumerator ObjectScaler(GameObject target)
    {
        float time = 0;
        target.gameObject.TryGetComponent(out Animator animator);
        animator.enabled = false;
        while (target.transform.localScale != Vector3.zero)
        {
            target.transform.localScale = Vector3.Lerp(target.transform.localScale, Vector3.zero, time);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(target);
        yield break;
    }
}
