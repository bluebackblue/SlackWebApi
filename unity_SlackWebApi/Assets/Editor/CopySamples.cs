

/** Editor
*/
#if(UNITY_EDITOR)
namespace Editor
{
    /** CopySamples
    */
    public class CopySamples
    {
        /** s_process
        */
        private static System.Diagnostics.Process s_process = null;

        /** MenuItem_Tool_CopySamples
        */
        [UnityEditor.MenuItem("ツール/CopySamples")]
        private static void MenuItem_Tool_CopySamples()
        {
            s_process = new System.Diagnostics.Process();
            {
                s_process.StartInfo.FileName = UnityEngine.Application.dataPath + "/Editor/CopySamples.bat";
                s_process.StartInfo.Arguments = "SlackWebApi";
                s_process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                s_process.StartInfo.UseShellExecute = true;
                s_process.Exited += ExitProcess;
                s_process.Start();
            }
        }

        /** ExitProcess
        */
        private static void ExitProcess(object a_sender,System.EventArgs a_eventargs)
        {
            if(s_process != null){
                s_process.Dispose();
                s_process = null;
            }
        }
    }
}
#endif

