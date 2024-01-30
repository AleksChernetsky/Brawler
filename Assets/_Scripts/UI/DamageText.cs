using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private Rigidbody _textRigidbody;
    public Rigidbody RigidBody => _textRigidbody;
    public TextMesh textMesh;

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, 180f));
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
        _textRigidbody.velocity = Vector3.zero;
        _textRigidbody.position = Vector3.zero;
        _textRigidbody.rotation = Quaternion.identity;
    }
}