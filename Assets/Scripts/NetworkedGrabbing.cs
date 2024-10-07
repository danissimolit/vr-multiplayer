using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkedGrabbing : MonoBehaviourPun, IPunOwnershipCallbacks
{
    [SerializeField] private InteractionLayerMask _interactionLayerInteractable;
    [SerializeField] private InteractionLayerMask _InteractionLayerDefault;

    private PhotonView m_View;
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        m_View = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void TransferOwnership()
    {
        if (m_View.Owner != PhotonNetwork.LocalPlayer)
        {
            Debug.Log("Requesting ownership transfer...");
            m_View.RequestOwnership();
        }
    }


    public void OnSelectEntered()
    {
        Debug.Log("Grabbed");

        m_View.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);
        m_View.RPC("SetDefaultInteractionLayer", RpcTarget.OthersBuffered);
        TransferOwnership();
    }

    public void OnSelectExited()
    {
        Debug.Log("Released");

        m_View.RPC("StopNetworkGrabbing", RpcTarget.AllBuffered);
        m_View.RPC("SetInteractableInteractionLayer", RpcTarget.OthersBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != m_View)
        {
            return;
        }

        Debug.Log("Ownership requested for: " + targetView.name + " from " + requestingPlayer.NickName);
        m_View.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("Ownership transfered to " + targetView.name + " from " + previousOwner.NickName);
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        Debug.Log("Transfer failed for " + targetView.name + ". Sender of failed request: " + senderOfFailedRequest.NickName);
    }


    [PunRPC]
    public void StartNetworkGrabbing()
    {
        rb.isKinematic = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        rb.isKinematic = false;
    }

    [PunRPC]
    public void SetInteractableInteractionLayer()
    {
        grabInteractable.interactionLayers = _interactionLayerInteractable;
    }

    [PunRPC]
    public void SetDefaultInteractionLayer()
    {
        grabInteractable.interactionLayers = _InteractionLayerDefault;
    }
}
