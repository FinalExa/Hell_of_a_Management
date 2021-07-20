using UnityEngine;


/// <summary>
/// This class manages any surface behaviour 
/// </summary>
public sealed class SurfaceManager : MonoBehaviour
{
    public static SurfaceManager self { private set; get; } = null;

    ItemStack icyTerrainStack;
    ItemStack mudTerrainStack;

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
    #endregion

    /// <summary>
    /// Initializes surface management properties
    /// </summary>
    void Init()
    {
        if (!self) self = this;

        icyTerrainStack = new ItemStack("Prefabs/icy_surface", 10, self.gameObject);
        mudTerrainStack = new ItemStack("Prefabs/mud_surface", 10, self.gameObject);
    }

    public void GeneratesSurfaceFromThrownPlate(SurfaceType type, Transform plate)
    {
        SurfaceController surface = null;
        switch (type)
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
            target.gameObject.SetActive(false);

            //surface destrouction events hanlder
            OnSurfaceDestoryed?.Invoke(self, target);
            if (target.Type == SurfaceType.ICE) OnIcySurfaceDestroyed?.Invoke(self, target);
            else if (target.Type == SurfaceType.MUD) OnMudSurfaceDestroyed?.Invoke(self, target);
        });
    }

}
