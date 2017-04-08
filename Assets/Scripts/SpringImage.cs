using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringImage : MonoBehaviour {

    public GameObject PhotoFramePrefab;
    public string SearchWord = "春";
    public int TheNumberOfPhotos = 3;

    void Start () {
        StartCoroutine(setup());	
	}

    IEnumerator setup() {
        var imageSearchClient = new ImageSearchClient();
        yield return StartCoroutine(imageSearchClient.Search(SearchWord, TheNumberOfPhotos));
        var httpClient = new HttpClient();
        yield return StartCoroutine(httpClient.GetTextures(imageSearchClient.ImageUriList));
        Debug.Log("ImageDownload OK");
        var textures = httpClient.ResponsedTextures;
        for (int i = 0; i < textures.Count; ++i) {
            var frame = Instantiate(PhotoFramePrefab);
            frame.transform.parent = gameObject.transform;
            frame.transform.rotation = new Quaternion(0, 0, 0, 0);
            frame.GetComponent<Renderer>().material.mainTexture = textures[i];
            frame.transform.position = calcPhotFramePosition(i);
        }
    }

    Vector3 calcPhotFramePosition(int index) {
        var width = PhotoFramePrefab.transform.localScale.z;
        var height = PhotoFramePrefab.transform.localScale.y;
        var z = transform.position.z;        
        var y = 0.1 + transform.position.y;        
        var x = width * index + 0.1 + transform.position.x;
        return new Vector3((float)x, (float)y, (float)z);
    }

    void Update () {		
	}
}
