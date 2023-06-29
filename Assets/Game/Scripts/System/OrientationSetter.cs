using UnityEngine;

public class OrientationSetter : MonoBehaviour
{
    public enum Orientation
    {
        Any,
        Portrait,
        PortraitUpsideDown,
        LandscapeLeft,
        LandscapeRight,
        Landscape
    }

    public Orientation ScreenOrientation;

    private void Start()
    {
        switch (ScreenOrientation)
        {
            case Orientation.Any:
                Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;
                
                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                break;
            
            case Orientation.Portrait:
                Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
                break;
            
            case Orientation.PortraitUpsideDown:
                Screen.orientation = UnityEngine.ScreenOrientation.PortraitUpsideDown;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
                break;
            
            case Orientation.LandscapeLeft:
                Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
                break;
            
            case Orientation.LandscapeRight:
                Screen.orientation = UnityEngine.ScreenOrientation.LandscapeRight;
                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
                break;
            
            case Orientation.Landscape:
                Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                break;
        }

        Destroy(gameObject);
    }
}