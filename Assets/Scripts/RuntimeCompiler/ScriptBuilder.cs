using RoslynCSharp;
using RoslynCSharp.Compiler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    /// <summary>
    /// Builder for assembling the script components and compiling the scripts created at runtime.
    /// </summary>
    public class ScriptBuilder : MonoBehaviour
    {
        public QuestSystem questSystem;
        public AssemblyReferenceAsset[] assemblyReferences;

        private string activeSourceCode = null;
        private ScriptProxy activeQuestScript = null;
        private ScriptDomain domain = null;

        // Create domain and add needed c# code librarys. 
        private void Start()
        {
            // Create the domain
            domain = ScriptDomain.CreateDomain("QuestScriptCode", true);

            // Add assembly references
            foreach (AssemblyReferenceAsset reference in assemblyReferences)
                domain.RoslynCompilerService.ReferenceAssemblies.Add(reference);
        }

        // Create and compile script with the ingame written code.
        public bool CreateAndCompileScript(string playerWrittenCode)
        {
            string sourceCode = @"
            using System;
            using System.Collections;
            using System.Collections.Generic;
            using UnityEngine;
            namespace TN
            {
                class Script
                {
                    " + questSystem.activeVariables.text + @"
                    public " + questSystem.activeMethodName.text + @"
                    {
                        " + playerWrittenCode + @"

                        " + questSystem.activeReturnName.text + @"
                    }
                }
            }";

            // Clear error message text field
            questSystem.consoleField.text = "";

            Debug.Log(sourceCode);

            if (activeSourceCode != sourceCode || activeQuestScript == null)
            {
                // Remove any other scripts
                ResetQuestScript();

                // Compile code
                ScriptType type = domain.CompileAndLoadMainSourceInterpreted(sourceCode, ScriptSecurityMode.UseSettings, assemblyReferences);

                // Check for null
                if (type == null)
                {
                    if (domain.RoslynCompilerService.LastCompileResult.Success == false)
                    {
                        ConsoleMessage("Code contained errors. Please fix and try again");
                        throw new Exception("Code contained errors. Please fix and try again");
                    }
                    else if (domain.SecurityResult.IsSecurityVerified == false)
                    {
                        ConsoleMessage("Failed code security verification");
                        throw new Exception("Failed code security verification");
                    }
                    else
                    {
                        ConsoleMessage("Failed to build code");
                        throw new Exception("Failed to build code");
                    }
                }

                // // Check for base class
                // if (type.IsSubTypeOf<QuestScript>() == false)
                // {
                //     ConsoleMessage("Code must define a single type that inherits from 'TN.QuestScript'");
                //     throw new Exception("Code must define a single type that inherits from 'TN.QuestScript'");
                // }

                // Create an instance
                activeQuestScript = type.CreateInstance();
                activeSourceCode = sourceCode;
            }
            return questSystem.EvaluateQuestScript(activeQuestScript);
        }

        // Destroy the old quest script.
        public void ResetQuestScript()
        {
            if (activeQuestScript != null)
            {
                // Destroy script
                activeQuestScript.Dispose();
                activeQuestScript = null;
            }
        }

        // Display the error message.
        public void ConsoleMessage(string consoleMessage)
        {
            questSystem.consoleField.text = consoleMessage;
        }


    }

}
