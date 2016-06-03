using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class MoveOwnPosition : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj;
    [SerializeField] GameObject ownPlayer;
    [SerializeField] GameObject targetPointer;
    Vector3 movePos;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        targetPointer.SetActive(false);
        this.UpdateAsObservable()
            .Where(_ => isTriggerDown())
            .Subscribe(_ => dropRay());

        this.UpdateAsObservable()
            .Where(_ => isTriggerUp())
            .Subscribe(_ => moveTOPoint());
    }

    bool isTriggerDown()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            return true;
        }
        return false;
    }

    bool isTriggerUp()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            return true;
        }
        return false;
    }

    void dropRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
        {
            movePos = hit.point;
            targetPointer.SetActive(true);
            targetPointer.transform.position = movePos;
        }
    }

    void moveTOPoint()
    {
        ownPlayer.transform.position = movePos;
    }
}
