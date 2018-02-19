using EventsSample.AspNetCore.Events;
using JKang.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EventsSample.AspNetCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IEventPublisher _eventPublisher;

        public IndexModel(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        [BindProperty]
        public string Message { get; set; }

        public async Task OnPostAsync()
        {
            var @event = MessageSent.Create(Message);
            await _eventPublisher.PublishEventAsync(@event);
        }
    }
}