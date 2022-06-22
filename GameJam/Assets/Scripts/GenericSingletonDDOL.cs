using UnityEngine;

public class GenericSingletonDDOL : MonoBehaviour
{
    private static GenericSingletonDDOL instance;
    public static GenericSingletonDDOL Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
