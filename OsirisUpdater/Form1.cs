using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibGit2Sharp;

namespace OsirisUpdater
{
    public partial class Form1 : Form
    {
        private Repository repo;
        private static Config config = new Config();
        private JsonClass json = new JsonClass();
        private string log = "";
        public Form1()
        {
            InitializeComponent();
            json.LoadConfig(ref config);
            if (config.state == 1)
            {
                label2.Text = "Initilazed";
                button1.Text = "Update";
                repo = new Repository(config.pathToOsiris);
            }else if (config.state == 2)
            {
                label2.Text = "Builded";
                button1.Text = "Update";
                repo = new Repository(config.pathToOsiris);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (config.state == 0)
            {
                DialogResult result = folderBrowserDialog1.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                {
                    try
                    {
                        //Repository.Clone("https://github.com/danielkrupinski/Osiris.git", folderBrowserDialog1.SelectedPath);
                        config.pathToOsiris = folderBrowserDialog1.SelectedPath;
                        Clone("https://github.com/danielkrupinski/Osiris.git", folderBrowserDialog1.SelectedPath);
                    }
                    catch (UserCancelledException ex)
                    {
                        MessageBox.Show("Clone canceled!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Exeption: {ex.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    label2.Text = "Initilaized";
                    config.state = 1;
                    button1.Text = "Update";
                    json.SaveConfig(config);
                    repo = new Repository(config.pathToOsiris);
                }
            }
            else
            {
                var remote = repo.Network.Remotes["origin"];
                var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                Commands.Pull(repo, new Signature("name", "email@mysite.net", DateTimeOffset.Now), new PullOptions() );
                config.state = 1;
                label2.Text = "Updated";
                //Commands.Fetch(repo, remote.Name, refSpecs, null, log);
            }
        }

        private void Clone(string source, string dist)
        {
            Form2 form2 = new Form2();
            form2.Show();

            if (Directory.Exists(dist))
            {
                if (Directory.EnumerateFiles(dist).Count() != 0)
                {
                    MessageBox.Show("Folder could be empty!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form2.Close();
                    return;
                }
            }

            try
            {
                Task task = new Task(() => Repository.Clone(source, dist));
                task.Start();
                task.Wait();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while cloning: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            form2.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(config.pathToBuildTools))
                while (string.IsNullOrEmpty(config.pathToBuildTools))
                    openFileDialog1.ShowDialog();
            ProcessStartInfo info = new ProcessStartInfo(config.pathToBuildTools);
            info.Arguments = $"{Path.Combine(config.pathToOsiris, "Osiris.sln")} /p:Configuration=Release /m -fl";
            Process proc = Process.Start(info);
            proc.WaitForExit();
            if (File.Exists(Path.Combine(config.pathToOsiris, "Release/Osiris.dll"))) config.pathToDll = Path.Combine(config.pathToOsiris, "Release/Osiris.dll");
            else {
                MessageBox.Show("Osiris.dll not exists! Check msbuildlog.txt for more info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            config.state = 2;
            label2.Text = "Builded";
            json.SaveConfig(config);
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (e.Cancel) return;
            config.pathToBuildTools = openFileDialog1.FileName;
            json.SaveConfig(config);
        }

        public static void SaveConfig(Config cfg)
        {
            config = cfg;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(config.pathToInjector))
                while (string.IsNullOrEmpty(config.pathToInjector))
                    openFileDialog2.ShowDialog();
            ProcessStartInfo info = new ProcessStartInfo(config.pathToInjector);
            Process[] processes = Process.GetProcesses();
            string injectTypeStr = "";
            if (config.injectType == 0) injectTypeStr = "-loadlibrary";
            else if (config.injectType == 1) injectTypeStr = "-manual-map";
            else injectTypeStr = "-loadlibrary";
            foreach (Process proc in processes)
            {
                if(proc.ProcessName == "csgo")
                {
                    info.Arguments = $"{injectTypeStr} {config.pathToDll} {proc.Id}";
                }
            }
            if (string.IsNullOrEmpty(info.Arguments))
            {
                MessageBox.Show("Run game before try inject", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process.Start(info);
        }

        private void ShowInjectorConfig()
        {
            Form3 form = new Form3(config);
            form.ShowDialog();
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            ShowInjectorConfig();
        }

        private void OpenFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            if (e.Cancel) return;
            config.pathToInjector = openFileDialog2.FileName;
        }
    }
}
