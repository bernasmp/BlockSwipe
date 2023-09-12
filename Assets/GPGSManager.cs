using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;

public class GPGSManager : MonoBehaviour
{
    public TextMeshProUGUI statusTxt;
    // Start is called before the first frame update
    void Start() {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication); //automatically creates the build configuration, activates it and authenticates the user.
    }
    internal void ProcessAuthentication(SignInStatus status) // called on start, this handles the call back method received from play games platform and processes the result.
    {
        if (status == SignInStatus.Success) {
            statusTxt.text = "Successful Sign In";
        } else if (status == SignInStatus.InternalError) {
            statusTxt.text = "Failed due to internal error";
        } else if (status == SignInStatus.Canceled) {
            statusTxt.text = "Failed due to Canceled status";
        } else {
            statusTxt.text = "This should never be triggered, not one of the call back responses.";
        }
    }
    public void TriggerManualSignIn() // the script attached to the button to trigger a manual signin.
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }
}
