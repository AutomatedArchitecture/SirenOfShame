using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class ViewBuilds : UserControl
    {
        public event GettingStartedClick OnGettingStartedClick;
        public event SelectedBuildChanged SelectedBuildChanged;

        private void OnSelectedBuildChanged(string buildId)
        {
            SelectedBuildChanged handler = SelectedBuildChanged;
            if (handler != null) handler(this, new SelectedBuildChangedArgs { BuildId = buildId });
        }

        private void InvokeOnGettingStartedClick(object sender, GettingStartedOpenDialogArgs args)
        {
            if (args.GettingStartedClickType == GettingStartedClickTypeEnum.NeverShowGettingStarted)
            {
                _settings.NeverShowGettingStarted = true;
                _settings.Save();
                DisposeGettingStarted();
            } 
            else
            {
                GettingStartedClick handler = OnGettingStartedClick;
                if (handler != null) handler(this, args);
            }
        }

        private SirenOfShameSettings _settings;
        private readonly Timer _prettyDateTimer = new Timer();
        private List<BuildStatusDto> _lastBuildStatusDtos = new List<BuildStatusDto>();

        private class BuildStatusDtoAndControl
        {
            public BuildStatusDto BuildStatusDto { get; set; }
            public ViewBuildBase Control { get; set; }
        }

        public ViewBuilds()
        {
            InitializeComponent();

            _gettingStarted.OnGettingStartedClick += InvokeOnGettingStartedClick;

            _prettyDateTimer.Interval = 20000;
            _prettyDateTimer.Tick += PrettyDateTimerOnTick;
            _prettyDateTimer.Start();
        }

        private void InitializeForBuild(string buildId)
        {
            this.SuspendDrawing(() =>
            {
                OnSelectedBuildChanged(buildId);
                bool viewAllBuilds = buildId == null;
                var buildStatusDto = _lastBuildStatusDtos.FirstOrDefault(i => i.Id == buildId);
                InitializeLabelsForBuild(viewAllBuilds);
                InitializeViewBuildBig(buildStatusDto);
                InitializeDisplayModes();
                InitializeSmallBuildsVisibility(buildId);
                SortExistingControls();
            });
        }

        private void InitializeSmallBuildsVisibility(string activeBuildId)
        {
            var smallBuilds = GetSmallViewBuilds();
            foreach (var viewBuildSmall in smallBuilds)
            {
                var isActive = viewBuildSmall.BuildId == activeBuildId;
                viewBuildSmall.Visible = !isActive;
            }
        }

        private void InitializeViewBuildBig(BuildStatusDto buildStatusDto)
        {
            if (buildStatusDto == null) return;
            _viewBuildBig.InitializeForBuild(buildStatusDto);
        }

        private void InitializeLabelsForBuild(bool viewAllBuilds)
        {
            _back.Visible = !viewAllBuilds;
            _viewBuildBig.Visible = !viewAllBuilds;
        }

        private void PrettyDateTimerOnTick(object sender, EventArgs eventArgs)
        {
            GetViewBuilds().ToList().ForEach(i => i.RecalculatePrettyDate());
        }

        public void RefreshBuildStatuses(RefreshStatusEventArgs args)
        {
            _lastBuildStatusDtos = args.BuildStatusDtos.ToList();
            var buildStatusDtosAndControl = GetBuildStatusDtoAndControls(_lastBuildStatusDtos).ToList();
            RemoveAllChildControlsIfBuildCountOrBuildNamesChanged(buildStatusDtosAndControl, _lastBuildStatusDtos);
            if (NoChildControlsExist)
            {
                CreateControlsAndAddToPanels(_lastBuildStatusDtos);
            } else
            {
                UpdateExistingControls(buildStatusDtosAndControl);
            }
            InitializeDisplayModes();
        }

        private bool IsViewBuildBigVisible
        {
            get { return _viewBuildBig.Visible; }
        }
        
        public static int GetIdealSmallRows(int count)
        {
            if (count <= 8) return 2;
            if (count <= 10) return 1;
            return 0;
        }
        
        private void InitializeDisplayModes()
        {
            int idealSmallControlCountPerRow = GetIdealSmallControlCountPerRow();
            _viewBuildBig.Width = GetIdealBigControlWidth(idealSmallControlCountPerRow);

            var controls = GetSmallViewBuilds().ToList();
            var idealSmallControlCount = idealSmallControlCountPerRow * GetIdealSmallRows(controls.Count);
            for (int i = 0; i < controls.Count; i++)
            {
                var control = controls[i];
                var mode = GetDisplayMode(idealSmallControlCount, i);
                control.SetDisplayMode(mode);
            }
        }

        private static int GetIdealBigControlWidth(int idealSmallControlCountPerRow)
        {
            idealSmallControlCountPerRow = Math.Max(idealSmallControlCountPerRow, 2);
            int marginWidths = ((idealSmallControlCountPerRow - 1)*ViewBuildSmall.MARGIN*2);
            int smallControlWidths = (idealSmallControlCountPerRow*ViewBuildSmall.WIDTH);
            return smallControlWidths + marginWidths;
        }

        private ViewBuildDisplayMode GetDisplayMode(int controlCount, int idealSmallControlCount)
        {
            if (IsViewBuildBigVisible) return ViewBuildDisplayMode.Tiny;
            return idealSmallControlCount < controlCount ? ViewBuildDisplayMode.Normal : ViewBuildDisplayMode.Tiny;
        }

        private void RemoveAllChildControlsIfBuildCountOrBuildNamesChanged(ICollection buildStatusDtosAndControl, ICollection<BuildStatusDto> buildStatusDtos)
        {
            int visibleViewBuildControls = VisibleViewBuildControls;
            bool numberOfBuildsChanged = visibleViewBuildControls != 0 && visibleViewBuildControls != buildStatusDtos.Count();
            var anyBuildNameChanged = buildStatusDtosAndControl.Count != buildStatusDtos.Count;
            if (numberOfBuildsChanged || anyBuildNameChanged)
            {
                RemoveAllSmallBuildControls();
            }
        }

        private int VisibleViewBuildControls
        {
            get { return _mainFlowLayoutPanel.Controls.Cast<Control>().Count(i => i.Visible); }
        }

        private bool NoChildControlsExist
        {
            get { return VisibleViewBuildControls == 0; }
        }

        private void RemoveAllSmallBuildControls()
        {
            var smallBuilds = GetSmallViewBuilds().ToList();
            foreach (var viewBuildSmall in smallBuilds)
            {
                _mainFlowLayoutPanel.Controls.Remove(viewBuildSmall);
                viewBuildSmall.Dispose();
            }
            _viewBuildBig.Visible = false;
        }

        private IEnumerable<BuildStatusDtoAndControl> GetBuildStatusDtoAndControls(IEnumerable<BuildStatusDto> buildStatusDtos)
        {
            return from control in GetViewBuilds() 
                   where control.Visible
                   join buildStatusDto in buildStatusDtos on control.BuildId equals buildStatusDto.Id
                   orderby buildStatusDto.LocalStartTime descending 
                   select new BuildStatusDtoAndControl { Control = control, BuildStatusDto = buildStatusDto };
        }

        private void UpdateExistingControls(IEnumerable<BuildStatusDtoAndControl> buildStatusDtosAndControl)
        {
            UpdateDetailsInExistingControls(buildStatusDtosAndControl);
            SortExistingControls();
        }

        private static void UpdateDetailsInExistingControls(IEnumerable<BuildStatusDtoAndControl> buildStatusDtosAndControl)
        {
            foreach (var buildStatusAndControl in buildStatusDtosAndControl)
            {
                buildStatusAndControl.Control.UpdateListItem(buildStatusAndControl.BuildStatusDto);
            }
        }

        private void SortExistingControls()
        {
            var buildsExceptActiveOne = GetSmallViewBuilds().OrderByDescending(i => i.LocalStartTime).ToList();
            var startIndex = _viewBuildBig.Visible ? 1 : 0;
            for (int i = 0; i < buildsExceptActiveOne.Count; i++)
            {
                var buildStatusAndControl = buildsExceptActiveOne[i];
                _mainFlowLayoutPanel.Controls.SetChildIndex(buildStatusAndControl, i + startIndex);
            }
            _mainFlowLayoutPanel.Controls.SetChildIndex(_viewBuildBig, 0);
        }

        private void CreateControlsAndAddToPanels(IEnumerable<BuildStatusDto> buildStatusDtos)
        {
            this.SuspendDrawing(() =>
            {
                var buildsOrdered = buildStatusDtos
                    .OrderByDescending(i => i.LocalStartTime)
                    .ToList();

                buildsOrdered
                    .Select(CreateViewBuildSmall)
                    .ToList()
                    .ForEach(i => _mainFlowLayoutPanel.Controls.Add(i));
            });
        }

        private ViewBuildSmall CreateViewBuildSmall(BuildStatusDto i)
        {
            var viewBuildSmall = new ViewBuildSmall(i, _settings);
            viewBuildSmall.Click += ViewBuildSmallOnClick;
            viewBuildSmall.MouseEnter += ViewBuildSmallOnMouseEnter;
            return viewBuildSmall;
        }

        private void ViewBuildSmallOnMouseEnter(object sender, EventArgs e)
        {
            EnableMouseScrollWheel();
        }

        private void EnableMouseScrollWheel()
        {
            if (Parent.ContainsFocus)
                _mainFlowLayoutPanel.Focus();
        }
        
        private void ViewBuildSmallOnClick(object sender, EventArgs eventArgs)
        {
            ViewBuildSmall viewBuildSmall = (ViewBuildSmall)sender;
            InitializeForBuild(viewBuildSmall.BuildId);
        }

        private int GetIdealSmallControlCountPerRow()
        {
            return (_mainFlowLayoutPanel.Width) / (ViewBuildSmall.MARGIN + ViewBuildSmall.WIDTH + ViewBuildSmall.MARGIN);
        }

        private IEnumerable<ViewBuildSmall> GetSmallViewBuilds()
        {
            return _mainFlowLayoutPanel.Controls
                .Cast<Control>()
                .Select(i => i as ViewBuildSmall)
                .Where(i => i != null);
        }
        
        private IEnumerable<ViewBuildBase> GetViewBuilds()
        {
            return _mainFlowLayoutPanel.Controls.Cast<ViewBuildBase>();
        }

        public void Initialize(SirenOfShameSettings settings)
        {
            _settings = settings;
            _viewBuildBig.Settings = _settings;

            InitializeGettingStarted();
        }

        private void InitializeGettingStarted()
        {
            bool gettingStartedWasPreviouslyClosed = _gettingStarted == null;
            if (gettingStartedWasPreviouslyClosed) return;
            
            bool isGettingStarted = _settings.IsGettingStarted();
            _gettingStarted.Visible = isGettingStarted;
            if (isGettingStarted)
            {
                _gettingStarted.Initialize(_settings);
            } 
            else
            {
                DisposeGettingStarted();
            }
        }

        private void DisposeGettingStarted()
        {
            _gettingStarted.OnGettingStartedClick -= InvokeOnGettingStartedClick;
            _gettingStarted.Dispose();
            _gettingStarted = null;
        }

        private void BackClick(object sender, EventArgs e)
        {
            InitializeForBuild(null);
        }

        public void ReinitializeGettingStarted()
        {
            InitializeGettingStarted();
        }

        private void MainFlowLayoutPanelMouseEnter(object sender, EventArgs e)
        {
            EnableMouseScrollWheel();
        }

        public void RefreshStats()
        {
            if (_viewBuildBig != null)
                _viewBuildBig.RefreshStats();
        }
    }

    public delegate void SelectedBuildChanged(object sender, SelectedBuildChangedArgs args);

    public class SelectedBuildChangedArgs
    {
        public string BuildId { get; set; }
    }
}
