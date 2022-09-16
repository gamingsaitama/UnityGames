using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendPrefab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI FriendName;
    [SerializeField] private TextMeshProUGUI Id;
    [SerializeField] private Image Avatar;

    public void SetAttributes(string name, string id, Image image)
    {
        FriendName.text = name;
        Id.text = id;
        Avatar = image;
    }

}
