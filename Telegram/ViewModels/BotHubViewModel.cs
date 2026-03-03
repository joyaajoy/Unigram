//
// Copyright (c) Fela Ameghino 2015-2026
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//

using System.Threading.Tasks;
using Telegram.Collections;
using Telegram.Navigation;
using Telegram.Navigation.Services;
using Telegram.Services;
using Windows.UI.Xaml.Navigation;

namespace Telegram.ViewModels
{
    public partial class BotHubViewModel : ViewModelBase
    {
        public BotHubViewModel(IClientService clientService, ISettingsService settingsService, IEventAggregator aggregator)
            : base(clientService, settingsService, aggregator)
        {
            BotTasks = new MvxObservableCollection<BotHubTaskItem>();
            AgentTasks = new MvxObservableCollection<BotHubTaskItem>();
            DeploymentTasks = new MvxObservableCollection<BotHubTaskItem>();
        }

        protected override Task OnNavigatedToAsync(object parameter, NavigationMode mode, NavigationState state)
        {
            if (BotTasks.Empty())
            {
                BotTasks.Add(new BotHubTaskItem { Title = "Configure bot commands" });
                BotTasks.Add(new BotHubTaskItem { Title = "Set up webhook endpoint" });
                BotTasks.Add(new BotHubTaskItem { Title = "Define message presets" });
            }

            if (AgentTasks.Empty())
            {
                AgentTasks.Add(new BotHubTaskItem { Title = "Define project goals" });
                AgentTasks.Add(new BotHubTaskItem { Title = "Create automation pipeline" });
                AgentTasks.Add(new BotHubTaskItem { Title = "Set up monitoring" });
            }

            if (DeploymentTasks.Empty())
            {
                DeploymentTasks.Add(new BotHubTaskItem { Title = "Set up server environment" });
                DeploymentTasks.Add(new BotHubTaskItem { Title = "Configure environment variables" });
                DeploymentTasks.Add(new BotHubTaskItem { Title = "Run deployment tests" });
            }

            return Task.CompletedTask;
        }

        // App format/type settings
        private BotHubAppFormat _appFormat = BotHubAppFormat.Html;
        public BotHubAppFormat AppFormat
        {
            get => _appFormat;
            set => Set(ref _appFormat, value);
        }

        private BotHubMarkupType _markupType = BotHubMarkupType.InlineKeyboard;
        public BotHubMarkupType MarkupType
        {
            get => _markupType;
            set => Set(ref _markupType, value);
        }

        // Computed bool properties for RadioButton two-way binding
        public bool IsFormatHtml
        {
            get => _appFormat == BotHubAppFormat.Html;
            set { if (value) AppFormat = BotHubAppFormat.Html; }
        }

        public bool IsFormatMarkdown
        {
            get => _appFormat == BotHubAppFormat.Markdown;
            set { if (value) AppFormat = BotHubAppFormat.Markdown; }
        }

        public bool IsFormatPlainText
        {
            get => _appFormat == BotHubAppFormat.PlainText;
            set { if (value) AppFormat = BotHubAppFormat.PlainText; }
        }

        public bool IsMarkupInlineKeyboard
        {
            get => _markupType == BotHubMarkupType.InlineKeyboard;
            set { if (value) MarkupType = BotHubMarkupType.InlineKeyboard; }
        }

        public bool IsMarkupReplyKeyboard
        {
            get => _markupType == BotHubMarkupType.ReplyKeyboard;
            set { if (value) MarkupType = BotHubMarkupType.ReplyKeyboard; }
        }

        public bool IsMarkupForceReply
        {
            get => _markupType == BotHubMarkupType.ForceReply;
            set { if (value) MarkupType = BotHubMarkupType.ForceReply; }
        }

        // Bot search query
        private string _searchQuery = string.Empty;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                Set(ref _searchQuery, value);
                FilterBotPresets(value);
            }
        }

        // Bot presets
        private MvxObservableCollection<BotHubPreset> _botPresets;
        public MvxObservableCollection<BotHubPreset> BotPresets => _botPresets ??= CreateDefaultPresets();

        private MvxObservableCollection<BotHubPreset> CreateDefaultPresets()
        {
            return new MvxObservableCollection<BotHubPreset>
            {
                new BotHubPreset { Name = "Support Bot", Description = "Automatically handles customer support queries" },
                new BotHubPreset { Name = "News Bot", Description = "Sends regular news updates to subscribers" },
                new BotHubPreset { Name = "E-commerce Bot", Description = "Manages orders and product queries" },
            };
        }

        private void FilterBotPresets(string query)
        {
            // In a full implementation this would filter presets by query
            RaisePropertyChanged(nameof(BotPresets));
        }

        // TODO lists
        public MvxObservableCollection<BotHubTaskItem> BotTasks { get; }
        public MvxObservableCollection<BotHubTaskItem> AgentTasks { get; }
        public MvxObservableCollection<BotHubTaskItem> DeploymentTasks { get; }

        public void AddBotTask(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                BotTasks.Add(new BotHubTaskItem { Title = title });
            }
        }

        public void AddAgentTask(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                AgentTasks.Add(new BotHubTaskItem { Title = title });
            }
        }

        public void AddDeploymentTask(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                DeploymentTasks.Add(new BotHubTaskItem { Title = title });
            }
        }

        public void RemoveBotTask(BotHubTaskItem item)
        {
            BotTasks.Remove(item);
        }

        public void RemoveAgentTask(BotHubTaskItem item)
        {
            AgentTasks.Remove(item);
        }

        public void RemoveDeploymentTask(BotHubTaskItem item)
        {
            DeploymentTasks.Remove(item);
        }
    }

    public enum BotHubAppFormat
    {
        Html = 0,
        Markdown = 1,
        PlainText = 2
    }

    public enum BotHubMarkupType
    {
        InlineKeyboard = 0,
        ReplyKeyboard = 1,
        ForceReply = 2
    }

    public partial class BotHubPreset : BindableBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }
    }

    public partial class BotHubTaskItem : BindableBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set => Set(ref _isDone, value);
        }
    }
}
