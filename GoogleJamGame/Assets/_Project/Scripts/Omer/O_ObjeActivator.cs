using UnityEngine;

public class O_ObjeActivator : MonoBehaviour
{
    [SerializeField] private O_AniScriptable o_AniScriptable;
    [SerializeField] private GameObject StartObject;
    [SerializeField] private GameObject CompleteObject;

    void OnEnable()
    {
        if (o_AniScriptable.isCompleted)
        {
            if(StartObject != null) StartObject.SetActive(true);
            if(CompleteObject != null) CompleteObject.SetActive(false);
        }
        else
        {
            if(StartObject != null) StartObject.SetActive(false);
            if(CompleteObject != null) CompleteObject.SetActive(true);
        }
    }
}
