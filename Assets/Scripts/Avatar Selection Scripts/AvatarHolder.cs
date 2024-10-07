using Unity.XR.CoreUtils;
using UnityEngine;

public class AvatarHolder : MonoBehaviour
{
    public Transform mainAvatarTransform;
    public Transform headTransform;
    public Transform bodyTransform;
    public Transform handLeftTransform;
    public Transform handRightTransform;

    private void Start()
    {
        //Setting the layer of avatar head to AvatarLocalHead layer so that it does not block the view of the local VR Player
        headTransform.gameObject.SetLayerRecursively(MultiplayerVRConstants.LAYER_LOCAL_HEAD_INT_VALUE);

        //Setting the layer of avatar body to AvatarLocalBody layer so that it does not block the view of the local VR Player
        bodyTransform.gameObject.SetLayerRecursively(MultiplayerVRConstants.LAYER_LOCAL_BODY_INT_VALUE);
    }
}
