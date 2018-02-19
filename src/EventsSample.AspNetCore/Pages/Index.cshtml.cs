using EventsSample.AspNetCore.Events;
using JKang.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsSample.AspNetCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IMemoryCache _memoryCache;

        public IndexModel(
            IEventPublisher eventPublisher,
            IMemoryCache memoryCache)
        {
            _eventPublisher = eventPublisher;
            _memoryCache = memoryCache;
        }

        public List<string> Messages { get; private set; }

        [BindProperty]
        public string Message { get; set; }

        public void OnGet()
        {
            Messages = _memoryCache.Get<List<string>>("messages") ?? new List<string>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var @event = MessageSent.Create(Message);
            await _eventPublisher.PublishEventAsync(@event);

            return RedirectToPage();
        }
    }
}