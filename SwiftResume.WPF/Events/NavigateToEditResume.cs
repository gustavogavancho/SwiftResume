using Prism.Events;

namespace SwiftResume.WPF.Events;

public class NavigateToEditResume : PubSubEvent<NavigateToEditResumeArgs>
{

}

public struct NavigateToEditResumeArgs
{
    public int Id { get; set; }
}