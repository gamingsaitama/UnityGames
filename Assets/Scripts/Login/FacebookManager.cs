using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using TMPro;
using UnityEngine.UI;
using Facebook.MiniJSON;
using UnityEngine.Networking;

public class FacebookManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UserName;
    [SerializeField] private Image ProfilePic;
    [SerializeField] private FriendPrefab Prefab;
    [SerializeField] private Transform Content;
    private Image avatarFriend;

    void Start()
    {
        var perms = new List<string>() {"public_profile","email", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthenticationCallback);
    }

    private void AuthenticationCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = AccessToken.CurrentAccessToken;
            Debug.Log(aToken.UserId);

            FB.API("/me?fields=name", HttpMethod.GET,GetUserName);

            //FB.API("/me?picture.width(128).height(128)", HttpMethod.GET, GetProfilePicture);
            FB.API("/v2.4/me/?fields=picture", HttpMethod.GET, GetProfilePicture);
            FB.API("/me/friends?fields=name,id,picture.width(128).height(128)&limit=100", HttpMethod.GET, GetFriendList);

        }
        else
        {
            Debug.Log("User canecelled login");
        }
    }

    private void GetUserName(IResult result)
    {
        if (result.Error!=null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            Debug.Log(result);
            string userName = result.ResultDictionary["name"].ToString();
            SetName(userName, UserName);
        }
    }

    private void GetProfilePicture(IGraphResult result)
    {
        if (result.Error!=null)
        {
            Debug.Log("Error : "+result.Error);
        }
        else
        {
            Dictionary<string, object> avatarDict = result.ResultDictionary["picture"] as Dictionary<string, object>;
            avatarDict = avatarDict["data"] as Dictionary<string, object>;

            string friendavatarurl = avatarDict["url"] as string;
            Debug.Log(friendavatarurl);
            DownloadImageHandler(friendavatarurl);
            Sprite profilepic = Sprite.Create(result.Texture, new Rect(0,0,result.Texture.width,result.Texture.height),new Vector2());
            SetProfileImage(avatarFriend.sprite, ProfilePic);
        }
    }

    private void GetFriendList(IGraphResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            Dictionary<string, object> reqResult = Json.Deserialize(result.RawResult) as Dictionary<string, object>;

            List<object> dataObjects = reqResult["data"] as List<object>;
            Debug.Log("FriendsList: " + dataObjects.Count);

            foreach (var datas in dataObjects)
            {
                Dictionary<string, object> dataConvert = datas as Dictionary<string, object>;
                string friendid = dataConvert["id"] as string;
                string friendname = dataConvert["name"] as string;
                Dictionary<string, object> avatarDict = dataConvert["picture"] as Dictionary<string, object>;
                avatarDict = avatarDict["data"] as Dictionary<string, object>;

                string friendavatarurl = avatarDict["url"] as string;
                Debug.Log(friendavatarurl);
                DownloadImageHandler(friendavatarurl);
                var friendPrefab = Instantiate(Prefab,Content);
                friendPrefab.SetAttributes(friendname, friendid, avatarFriend);
            }
        }
    }

    private void SetName(string name,TMP_Text nameText)
    {
        nameText.text = name;
    }

    private void SetProfileImage(Sprite resultImage, Image imageHolder)
    {
        imageHolder.sprite = resultImage;
    }

    IEnumerator DownloadImageHandler(string url)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url);
        DownloadHandlerTexture textureDownload = new DownloadHandlerTexture(true);
        webRequest.downloadHandler = textureDownload;
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Texture2D t = textureDownload.texture;
            Sprite downloadedImage = Sprite.Create(t, new Rect(0, 0, t.width, t.height),
                Vector2.zero, 1f);
            avatarFriend.sprite = downloadedImage;
        }
    }
}