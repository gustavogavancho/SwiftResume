using Prism.Events;

namespace SwiftResume.WPF.Events;

public class NavigateToEditResume : PubSubEvent<NavigateToEditResumeArgs>
{

}

public class NavigateToEditResumeArgs
{
    public int Id { get; set; }
}