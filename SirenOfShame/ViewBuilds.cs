using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class ViewBuilds : UserControl
    {
        private SirenOfShameSettings _settings;

        public ViewBuilds()
        {
            InitializeComponent();

            _prettyDateTimer.Interval = 20000;
            _prettyDateTimer.Tick += PrettyDateTimerOnTick;
            _prettyDateTimer.Start();
        }

        private void PrettyDateTimerOnTick(object sender, EventArgs eventArgs)
        {
            GetViewBuilds().ToList().ForEach(i => i.RecalculatePrettyDate());
        }

        private readonly Timer _prettyDateTimer = new Timer();

        public void RefreshBuildStatuses(RefreshStatusEventArgs args)
        {
            var buildStatusDtos = args.BuildStatusDtos.ToList();
            bool numberOfBuildsChanged = ViewBuildsCount != 0 && ViewBuildsCount != buildStatusDtos.Count();

            var buildStatusDtosAndControl = (
                from control in GetViewBuilds()
                join buildStatusDto in buildStatusDtos on control.BuildName equals buildStatusDto.Name
                orderby buildStatusDto.LocalStartTime descending 
                select new { control, buildStatusDto }
                ).ToList();

            var anyBuildNameChanged = buildStatusDtosAndControl.Count != buildStatusDtos.Count;
            if (numberOfBuildsChanged || anyBuildNameChanged)
            {
                flowLayoutPanel1.Controls.Clear();
            }
            if (ViewBuildsCount == 0)
            {
                buildStatusDtos
                    .OrderByDescending(i => i.LocalStartTime)
                    .Select(i => new ViewBuildSmall(i, _settings))
                    .ToList()
                    .ForEach(i => flowLayoutPanel1.Controls.Add(i));
            }
            else
            {
                for (int i = 0; i < buildStatusDtosAndControl.Count; i++)
                {
                    var buildStatusAndControl = buildStatusDtosAndControl[i];
                    buildStatusAndControl.control.UpdateListItem(buildStatusAndControl.buildStatusDto);
                    flowLayoutPanel1.Controls.SetChildIndex(buildStatusAndControl.control, i);
                }
            }
        }

        private int ViewBuildsCount
        {
            get { return flowLayoutPanel1.Controls.Count; }
        }

        private IEnumerable<ViewBuildSmall> GetViewBuilds()
        {
            return flowLayoutPanel1.Controls.Cast<ViewBuildSmall>();
        }

        public void Initialize(SirenOfShameSettings settings)
        {
            _settings = settings;
        }
    }
}
