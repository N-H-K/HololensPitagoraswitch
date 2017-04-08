using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ImageSearchClient : MonoBehaviour {
    public List<string> ImageUriList = new List<string>();
    string subscriptionKey = Configure.ImageSearchAppKey;
    string endpoint = "https://api.cognitive.microsoft.com/bing/v5.0/images/search";

    public IEnumerator Search(string word, int num) {
        using (var request = createRequestQuery(word, num)) {
            yield return request.Send();
            if (request.isError) {
                Debug.Log(request.error);
            } else {
                ImageUriList = parseResult(request.downloadHandler.text);
            }
            yield return ImageUriList;
        }
    }

    UnityWebRequest createRequestQuery(string searchWord, int num) {
        var parameters = new Dictionary<string, string>();
        parameters.Add("q", WWW.EscapeURL(searchWord));
        parameters.Add("count", num.ToString());
        parameters.Add("mkt", "ja-JP");
        var q = string.Join("&", parameters.Select(p => p.Key + "=" + p.Value).ToArray());

        var request = UnityWebRequest.Get(endpoint + "?" + q);
        request.SetRequestHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
        return request;
    }

    List<string> parseResult(string json) {
        var imageUriList = new List<string>();
        var resopnse = JsonUtility.FromJson<BingImageSearchResponseField>(json);
        foreach (var item in resopnse.value) {
            imageUriList.Add(item.contentUrl);
        }
        return imageUriList;
    }

    [Serializable]
    class BingImageSearchResponseField {
        public List<Item> value;

        [Serializable]
        public class Item {
            public int width;
            public int height;
            public string contentUrl;
        }
    }

}
