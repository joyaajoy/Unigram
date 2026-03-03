//
// Copyright (c) Fela Ameghino 2015-2026
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//

using Telegram.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Telegram.Views
{
    public sealed partial class BotHubPage : HostedPage
    {
        public BotHubViewModel ViewModel => DataContext as BotHubViewModel;

        public BotHubPage()
        {
            InitializeComponent();
            Title = Strings.BotHub;
        }

        private void AddBotTask_Click(object sender, RoutedEventArgs e)
        {
            var title = NewBotTaskBox.Text?.Trim();
            if (!string.IsNullOrEmpty(title))
            {
                ViewModel.AddBotTask(title);
                NewBotTaskBox.Text = string.Empty;
            }
        }

        private void AddAgentTask_Click(object sender, RoutedEventArgs e)
        {
            var title = NewAgentTaskBox.Text?.Trim();
            if (!string.IsNullOrEmpty(title))
            {
                ViewModel.AddAgentTask(title);
                NewAgentTaskBox.Text = string.Empty;
            }
        }

        private void AddDeploymentTask_Click(object sender, RoutedEventArgs e)
        {
            var title = NewDeploymentTaskBox.Text?.Trim();
            if (!string.IsNullOrEmpty(title))
            {
                ViewModel.AddDeploymentTask(title);
                NewDeploymentTaskBox.Text = string.Empty;
            }
        }
    }
}
