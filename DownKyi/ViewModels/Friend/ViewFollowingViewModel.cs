using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownKyi.ViewModels.Friend
{
    public class ViewFollowingViewModel : BaseViewModel
    {
        public const string Tag = "PageFriendFollowing";

        public ViewFollowingViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}
