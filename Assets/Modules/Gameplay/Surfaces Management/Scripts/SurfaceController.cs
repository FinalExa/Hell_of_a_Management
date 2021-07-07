using UnityEngine;
using System;
using DG.Tweening;

    public class SurfaceController : MonoBehaviour
    {
        [SerializeField] SurfaceManager.SurfaceType type = SurfaceManager.SurfaceType.NONE;
        public SurfaceManager.SurfaceType Type => type;

        [SerializeField] Vector3 size = new Vector3(0.2f, 0, 0.2f);
        public Vector3 Size => size;

        /// <summary>
        /// The referred surface target where this surface is placed
        /// </summary>
        internal SurfaceTarget starget;

        #region Events
        public delegate void SurfaceEventHandler(SurfaceController sender, GameObject obj, SurfaceManager.SurfaceType type);

        /// <summary>
        /// Event called when an object moves into a surface
        /// </summary>
        public static event SurfaceEventHandler OnSurface;
        /// <summary>
        /// Event called when an object moves into a slippery surface
        /// </summary>
        public static event SurfaceEventHandler OnSlipperySurface;
        /// <summary>
        /// Evetn called whena an object moves into a slowery surface
        /// </summary>
        public static event SurfaceEventHandler OnSlowerySurface;

        /// <summary>
        /// Event called when the player hits a surface
        /// </summary>
        public static event SurfaceEventHandler OnPlayerHitSurface;
        /// <summary>
        /// Event called when a player hits a slippery surface;
        /// </summary>
        public static event SurfaceEventHandler OnPlayerHitSlipperySurface;
        /// <summary>
        /// Event called when a player hits a slowery surface
        /// </summary>
        public static event SurfaceEventHandler OnPlayerHitSlowerySurface;
        #endregion

        #region Unity Callbacks
        protected virtual void OnTriggerEnter(Collider other)
        {
            OnSurface?.Invoke(this, other.gameObject, Type);
            if(type == SurfaceManager.SurfaceType.ICE)
            {
                OnSlipperySurface?.Invoke(this, other.gameObject, Type);
            }
            else if(type == SurfaceManager.SurfaceType.MUD)
            {
                OnSlowerySurface?.Invoke(this, other.gameObject, Type);
            }

            
            if (other.tag == "Player")
            {
                switch (type)
                {
                    case SurfaceManager.SurfaceType.MUD:
                    //Attiva muovimento Mud
                        break;
                    case SurfaceManager.SurfaceType.ICE:
                    //Attiva muovimento Ice
                        break;
                }
            }
        }
        protected virtual void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
            {
                //Reimposta muovimento Default
            }
        }
        #endregion

        public void RegisterSurfaceTarget(ref SurfaceTarget st) => starget = st;
        public void UnregisterSurfaceTarget() => starget = null;

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