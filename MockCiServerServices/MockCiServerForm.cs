using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;

namespace MockCiServerServices
{
    public partial class MockCiServerForm : Form
    {
        public MockCiServerForm()
        {
            InitializeComponent();
            _project1.ProjectName = "Project 1";
            _project2.ProjectName = "Project 2";
            _project3.ProjectName = "Project 3";
        }

        public IList<BuildStatus> GetBuildStatus()
        {
            if (_serverUnavailable.Checked)
            {
                throw new ServerUnavailableException("Message", new Exception("Inner Message"));
            }
            IList<BuildStatus> results = null;
            this.Invoke(() =>
            {
                results = new List<BuildStatus> {
                        _project1.GetBuildStatus(),
                        _project2.GetBuildStatus(),
                        _project3.GetBuildStatus()
                    };
            });
            return results;
        }

        public void StopWatching()
        {

        }
    }
}
