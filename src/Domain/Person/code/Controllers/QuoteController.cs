namespace Habitat.Person.Controllers
{
    using System.Web.Mvc;
    using Habitat.Person.Repositories;
    using Sitecore.Mvc.Presentation;

    public class QuoteController : Controller
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuoteController() : this(new QuoteRepository(RenderingContext.Current.Rendering.Item))
        {
        }

        public QuoteController(IQuoteRepository quoteRepository)
        {
            this._quoteRepository = quoteRepository;
        }

        public ActionResult Quote()
        {
            var item = this._quoteRepository.Get();
            return this.View("Quote", item);
        }
    }
}