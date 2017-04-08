using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpClient {
    public List<Texture> ResponsedTextures;
    public Texture ResponsedTexture;

    public IEnumerator GetTextures(IList<string> uriList) {
        ResponsedTextures = new List<Texture>();
        var coroutines = new List<IEnumerator>();
        foreach (var uri in uriList) {
            coroutines.Add(GetTexture(uri));
        }
        foreach (var c in coroutines) {
            yield return c;
            ResponsedTextures.Add((Texture)c.Current);
        }
        Debug.Log("Load textures " + ResponsedTextures.Count);
        //yield return ResponsedTextures;
    }
    public IEnumerator GetTexture(string imageUri) {
        using (var request = UnityWebRequest.GetTexture(imageUri)) {
            yield return request.Send();
            if (request.isError) {
                Debug.Log(request.error);
            } else {
                ResponsedTexture = ((DownloadHandlerTexture)(request.downloadHandler)).texture;
            }
            yield return ResponsedTexture;
        }
    }

}
