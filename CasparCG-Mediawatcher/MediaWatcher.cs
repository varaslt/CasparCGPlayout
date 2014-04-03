namespace MediaWatcher
{
    using System;
    using System.IO;
    using System.Configuration;
    using System.Diagnostics;
    using System.ServiceProcess;
    using System.Data;
    using log4net;
    using log4net.Config;
    using MySql.Data.MySqlClient;
    using FFmpegSharp.Interop;
    using FFmpegSharp;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Drawing.Imaging;
    using CasparCG_Mediawatcher;
    using System.Xml.XPath;
    using System.Xml;
   

    class MediaWatcher : ServiceBase
    {
        FileSystemWatcher _watchFolder = new FileSystemWatcher();
        private MySqlConnection _connection;

        public string casparDatabaseServerHostname { get; set; }
        public string casparDatabaseServerUsername { get; set; }
        public string casparDatabaseServerPassword { get; set; }
        public string casparDatabaseServerDatabase { get; set; }
        public string casparDatabaseServerConnectiontimeout { get; set; }
        public string DirectoryToWatch { get; set; }

        private ServiceInstaller serviceInstaller;
        
        private ServiceProcessInstaller serviceProcessInstaller;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MediaWatcher));

        // The main entry point for the process
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new MediaWatcher() };
            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }
        /// <summary>
        /// /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServiceName = "CasparCG-MediaWatcher";
        }

        /// <summary>
        /// Set things in motion so your service can do its work.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            XmlConfigurator.Configure();
           
            try
            {
                casparDatabaseServerHostname = ConfigurationManager.AppSettings["MySQLHostname"];
                casparDatabaseServerUsername = ConfigurationManager.AppSettings["MySQLUser"];
                casparDatabaseServerPassword = ConfigurationManager.AppSettings["MySQLPass"];
                casparDatabaseServerDatabase = ConfigurationManager.AppSettings["MySQLDatabase"];
                casparDatabaseServerConnectiontimeout = ConfigurationManager.AppSettings["SQLConnectonTimeout"];
                DirectoryToWatch = ConfigurationManager.AppSettings["DirectoryToWatch"];

            }
            catch (ArgumentOutOfRangeException e)
            {
                Logger.Info(e.Message);
            }
            Logger.Info("=========================");
            Logger.Info("Starting Application...");
            Logger.Info("=========================");
            Logger.Info("Connecting to Database");
            connectDatabase();

            // This is the path we want to monitor
            _watchFolder.Path = DirectoryToWatch;

            // Make sure you use the OR on each Filter because we need to monitor
            // all of those activities

            _watchFolder.NotifyFilter = System.IO.NotifyFilters.DirectoryName;

            _watchFolder.NotifyFilter = _watchFolder.NotifyFilter | System.IO.NotifyFilters.FileName;
            _watchFolder.NotifyFilter = _watchFolder.NotifyFilter | System.IO.NotifyFilters.Attributes;

            // Now hook the triggers(events) to our handler (eventRaised)
            _watchFolder.Changed += new FileSystemEventHandler(eventRaised);
            _watchFolder.Created += new FileSystemEventHandler(eventRaised);
            _watchFolder.Deleted += new FileSystemEventHandler(eventRaised);

            // Occurs when a file or directory is renamed in the specific path
            _watchFolder.Renamed += new System.IO.RenamedEventHandler(eventRenameRaised);

            // And at last.. We connect our EventHandles to the system API (that is all
            // wrapped up in System.IO)
            try
            {
                _watchFolder.EnableRaisingEvents = true;
                Logger.Info("Up and running... Monitoring: " + DirectoryToWatch);
                
            }
            catch (ArgumentException)
            {
                abortActivityMonitoring();
            }

            
            

        }
        protected override void OnStop()
        {
            abortActivityMonitoring();
            _connection.Close();
        }

        private void abortActivityMonitoring()
        {
            _watchFolder.EnableRaisingEvents = false;
        }

        /// <summary>
        /// Triggerd when an event is raised from the folder acitivty monitoring.
        /// All types exists in System.IO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">containing all data send from the event that got executed.</param>
        private void eventRaised(object sender, System.IO.FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    Logger.Info(string.Format("File {0} has been modified\r\n", e.FullPath));
                    ChangedMedia(e);
                    break;
                case WatcherChangeTypes.Created:
                    Logger.Info(string.Format("File {0} has been created\r\n", e.FullPath));
                    AddNewMedia(e);
                    break;
                case WatcherChangeTypes.Deleted:
                    Logger.Info(string.Format("File {0} has been deleted\r\n", e.FullPath));
                    DeletedMedia(e);

                    break;
                default: // Another action
                    break;
            }
        }

        private void DeletedMedia(FileSystemEventArgs e)
        {
        }

        private void ChangedMedia(FileSystemEventArgs e)
        {

            //Ok, heres the plan.
            //Monitor a directory for a file change...
            //If its an xml extension, read it and get the media injest details for the "media" table and name in data
            //Convert file to "House Standard" and move the import to successful folder or invalid if not completed.
            //Take a snapshot of the converted file into memory
            //Insert SQL if doesn't exist already / or Update with new snapshot & media details.

            XPathNavigator nav;
            XPathDocument docNav;
            XPathNodeIterator NodeIter;
            XPathExpression expr;

            if (Path.GetExtension(e.FullPath) == ".xml")
            {
                // Open the XML.
                docNav = new XPathDocument(e.FullPath);
                // Create a navigator to query with XPath.
                nav = docNav.CreateNavigator();
               expr = nav.Compile("/asset/infilename");
              XPathNodeIterator iterator = nav.Select(expr);

              try
                {
                while (iterator.MoveNext())
                  {
                    
                  }
                }
                catch(Exception ex) 
                {
                  Console.WriteLine(ex.Message);
                }
                
                MediaFile file = new MediaFile(e.FullPath);
                Logger.Info("Opened Media Stream: " + e.FullPath);
                Random random = new Random();
                VideoHandler vh = new VideoHandler();

                foreach (DecoderStream stream in file.Streams)
                {
                    VideoDecoderStream videoStream = stream as VideoDecoderStream;
                    if (videoStream != null)
                    {
                        vh.Stream = new VideoScalingStream(videoStream, 720, 576, FFmpegSharp.Interop.Util.PixelFormat.PIX_FMT_RGB32);
                    }
                }
            }


            

    
        }



        private void AddNewMedia(FileSystemEventArgs e)
        {
          
        }

        private string GetMediaPathString(string p)
        {
           return "d:\\casparcg\\_media\\" + p;
        }

      

        private void connectDatabase()
        {
            String connectionString = CreateConnStr(casparDatabaseServerHostname,
                                                                            casparDatabaseServerDatabase,
                                                                            casparDatabaseServerUsername,
                                                                            casparDatabaseServerPassword,
                                                                            casparDatabaseServerConnectiontimeout);
            _connection = new MySqlConnection(connectionString);
            _connection.StateChange += connectionStateChange;

            try
            {
                _connection.Open();
            }
            catch (MySqlException e)
            {
                Logger.Fatal("MySQL - Couldn't connect (ConnectDatabase()): " + e.Data);
            }
        }

        private void connectionStateChange(object sender, StateChangeEventArgs e)
        {
            Logger.Info("Mysql Connection State: " + e.CurrentState);
            if (e.CurrentState == ConnectionState.Open)
            {
              Logger.Info("Database connected to: " +  casparDatabaseServerHostname +"/"+casparDatabaseServerDatabase);
            }

            if (e.CurrentState == ConnectionState.Closed)
            {
                 Logger.Info("Database disconnect");
            }
        }

        /// <summary>
        /// When a folder or file is renamed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void eventRenameRaised(object sender, System.IO.RenamedEventArgs e)
        {
            Logger.Info(string.Format("File {0} has been renamed to {1}\r\n", e.OldName, e.Name));
        }

        public static string CreateConnStr(string server, string databaseName, string user, string pass, string timeout)
        {
            //build the connection string
            string connStr = "server=" + server + ";database=" + databaseName + ";uid=" +
                user + ";password=" + pass + ";ConnectionTimeout=" + timeout + ";";

            //return the connection string
            return connStr;
        }
    }
}