using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knoledge_spv
{
    public class HourGlass : IDisposable
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public HourGlass()
        {
            Enabled = true;
        }

        public void Dispose()
        {
            Enabled = false;
        }

        public static bool Enabled
        {
            get { return Application.UseWaitCursor; }
            set
            {
                if (value == Application.UseWaitCursor) return;
                Application.UseWaitCursor = value;
                try
                {
                    var form = Form.ActiveForm;

                    if (form != null) // Send WM_SETCURSOR
                        SendMessage(form.Handle, 0x20, form.Handle, (IntPtr)1);
                }
                catch
                {
                }
            }
        }
    }
}
