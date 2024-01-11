using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildIngameScript : MonoBehaviour
{
    public TextMeshProUGUI methodName;
    public TextMeshProUGUI variables;
    public TextMeshProUGUI returnName;
    public TMP_InputField inputField;

    private string script;

    public void BuildScript()
    {
        script = methodName.text + @"
        {
            " + variables.text + @"
            " + inputField.text + @"
            " + returnName.text + @"
        }";
    }
}
