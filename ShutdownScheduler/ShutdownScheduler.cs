using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShutdownScheduler
{
    public partial class ShutdownScheduler : Form
    {
        public ShutdownScheduler()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double totalDelaySeconds = 0;


            if (double.TryParse(textBox1.Text, out double delay1))
                totalDelaySeconds += (delay1 * 86400);

            if (double.TryParse(textBox2.Text, out double delay2))
                totalDelaySeconds += (delay1 * 3600);

            if (double.TryParse(textBox3.Text, out double delay3))
                totalDelaySeconds += (delay3 * 60);

            if (double.TryParse(textBox4.Text, out double delay4))
                totalDelaySeconds += delay4;


            if(totalDelaySeconds > 0)
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = $"-s -t {totalDelaySeconds}",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                };
                Process process = new Process
                {
                    StartInfo = psi,
                };
                process.Start();
                process.WaitForExit();

                if(process.ExitCode == 1190)
                {
                    MessageBox.Show("A shutdown is already scheduled!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Successfully scheduled shutdown!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No valid values provided!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "shutdown",
                Arguments = "-a",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            Process process = new Process
            {
                StartInfo = psi,
            };
            process.Start();
            process.WaitForExit();

            int exitCode = process.ExitCode;

            if(exitCode == 1116)
            {
                MessageBox.Show("Unable to abort shutdown because no shutdown was in progress.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Shutdown aborted!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
