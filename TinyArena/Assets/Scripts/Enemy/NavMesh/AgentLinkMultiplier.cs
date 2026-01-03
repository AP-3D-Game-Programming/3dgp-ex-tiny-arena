using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;


#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(NavMeshLink))]
public class AgentLinkMultiplier : MonoBehaviour
{
    [SerializeField] private float angleIncrement = 15f;
    [SerializeField] private int numberOfCopies = 23;
    [SerializeField] private Transform centerPoint;
    
    private NavMeshLink sourceLink;

    void Start()
    {
        sourceLink = GetComponent<NavMeshLink>();
    }

    [ContextMenu("Multiply NavMesh Link")]
    public void MultiplyLink()
    {
        sourceLink = GetComponent<NavMeshLink>();
        if (sourceLink == null)
        {
            Debug.LogError("No NavMeshLink found!");
            return;
        }

        Vector3 center = centerPoint != null ? centerPoint.position : Vector3.zero;
        
        ClearCopies();
        
        for (int i = 1; i <= numberOfCopies; i++)
        {
            CreateCopy(angleIncrement * i, center, i);
        }
        
        Debug.Log($"Created {numberOfCopies} NavMeshLink copies");

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
#endif
    }

    private void CreateCopy(float angle, Vector3 center, int index)
    {
        // Duplicate the GameObject
        GameObject copy = Instantiate(gameObject, transform.parent);
        copy.name = $"NavMeshLink_Copy_{index}";
        
        // Remove the multiplier script from the copy
        AgentLinkMultiplier multiplierScript = copy.GetComponent<AgentLinkMultiplier>();
        if (multiplierScript != null)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(multiplierScript);
            else
#endif
                Destroy(multiplierScript);
        }
        
        // Calculate rotation around center
        Vector3 directionFromCenter = transform.position - center;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 newPosition = center + rotation * directionFromCenter;
        
        // Apply rotation
        copy.transform.position = newPosition;
        copy.transform.rotation = transform.rotation * rotation;
        
        Debug.Log($"Copy {index}: Rotated {angle}° around {center}");

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Undo.RegisterCreatedObjectUndo(copy, "Create NavMeshLink Copy");
            EditorUtility.SetDirty(copy);
        }
#endif
    }

    [ContextMenu("Clear Copies")]
    public void ClearCopies()
    {
        if (transform.parent == null) return;

        int cleared = 0;
        for (int i = transform.parent.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.parent.GetChild(i);
            if (child.name.StartsWith("NavMeshLink_Copy_"))
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    DestroyImmediate(child.gameObject);
                else
#endif
                    Destroy(child.gameObject);
                cleared++;
            }
        }
        
        Debug.Log($"Cleared {cleared} copies");
    }
}