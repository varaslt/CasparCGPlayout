using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WMPLib;
using MySql.Data.MySqlClient; // this file is called Interop.WMPLib.dll

namespace MediaImport
{
    public partial class Form1 : Form
    {
        private readonly WindowsMediaPlayerClass wmp = new WindowsMediaPlayerClass();
        private const string targetImportPath = @"D:\caspar\media\import\";
        private const string targetPath = @"D:\caspar\media\";
        MySqlConnection connection;
        private readonly string caspar_database_server_username;
        private readonly string caspar_database_server_hostname;
        private readonly string caspar_database_server_password;
        private readonly string caspar_database_server_database;

        

        public Form1()
        {
            InitializeComponent();
            caspar_database_server_hostname = "bungle.andymace.co.uk";
            caspar_database_server_username = "casparcg";
            caspar_database_server_password = "c45p5rcg";
            caspar_database_server_database = "casparcg";
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProcessDirectory(folderBrowserDialog1.SelectedPath);
            }

        }

        private void ProcessDirectory(string d)
        {
            ConnectDatabase();

            if (!System.IO.Directory.Exists(targetImportPath))
            {
                System.IO.Directory.CreateDirectory(targetImportPath);
            }

            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }


            string[] filePaths = Directory.GetFiles(@d);
           


            int filecounter = 10;

            for (int i=0;i<filePaths.Length;i++)
            {
                
                String filecounterstring = String.Format("{0:D6}"+System.IO.Path.GetExtension(filePaths[i]),filecounter);
                string destFile = System.IO.Path.Combine(targetImportPath, filecounterstring);

                //Hacky i know
                Application.DoEvents();
                System.IO.File.Copy(filePaths[i], destFile, true);
                Application.DoEvents();

                lbLog.Items.Add("Copied: " + System.IO.Path.GetFileName(filePaths[i]) + " Succesfully to Import Area");
            
                
                IWMPMedia mediaInfo = wmp.newMedia(destFile);
                lbLog.Items.Add("Got Data: " + System.IO.Path.GetFileName(filePaths[i]) + " Succesfully");
                
                String dispname = ChopSaveYouTubeandExtention(filePaths[i]);
                String sql = "INSERT INTO `casparcg`.`media` (`clipid`, `display1`,`display2`, `timespan`, `whatnext`, `inframes`,`outframes`) VALUES ('" + MySqlHelper.EscapeString(mediaInfo.name) + "', '" + MySqlHelper.EscapeString(dispname) + "', '" + MySqlHelper.EscapeString(mediaInfo.durationString) + "', )";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
 
                
                //exListBoxClipItem item = new exListBoxClipItem(1, "00:00:00", rdr.GetString("clipid"), rdr.GetString("displayname"), rdr.GetTimeSpan("timespan"), image);
                //exListBox1.Items.Add(item);
 
                String ts = TimeSpan.Parse(mediaInfo.durationString).ToString();
                Console.WriteLine("item = new exListBoxClipItem(1, \"00:00:00\", \""+mediaInfo.name+"\", \""+dispname+"\",TimeSpan.Parse(\"00:"+mediaInfo.durationString+"\"), image);");
                Console.WriteLine("exListBox1.Items.Add(item);");


                lbLog.Items.Add("Inserted " + System.IO.Path.GetFileName(filePaths[i]) + " details into database");

                //Move Items
                System.IO.File.Move(destFile, System.IO.Path.Combine(targetPath, System.IO.Path.GetFileName(destFile)));

                lbLog.Items.Add("Moved: " + System.IO.Path.GetFileName(filePaths[i]) + " Succesfully to Media Area");

                filecounter++;
                
            }

        }

        private void ConnectDatabase()
        {
            string connStr = "server=" + caspar_database_server_hostname + ";database=" + caspar_database_server_database + ";uid=" +
                caspar_database_server_username + ";password=" + caspar_database_server_password + ";";
            connection = new MySqlConnection(connStr);
            connection.Open();

        }

        private string ChopSaveYouTubeandExtention(string p)
        {
            //Remove [SaveYouTube.com] and file extention.
            String n = System.IO.Path.GetFileNameWithoutExtension(p);
            n = n.Replace(" [SaveYouTube.com]", "");
            n = n.Replace(" [Official Music Video]","");
            n = n.Replace(" (Official Music Video)","");
            return n;
        }
    }
}
