using RoslynCSharp;
using RoslynCSharp.Compiler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TN
{
    public class ScriptBuilder : MonoBehaviour
    {
        private string activeSourceCode = null;
        private ScriptProxy activeQuestScript = null;
        private ScriptDomain domain = null;

        public QuestSystem questSystem;

        public AssemblyReferenceAsset[] assemblyReferences;

        public void Start()
        {
            // Create the domain
            domain = ScriptDomain.CreateDomain("QuestScriptCode", true);

            // Add assembly references
            foreach (AssemblyReferenceAsset reference in assemblyReferences)
                domain.RoslynCompilerService.ReferenceAssemblies.Add(reference);
        }

        public void CreateAndCompileScript(string playerWrittenCode)
        {
            string sourceCode = @"
            using System;
            using UnityEngine;
            namespace TN
            {
                class Script : QuestScript
                {"
                    + questSystem.activeVariables.text + @"
                    public override " + questSystem.activeMethodName.text + @"
                    {
                        " + playerWrittenCode + @"

                        " + questSystem.activeReturnName + @"
                    }
                }
            }";

            if (activeSourceCode != sourceCode || activeQuestScript == null)
            {
                // Remove any other scripts
                ResetQuestScript();

                // Compile code
                ScriptType type = domain.CompileAndLoadMainSource(sourceCode, ScriptSecurityMode.UseSettings, assemblyReferences);

                // Check for null
                if (type == null)
                {
                    if (domain.RoslynCompilerService.LastCompileResult.Success == false)
                        throw new Exception("Code contained errors. Please fix and try again");
                    else if (domain.SecurityResult.IsSecurityVerified == false)
                        throw new Exception("Failed code security verification");
                    else
                        throw new Exception("Failed to build code");
                }

                // Check for base class
                if (type.IsSubTypeOf<QuestScript>() == false)
                    throw new Exception("Code must define a single type that inherits from 'TN.QuestScript'");

                // Create an instance
                activeQuestScript = type.CreateInstance();
                activeSourceCode = sourceCode;
            }
            questSystem.EvaluateQuestScript(activeQuestScript);
        }

        public void ResetQuestScript()
        {
            if (activeQuestScript != null)
            {
                // Get the QuestScript instance
                // QuestScript questScript = activeQuestScript.GetInstanceAs<QuestScript>(false);

                // Destroy script
                activeQuestScript.Dispose();
                activeQuestScript = null;
            }
        }

    }

}
