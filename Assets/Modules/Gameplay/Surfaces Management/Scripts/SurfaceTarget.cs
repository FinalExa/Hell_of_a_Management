using UnityEngine;


    public sealed class SurfaceTarget : MonoBehaviour
    {
        [SerializeField] bool isActive = true;
        public bool IsActive => isActive;

        /// <summary>
        /// Enables to use this surface target.
        /// </summary>
        public void Enable() => isActive = true;
        /// <summary>
        /// Disables to use this surface target.
        /// </summary>
        public void Disable() => isActive = false;
    }
