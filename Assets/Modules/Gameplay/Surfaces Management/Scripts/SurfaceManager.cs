using UnityEngine;
using System;


    /// <summary>
    /// This class manages any surface behaviour 
    /// </summary>
    public sealed class SurfaceManager : MonoBehaviour
    {
        public static SurfaceManager self { private set; get; } = null;

        ItemStack icyTerrainStack;
        ItemStack mudTerrainStack;
        SurfaceTarget[] spots;              //Represents an array of spawn points for each surface

        [Space(10)]
        [Tooltip("Indica la percentuale per cui un cliente quando lascia il tavolo possa generare una [mud_surface] nel locale - necessita l'utilizzo di un [Surface Target]")]
        [SerializeField][Range(0, 100)] float customerMudSurfaceSpawnPercentage = 30f;
        public float CustomerMudSurfacePercentage => customerMudSurfaceSpawnPercentage;

        [Tooltip("Indica la percentuale per cui un cliente quando lascia il tavolo possa generare una [icy_surface] nel locale - necessita l'utilizzo di un [Surface Target]")]
        [SerializeField][Range(0, 100)] float customerIcySurfaceSpawnPercentage = 30f;
        public float CustomerIcySurfaceSpawnPercentage => customerIcySurfaceSpawnPercentage;

        [Space(10)]
        [Tooltip("Indica la percentuale per cui quando si lancia un piatto sul terreno possa crearsi una [mud_surface]")]
        [SerializeField] [Range(0, 100)] float mudSurfaceSpawnFromDishPercentage = 100f;
        public float MudSurfaceSpawnFromDishPercentage => mudSurfaceSpawnFromDishPercentage;

        [Tooltip("Indica la percentuale per cui quando si lancia un drink sul terreno possa crearsi una [drink_surface]")]
        [SerializeField] [Range(0, 100)] float icySurfaceSpawnFromDrinkPercentage = 100f;
        public float IcySurfaceSpawnFromDrinkPercentage => icySurfaceSpawnFromDrinkPercentage;

        public enum SurfaceType
        {
            NONE = -1,
            ICE,
            MUD
        }

        #region Events
        public delegate void SurfaceGenerationEventHandler(SurfaceManager sender, SurfaceController surface);
        public delegate void SurfaceDestructionEventHandler(SurfaceManager sender, SurfaceController surface);

        /// <summary>
        /// Event called when any surface is created
        /// </summary>
        public static event SurfaceGenerationEventHandler OnSurfaceCreated;
        /// <summary>
        /// Event called when mud surface is created
        /// </summary>
        public static event SurfaceGenerationEventHandler OnMudSurfaceCreated;
        /// <summary>
        /// Event called when icy surface is created
        /// </summary>
        public static event SurfaceGenerationEventHandler OnIcySurfaceCreated;

        /// <summary>
        /// Event called when any surface is destroyed
        /// </summary>
        public static event SurfaceDestructionEventHandler OnSurfaceDestoryed;
        /// <summary>
        /// Event called when mud surface is destroyed
        /// </summary>
        public static event SurfaceDestructionEventHandler OnMudSurfaceDestroyed;
        /// <summary>
        /// Event called when icy surface is destroyed
        /// </summary>
        public static event SurfaceDestructionEventHandler OnIcySurfaceDestroyed;


        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            Init();
        }
        private void Update()
        {
        //Questi Vengono Utilizzati Per Provare
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                ActivatesSurfaceFromSurfaceTarget((SurfaceType)UnityEngine.Random.Range(0, 2));
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                var surfaces = FindObjectsOfType<SurfaceController>();
                for (byte i = 0; i < surfaces.Length; i++)
                {
                    DeactivateSurface(ref surfaces[i]);
                }
            }
        }
        #endregion

        /// <summary>
        /// Initializes surface management properties
        /// </summary>
        void Init()
        {
            if (!self) self = this;

            icyTerrainStack = new ItemStack("Prefabs/icy_surface", 10, self.gameObject);
            mudTerrainStack = new ItemStack("Prefabs/mud_surface", 10, self.gameObject);

            spots = FindObjectsOfType<SurfaceTarget>();
        }
        
        public static void ActivatesSurfaceFromSurfaceTarget(SurfaceType type)
        {
            /** 
             * PLACMENT LOGIC
             * If there are not available spots for placing 
             * items, the While loop will run endless
             * causing an application crash.
             * 
             * For avoiding this behaviour is needed to quit 
             * immediately  if none available spot is free
             * for placing.
             */
            byte index = 0;
            foreach(var spot in self.spots)
            {
                if (spot.IsActive) index++;
            }
            if (index < 1) return;


            bool selected = false;
            byte spotIndex = 0;
            while(!selected)
            {
                Debug.Log("validate");
                spotIndex = (byte)UnityEngine.Random.Range(0, self.spots.Length);
                if (self.spots[spotIndex].IsActive) selected = true;
            }
            Debug.Log($"Index: {spotIndex}");
            self.spots[spotIndex].Disable();

            Debug.Log("process");
            SurfaceController surface = null;
            switch(type)
            {
                case SurfaceType.ICE:

                    surface = self.icyTerrainStack.GetElementFromStack().GetComponent<SurfaceController>();

                    break;
                case SurfaceType.MUD:

                    surface = self.mudTerrainStack.GetElementFromStack().GetComponent<SurfaceController>();

                    break;
            }
            surface.gameObject.transform.position = self.spots[spotIndex].transform.position;
            surface.gameObject.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            surface.gameObject.transform.localScale = Vector3.zero;
            surface.gameObject.SetActive(true);
            surface.RegisterSurfaceTarget(ref self.spots[spotIndex]);
            surface.PlayEnableAnimation();

            //Event handling
            OnSurfaceCreated?.Invoke(self, surface);
            if (surface.Type == SurfaceType.ICE) OnIcySurfaceCreated?.Invoke(self, surface);
            else if (surface.Type == SurfaceType.MUD) OnMudSurfaceCreated?.Invoke(self, surface);
        }
        public static void GeneratesSurfaceFromThrownPlate(SurfaceType type, Transform plate)
        {
            SurfaceController surface = null;
            switch(type)
            {
                case SurfaceType.ICE:
                    surface = self.icyTerrainStack.GetElementFromStack().GetComponent<SurfaceController>();
                    break;
                case SurfaceType.MUD:
                    surface = self.mudTerrainStack.GetElementFromStack().GetComponent<SurfaceController>();
                    break;
            }
            surface.transform.position = new Vector3(plate.position.x, 0.04f, plate.position.z);
            surface.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0);
            surface.transform.localScale = Vector3.zero;
            surface.gameObject.SetActive(true);
            surface.PlayEnableAnimation();

            //surface creation events handler
            OnSurfaceCreated?.Invoke(self, surface);
            if (surface.Type == SurfaceType.ICE) OnIcySurfaceCreated?.Invoke(self, surface);
            else if (surface.Type == SurfaceType.MUD) OnMudSurfaceCreated?.Invoke(self, surface);
        }
        public static void DeactivateSurface(ref SurfaceController surface)
        {
            surface.PlayDisableAnimation((target) =>
            {
                target.starget.Enable();
                target.UnregisterSurfaceTarget();
                target.gameObject.SetActive(false);

                //surface destrouction events hanlder
                OnSurfaceDestoryed?.Invoke(self, target);
                if (target.Type == SurfaceType.ICE) OnIcySurfaceDestroyed?.Invoke(self, target);
                else if (target.Type == SurfaceType.MUD) OnMudSurfaceDestroyed?.Invoke(self, target);
            });
        }

    }
