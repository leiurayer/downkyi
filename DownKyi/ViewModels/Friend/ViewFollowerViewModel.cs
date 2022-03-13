using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownKyi.ViewModels.Friend
{
    public class ViewFollowerViewModel : BaseViewModel
    {
        public const string Tag = "PageFriendFollower";

        public ViewFollowerViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}
