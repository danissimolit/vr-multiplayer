using UnityEngine;

public class AvatarInputConverter : MonoBehaviour
{
    [Header("Avatar")]
    public Transform mainAvatarTransform;
    public Transform avatarHead;
    public Transform avatarBody;
    public Transform avatarHandL;
    public Transform avatarHandR;

    [Header("XR")]
    [SerializeField] private Transform _xrCamera;
    [SerializeField] private Transform _xrHandL;
    [SerializeField] private Transform _xrHandR;

    [Header("Offsets")]
    [SerializeField] private Vector3 _headOffset;

    private const float _lerpTime = 0.5f;
    private const float _lerpTimeForBodyRotation = 0.05f;

    void Update()
    {
        //Body and Head transform
        mainAvatarTransform.position = Vector3.Lerp(mainAvatarTransform.position, _xrCamera.position + _headOffset, _lerpTime);
        avatarHead.rotation = Quaternion.Lerp(avatarHead.rotation, _xrCamera.rotation, _lerpTime);

        Vector3 yHeadRotation = new Vector3(0f, avatarHead.rotation.eulerAngles.y, 0f);
        avatarBody.rotation = Quaternion.Lerp(avatarBody.rotation, Quaternion.Euler(yHeadRotation), _lerpTimeForBodyRotation);

        //Hands transform
        avatarHandL.position = Vector3.Lerp(avatarHandL.position, _xrHandL.position, _lerpTime);
        avatarHandL.rotation = Quaternion.Lerp(avatarHandL.rotation, _xrHandL.rotation, _lerpTime);

        avatarHandR.position = Vector3.Lerp(avatarHandR.position, _xrHandR.position, _lerpTime);
        avatarHandR.rotation = Quaternion.Lerp(avatarHandR.rotation, _xrHandR.rotation, _lerpTime);
    }
}
