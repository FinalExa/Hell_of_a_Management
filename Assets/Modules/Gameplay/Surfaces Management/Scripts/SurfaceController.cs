using DG.Tweening;
using System;
using UnityEngine;

public class SurfaceController : MonoBehaviour
{
    [SerializeField] SurfaceManager.SurfaceType type = SurfaceManager.SurfaceType.NONE;
    public SurfaceManager.SurfaceType Type => type;

    [SerializeField] Vector3 size = new Vector3(0.2f, 0, 0.2f);
    public Vector3 Size => size;

    public PlayerData playerData;

    public bool isBeingRemoved;

    /// <summary>
    /// The referred surface target where this surface is placed
    /// </summary>

    #region Events
    public delegate void SurfaceEventHandler(SurfaceController sender, GameObject obj, SurfaceManager.SurfaceType type);

    #endregion

    #region Unity Callbacks
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (type)
            {
                case SurfaceManager.SurfaceType.MUD:
                    playerData.currentMovementSpeed = playerData.slowMovementSpeed;
                    break;
                case SurfaceManager.SurfaceType.ICE:
                    playerData.currentMovementSpeed = playerData.fastMovementSpeed;
                    break;
            }
        }
    }
    protected virtual void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
            playerData.currentMovementSpeed = playerData.defaultMovementSpeed;

    }
    #endregion

    /// <summary>
    /// Executes the enable animation
    /// </summary>
    /// <param name="OnAnimationCompleted">Event called when animation ends</param>
    public virtual void PlayEnableAnimation(Action<SurfaceController> OnAnimationCompleted = null)
    {
        gameObject.transform.DOScale(Size.magnitude, 0.5f)
            .OnComplete(() => OnAnimationCompleted?.Invoke(this));
    }
    /// <summary>
    /// Executes the disable animation
    /// </summary>
    /// <param name="OnAnimationCompleted">Event called when animation ends</param>
    public virtual void PlayDisableAnimation(Action<SurfaceController> OnAnimationCompleted = null)
    {
        gameObject.transform.DOScale(0, 0.5f)
            .OnComplete(() => OnAnimationCompleted?.Invoke(this));
    }
}
