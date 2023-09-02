using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlideshow : MonoBehaviour
{
    public Image[] images;  // An array of Image components containing your images
    public float delayBetweenImages = 15.0f;  // Time delay between image changes

    private void Start()
    {
        StartCoroutine(StartSlideshow());
    }

    private IEnumerator StartSlideshow()
    {
        // Loop through each image
        for (int i = 0; i < images.Length; i++)
        {
            // Set the current image to be visible
            images[i].enabled = true;
            Debug.Log(i);
            // Wait for the specified delay
            yield return new WaitForSeconds(delayBetweenImages);

            // Set the current image to be invisible
            images[i].enabled = false;
        }

        // If you want the slideshow to loop, you can uncomment the following line
        StartCoroutine(StartSlideshow());
    }
}